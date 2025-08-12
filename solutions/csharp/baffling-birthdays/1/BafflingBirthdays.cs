public static class BafflingBirthdays
{
    public static DateOnly[] RandomBirthdates(int numberOfBirthdays) => Enumerable.Range(0, numberOfBirthdays).Select(_ =>
    {
        var year = GetNonLeapRandomYear();
        var month = Random.Shared.Next(1, 13);
        return new DateOnly(year, month, Random.Shared.Next(1, DateTime.DaysInMonth(year, month) + 1));
    }).ToArray();

    public static bool SharedBirthday(DateOnly[] birthdays)
    {
        var birthDaysSet = new HashSet<DateOnly>(new BirthdayEqualityComparer());
        return birthdays.Any(birthday => !birthDaysSet.Add(birthday));
    }

    public static double EstimatedProbabilityOfSharedBirthday(int numberOfBirthdays) => (1 - (Math.Pow(364 / 365f, numberOfBirthdays * (numberOfBirthdays - 1) / 2f))) * 100;

    private static int GetNonLeapRandomYear()
    {
        int year;

        do
        {
            year = Random.Shared.Next(1900, 2025);
        } while (DateTime.IsLeapYear(year));

        return year;
    }
    
    private class BirthdayEqualityComparer : IEqualityComparer<DateOnly>
    {
        public bool Equals(DateOnly x, DateOnly y) => (x.Month, x.Day) == (y.Month, y.Day);
        public int GetHashCode(DateOnly obj) => (obj.Month, obj.Day).GetHashCode();
    }
}