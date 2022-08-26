using System;

public struct ComplexNumber
{
    private readonly double _real;
    private readonly double _imag;

    public ComplexNumber(double real, double imaginary = 0) => (_real, _imag) = (real, imaginary);

    public double Real() => _real;
    public double Imaginary() => _imag;

    public ComplexNumber Mul(ComplexNumber other) => new(_real * other._real - _imag * other._imag, _real * other._imag + other._real * _imag);
    public ComplexNumber Add(ComplexNumber other) => new(_real + other._real, _imag + other._imag);
    public ComplexNumber Sub(ComplexNumber other) => new(_real - other._real, _imag - other._imag);
    public ComplexNumber Div(ComplexNumber other)
    {
        var otherPow = Math.Pow(other._real, 2) + Math.Pow(other._imag, 2);
        return new((_real * other._real + _imag * other._imag) / otherPow, (_imag * other._real - _real * other._imag) / otherPow);
    }

    public double Abs() => Math.Sqrt(_real * _real + _imag * _imag);
    public ComplexNumber Conjugate() => new(_real, -_imag);
    public ComplexNumber Exp() => new ComplexNumber(Math.Pow(Math.Exp(1), _real)).Mul(new ComplexNumber(Math.Cos(_imag), Math.Sin(_imag)));
}