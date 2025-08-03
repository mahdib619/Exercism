using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;

public class Hangman
{
    private readonly Dictionary<char, LinkedList<int>> _charIndexes = new();
    public HangmanStateObservable StateObservable { get; } = new();
    public IObserver<char> GuessObserver { get; }
    public HangmanState LastState { get; private set; }

    public Hangman(string word)
    {
        GuessObserver = new AnonymousObserver<char>(Guess);
        LastState = new() { MaskedWord = new('_', word.Length) };

        for (var i = 0; i < word.Length; i++)
        {
            var ch = word[i];
            if (!_charIndexes.TryGetValue(ch, out var ixs))
                _charIndexes[ch] = ixs = new();

            ixs.AddLast(i);
        }

        StateObservable.OnNewState(LastState);
    }

    private void Guess(char ch)
    {
        if (LastState.RemainingGuesses == 0)
            StateObservable.OnError(new TooManyGuessesException());

        LastState = LastState.Next(ch, _charIndexes.GetValueOrDefault(ch, new()));

        if (LastState.IsCompleted)
            StateObservable.OnCompleted();
        else
            StateObservable.OnNewState(LastState);
    }
}

public class HangmanState
{
    public string MaskedWord { get; init; }
    public ImmutableHashSet<char> GuessedChars { get; init; } = ImmutableHashSet<char>.Empty;
    public int RemainingGuesses { get; init; } = 9;
    public bool IsCompleted => MaskedWord.All(char.IsLetter);

    public HangmanState Next(char guess, IReadOnlyCollection<int> guessIndexes)
    {
        var maskArr = MaskedWord.ToCharArray();
        foreach (var gi in guessIndexes)
            maskArr[gi] = guess;

        return new()
        {
            MaskedWord = string.Join("", maskArr),
            GuessedChars = GuessedChars.Append(guess).ToImmutableHashSet(),
            RemainingGuesses = guessIndexes.Count > 0 && !GuessedChars.Contains(guess) ? RemainingGuesses : RemainingGuesses - 1
        };
    }
}

public class HangmanStateObservable : IObservable<HangmanState>
{
    private bool _isCompleted;
    private Exception _exception;
    private readonly LinkedList<HangmanState> _states = new();
    private readonly HashSet<IObserver<HangmanState>> _observers = new();

    public IDisposable Subscribe(IObserver<HangmanState> observer)
    {
        if (OnNewSubscribe(observer))
            _observers.Add(observer);

        return Disposable.Create(() => _observers.Remove(observer));
    }

    private bool OnNewSubscribe(IObserver<HangmanState> observer)
    {
        if (_isCompleted)
        {
            observer.OnCompleted();
            return false;
        }

        foreach (var state in _states.Skip(_observers.Count == 0 ? 0 : 1))//only notify initial state to first subscriber
            observer.OnNext(state);

        var add = true;

        if (_exception is not null)
        {
            observer.OnError(_exception);
            add = false;
        }

        return add;
    }

    public void OnNewState(HangmanState state)
    {
        _states.AddLast(state);

        foreach (var observer in _observers)
            observer.OnNext(state);
    }

    public void OnCompleted()
    {
        _isCompleted = true;

        foreach (var observer in _observers)
            observer.OnCompleted();

        _observers.Clear();
    }

    public void OnError(Exception exception)
    {
        _exception = exception;

        foreach (var observer in _observers)
            observer.OnError(exception);

        _observers.Clear();
    }
}

public class TooManyGuessesException : Exception
{
}