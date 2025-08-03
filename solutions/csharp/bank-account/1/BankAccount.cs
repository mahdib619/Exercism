using System;

public class BankAccount
{
    private bool _isOpen;

    public void Open() => _isOpen = true;

    public void Close() => _isOpen = false;

    private decimal _balance;
    public decimal Balance => _isOpen ? _balance : throw new InvalidOperationException("Can't get Closed accont balance!");

    public void UpdateBalance(decimal change)
    {
        lock (this)
        {
            _balance += change;
        }
    }
}
