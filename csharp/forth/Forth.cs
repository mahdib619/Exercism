using System;
using System.Collections.Generic;
using System.Linq;

using Sprache;

public static class Forth
{
    private static Dictionary<string, WordDefinition> _wordDefinitions;
    private static Stack<int> _stack;

    public static string Evaluate(string[] instructions)
    {
        var allExceptSpace = Parse.AnyChar.Except(Parse.WhiteSpace).Many().Text();
        var spaceDelimitedItems = allExceptSpace.DelimitedBy(Parse.WhiteSpace);


        var mainOperators = from items in spaceDelimitedItems
                            select items.Select(GetItemFromStringCaseInsensitive);


        var startDefine = Parse.Regex(": ");
        var endDefine = Parse.Regex(" ;");

        var wordAndOperationsString = Parse.AnyChar.Except(startDefine).Except(endDefine).Many().Text();

        var wordAndOperationsForthItem = from name in allExceptSpace.Except(Parse.Number)
                                         from _ in Parse.WhiteSpace
                                         from operations in spaceDelimitedItems
                                         select new WordDefinition(name, operations.Select(GetItemFromStringCaseInsensitive).ToList());

        var fullWordDefinition = from definiation in wordAndOperationsString.Contained(startDefine, endDefine)
                                 select wordAndOperationsForthItem.Parse(definiation);

        (_wordDefinitions, _stack) = (new(), new());
        try
        {
            foreach (var instruction in instructions)
            {
                var fullWordDefinitionResult = fullWordDefinition.TryParse(instruction);

                if (fullWordDefinitionResult.WasSuccessful)
                    _wordDefinitions[fullWordDefinitionResult.Value.Name.ToLower()] = fullWordDefinitionResult.Value;
                else
                {
                    foreach (var operators in mainOperators.Parse(instruction))
                        operators.Process(_stack);
                }
            }
        }
        catch (ParseException)
        {
            throw new InvalidOperationException("Invalid Instruction!");
        }

        return string.Join(" ", _stack.Reverse());
    }

    private static ForthMember GetItemFromStringCaseInsensitive(string input) => GetItemFromString(input.ToLower());

    private static ForthMember GetItemFromString(string input) => input switch
    {
        _ when _wordDefinitions.ContainsKey(input) => _wordDefinitions[input],
        "+" => new BinaryOperator((a, b) => b + a),
        "-" => new BinaryOperator((a, b) => b - a),
        "*" => new BinaryOperator((a, b) => b * a),
        "/" => new BinaryOperator((a, b) => b / a),
        "dup" => new DupOperator(),
        "drop" => new DropOperator(),
        "swap" => new SwapOperator(),
        "over" => new OverOperator(),
        _ when int.TryParse(input, out var num) => new NumberOperand(num),
        _ => throw new InvalidOperationException($"Invalid operator {input}!")
    };

    private abstract class ForthMember
    {
        public abstract void Process(Stack<int> stack);
    }

    private class NumberOperand : ForthMember
    {
        public int Value { get; }

        public NumberOperand(int value) => Value = value;

        public override void Process(Stack<int> stack) => stack.Push(Value);
    }

    private class BinaryOperator : ForthMember
    {
        public Func<int, int, int> Operation { get; }

        public BinaryOperator(Func<int, int, int> operation) => Operation = operation;

        public override void Process(Stack<int> stack) => stack.Push(Operation(stack.Pop(), stack.Pop()));
    }

    private class DupOperator : ForthMember
    {
        public override void Process(Stack<int> stack) => stack.Push(stack.Peek());
    }

    private class DropOperator : ForthMember
    {
        public override void Process(Stack<int> stack) => stack.Pop();
    }

    private class SwapOperator : ForthMember
    {
        public override void Process(Stack<int> stack)
        {
            var (f, s) = (stack.Pop(), stack.Pop());
            stack.Push(f);
            stack.Push(s);
        }
    }

    private class OverOperator : ForthMember
    {
        public override void Process(Stack<int> stack)
        {
            var (f, s) = (stack.Pop(), stack.Pop());
            stack.Push(s);
            stack.Push(f);
            stack.Push(s);
        }
    }

    private class WordDefinition : ForthMember
    {
        public string Name { get; }
        public IReadOnlyCollection<ForthMember> Operations { get; }

        public WordDefinition(string name, IReadOnlyCollection<ForthMember> operations) => (Name, Operations) = (name, operations);

        public override void Process(Stack<int> stack)
        {
            foreach (var operation in Operations)
                operation.Process(stack);
        }
    }
}