using System;

public struct CurrencyAmount
{
    private readonly decimal _amount;
    private readonly string _currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        _amount = amount;
        _currency = currency;
    }

    public static bool operator ==(CurrencyAmount a, CurrencyAmount b) => ValidateCurrencies(a, b) && a._amount == b._amount;
    public static bool operator !=(CurrencyAmount a, CurrencyAmount b) => !(a == b);

    public static bool operator >(CurrencyAmount a, CurrencyAmount b) => ValidateCurrencies(a, b) && a._amount > b._amount;
    public static bool operator <(CurrencyAmount a, CurrencyAmount b) => ValidateCurrencies(a, b) && a._amount < b._amount;

    public static CurrencyAmount operator +(CurrencyAmount a, CurrencyAmount b)
    {
        ValidateCurrencies(a, b);
        return new(a._amount + b._amount, a._currency);
    }
    public static CurrencyAmount operator -(CurrencyAmount a, CurrencyAmount b)
    {
        ValidateCurrencies(a, b);
        return new(a._amount - b._amount, a._currency);
    }

    public static CurrencyAmount operator *(CurrencyAmount a, decimal b) => new(a._amount * b, a._currency);
    public static CurrencyAmount operator *(decimal b, CurrencyAmount a) => new(a._amount * b, a._currency);
    public static CurrencyAmount operator /(decimal b, CurrencyAmount a) => new(b / a._amount, a._currency);
    public static CurrencyAmount operator /(CurrencyAmount a, decimal b) => new(a._amount / b, a._currency);


    public static explicit operator double(CurrencyAmount a) => (double)a._amount;
    public static implicit operator decimal(CurrencyAmount a) => a._amount;

    private static bool ValidateCurrencies(CurrencyAmount a, CurrencyAmount b) => a._currency == b._currency ? true : throw new ArgumentException("Currencies must be the same!", nameof(b));
}
