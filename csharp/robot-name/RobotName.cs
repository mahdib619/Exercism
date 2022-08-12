using System;
using System.Collections.Generic;

public class Robot
{
    private static readonly HashSet<string> _generatedNames = new();

    private string _name;
    public string Name
    {
        get
        {
            if (_name is not null)
                return _name;

            while (true)
            {
                _name = $"{RandomChar()}{RandomChar()}{new Random().Next(100, 999)}";
                if (_generatedNames.Add(_name))
                    break;
            }

            return _name;
        }
    }

    public void Reset() => _name = null;

    private static char RandomChar() => (char)new Random().Next('A', 'Z' + 1);
}