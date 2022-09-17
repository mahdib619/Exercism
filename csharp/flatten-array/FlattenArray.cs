using System.Collections;

public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        foreach (var item in input)
        {
            if (item is IEnumerable subItems)
            {
                foreach (var subItem in Flatten(subItems))
                    yield return subItem;
            }
            else if (item is not null)
            {
                yield return item;
            }
        }
    }
}