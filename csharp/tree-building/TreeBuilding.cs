using System;
using System.Collections.Generic;

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        var sorted = new SortedSet<TreeBuildingRecord>(records, new TreeComparer());

        var root = ValidateRecordsAndGetRoot(sorted);
        var trees = new Dictionary<int, Tree> { [root.Id] = root };

        foreach (var record in sorted)
        {
            if (!trees.TryGetValue(record.ParentId, out var parent))
                throw new ArgumentException("Parent id should be higher than child!", nameof(records));

            trees[record.RecordId] = parent.AddChild(record.RecordId);
        }

        return root;
    }

    private static Tree ValidateRecordsAndGetRoot(SortedSet<TreeBuildingRecord> records)
    {
        var rootRecord = records.Min;

        if (rootRecord is null)
            throw new ArgumentException("Empty Input!", nameof(records));
        if (records.Max.RecordId >= records.Count)
            throw new ArgumentException("Maximum id should be less than records count!", nameof(records));
        if (rootRecord.RecordId is not 0 || rootRecord.RecordId != rootRecord.ParentId)
            throw new ArgumentException("Invalid root!", nameof(records));

        records.Remove(rootRecord);
        return new Tree { Id = rootRecord.RecordId, ParentId = rootRecord.RecordId };
    }
}

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Tree> Children { get; } = new();
    public bool IsLeaf => Children.Count == 0;

    public Tree AddChild(int id)
    {
        var child = new Tree { Id = id };
        Children.Add(child);
        return child;
    }
}

public class TreeComparer : IComparer<TreeBuildingRecord>
{
    public int Compare(TreeBuildingRecord x, TreeBuildingRecord y) => x.RecordId.CompareTo(y.RecordId);
}