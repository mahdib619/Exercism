using System;
using System.Threading;

public class BankAccount
{
    private readonly Mutex _mutex = new();
    private bool _isOpen;

    public void Open() => _isOpen = true;

    public void Close() => _isOpen = false;

    private decimal _balance;
    public decimal Balance => _isOpen ? _balance : throw new InvalidOperationException("Can't get Closed accont balance!");

    public void UpdateBalance(decimal change)
    {
        _mutex.WaitOne();

        try
        {
            _balance += change;
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }
}
