using System;
using System.Diagnostics;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    public RobotSimulator(Direction direction, int x, int y) => (Direction, X, Y) = (direction, x, y);

    public Direction Direction { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Move(string instructions)
    {
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case 'A':
                    Advance();
                    break;
                case 'R':
                    Direction = Direction.Right();
                    break;
                case 'L':
                    Direction = Direction.Left();
                    break;
                default:
                    throw new ArgumentException("Invalid instruction in param!", nameof(instructions));
            }
        }
    }

    private void Advance()
    {
        switch (Direction)
        {
            case Direction.North:
                Y++;
                break;
            case Direction.South:
                Y--;
                break;
            case Direction.East:
                X++;
                break;
            case Direction.West:
                X--;
                break;
            default:
                throw new UnreachableException("Invalid Enum value!");
        }
    }
}

internal static class Extentions
{
    public static Direction Right(this Direction dir) => dir.Rotate(1);
    public static Direction Left(this Direction dir) => dir.Rotate(-1);
    private static Direction Rotate(this Direction dir, int amount) => (Direction)((4 + amount + (int)dir) % 4);
}