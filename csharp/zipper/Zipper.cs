public class BinTree
{
    public BinTree(int value, BinTree left, BinTree right) => (Value, Left, Right) = (value, left, right);

    public int Value { get; set; }
    public BinTree Left { get; set; }
    public BinTree Right { get; set; }

    public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is BinTree other && Value == other.Value && Equals(Left, other.Left) && Equals(Right, other.Right);

    private static bool Equals(BinTree left, BinTree right) => ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
}

public class Zipper
{
    private readonly BinTree _root;
    private readonly BinTree _focus;
    private readonly Zipper _top;

    private Zipper(BinTree focus) => _root = _focus = focus;
    private Zipper(BinTree root, BinTree focus, Zipper top) => (_root, _focus, _top) = (root, focus, top);

    public int Value() => _focus.Value;

    public Zipper SetValue(int newValue)
    {
        _focus.Value = newValue;
        return this;
    }

    public Zipper SetLeft(BinTree binTree)
    {
        _focus.Left = binTree;
        return this;
    }

    public Zipper SetRight(BinTree binTree)
    {
        _focus.Right = binTree;
        return this;
    }

    public Zipper Left() => _focus.Left is null ? null : new(_root, _focus.Left, this);

    public Zipper Right() => _focus.Right is null ? null : new(_root, _focus.Right, this);

    public Zipper Up() => _top is null ? null : new(_root, _top._focus, _top._top);

    public BinTree ToTree() => _root;

    public static Zipper FromTree(BinTree tree) => tree is null ? null : new(tree);

    public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is Zipper other && other._focus.Equals(_focus));
}