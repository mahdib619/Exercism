using System;
using System.Collections.Generic;
using System.Linq;

public enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

public static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2) where T : IComparable
    {
        if (list1.Count == list2.Count)
            return CheckEquality(list1, list2);

        return list1.Count > list2.Count ? CheckSub(list1, list2, SublistType.Superlist) : CheckSub(list2, list1, SublistType.Sublist);
    }

    private static SublistType CheckEquality<T>(List<T> list1, List<T> list2) where T : IComparable
    {
        for (var i = 0; i < list1.Count; i++)
        {
            if (list1[i].CompareTo(list2[i]) != 0)
                return SublistType.Unequal;
        }

        return SublistType.Equal;
    }

    private static SublistType CheckSub<T>(List<T> sup, List<T> sub, SublistType expected) where T : IComparable
    {
        var hs1 = string.Join(',', sup.Select(s => s is null ? "#" : s.GetHashCode().ToString()));
        var hs2 = string.Join(',', sub.Select(s => s is null ? "#" : s.GetHashCode().ToString()));

        return hs1.Contains(hs2) ? expected : SublistType.Unequal;
    }
}