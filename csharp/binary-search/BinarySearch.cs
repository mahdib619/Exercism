public static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        int min = 0, max = input.Length - 1;

        while (min <= max)
        {
            var mid = (min + max) >> 1;

            if (value > input[mid])
                min = mid + 1;
            else if (value < input[mid])
                max = mid - 1;
            else
                return mid;
        }

        return -1;
    }
}