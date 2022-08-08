using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
        Func<int, int, int> doOperation = operation switch
        {
            "+" => SimpleOperation.Addition,
            "*" => SimpleOperation.Multiplication,
            "/" => SimpleOperation.Division,
            "" => throw new ArgumentException("Empty operation!", nameof(operation)),
            null => throw new ArgumentNullException(nameof(operation)),
            _ => throw new ArgumentOutOfRangeException(nameof(operation)),
        };

        try
        {
            return $"{operand1} {operation} {operand2} = {doOperation(operand1, operand2)}";
        }
        catch (DivideByZeroException)
        {
            return "Division by zero is not allowed.";
        }
    }
}
