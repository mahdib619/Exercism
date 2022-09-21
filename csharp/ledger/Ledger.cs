using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LedgerEntry : IComparable<LedgerEntry>
{
    public LedgerEntry(DateTime date, string desc, decimal chg)
    {
        Date = date;
        Desc = desc;
        Chg = chg;
    }

    public DateTime Date { get; }
    public string Desc { get; }
    public decimal Chg { get; }

    public int CompareTo(LedgerEntry other)
    {
        var chgCompare = (Chg < 0).CompareTo(other.Chg < 0);
        return chgCompare == 0 ? chgCompare : string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
    }

    public override string ToString() => $"Date:{Date}, Description:{Desc}, Change:{Chg}";
}

public static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string desc, int chng) => new(DateTime.Parse(date, CultureInfo.InvariantCulture), desc, chng / 100.0m);

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        var formatter = LedgerFormatterFactory.GetFormatter(locale, currency);
        var sortedEntries = entries.OrderBy(static a => a);
        return formatter.Format(sortedEntries);
    }
}

public abstract class LedgerFormatter
{
    protected CultureInfo CultureInfo { get; init; }
    protected abstract string LedgerHead { get; }

    public string Format(IEnumerable<LedgerEntry> ledger) => string.Join('\n', ledger.Select(Format).Prepend(LedgerHead));

    public string Format(LedgerEntry entry)
    {
        var date = FormatDate(entry.Date);
        var desc = FormatDescription(entry.Desc);
        var change = FormatChange(entry.Chg);

        return string.Join(" | ", date, desc, change);
    }

    private string FormatChange(decimal change) => (change.ToString("C", CultureInfo) + (change < 0 ? "" : " ")).PadLeft(13);
    private string FormatDate(DateTime date) => date.ToString("d", CultureInfo);
    private static string FormatDescription(string desc) => desc.Length > 25 ? $"{desc[..22]}..." : $"{desc,-25}";
}

public class AmericanLedgerFormatter : LedgerFormatter
{
    public AmericanLedgerFormatter(string currencySymbol) =>
        CultureInfo = new CultureInfo("en-US")
        {
            NumberFormat =
            {
                CurrencySymbol = currencySymbol,
                CurrencyNegativePattern = 0
            },
            DateTimeFormat =
            {
                ShortDatePattern = "MM/dd/yyyy"
            }
        };

    protected override string LedgerHead => "Date       | Description               | Change       ";
}

public class DutchLedgerFormatter : LedgerFormatter
{
    public DutchLedgerFormatter(string currencySymbol) =>
        CultureInfo = new CultureInfo("nl-NL")
        {
            NumberFormat =
            {
                CurrencySymbol = currencySymbol,
                CurrencyNegativePattern = 12
            }
        };

    protected override string LedgerHead => "Datum      | Omschrijving              | Verandering  ";
}

public static class LedgerFormatterFactory
{
    public static LedgerFormatter GetFormatter(string locale, string currency)
    {
        var currencySymbol = CurrencyFactory.GetCurrencySymbol(currency);

        return locale switch
        {
            "en-US" => new AmericanLedgerFormatter(currencySymbol),
            "nl-NL" => new DutchLedgerFormatter(currencySymbol),
            _ => throw new ArgumentException("Invalid Locale", nameof(locale))
        };
    }
}

public static class CurrencyFactory
{
    public static string GetCurrencySymbol(string currency) => currency switch
    {
        "USD" => "$",
        "EUR" => "€",
        _ => throw new ArgumentException("Invalid Currency!", nameof(currency))
    };
}