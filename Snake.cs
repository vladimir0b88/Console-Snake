
internal class Snake
{
    internal int XHead { get; private set; }
    internal int YHead { get; private set; }
    internal Directions Direction { get; private set; }

    internal enum Directions
    {
        Left,
        Right,
        Up,
        Down,
    }

    private Queue<SnakeTail> _snakeTails = new();

    internal int TailCount { get { return _snakeTails.Count; } }

    internal int MaxTailCount { get; set; } = 5;

    internal Snake(int xHead, int yHead, Directions direction = Directions.Right)
    {
        XHead = xHead;
        YHead = yHead;
        Direction = direction;
    }

    internal void MoveToDirection()
    {
        switch (Direction)
        {
            case Directions.Left: XHead -= 2; break;
            case Directions.Right: XHead += 2; break;

            case Directions.Up: YHead -= 1; break;
            case Directions.Down: YHead += 1; break;
        }

        Printer.PrintSnakeHead(this);
    }

    internal void SetDirection(Directions direction)
    {
        switch (direction)
        {
            case Directions.Left when Direction != Directions.Right: Direction = Directions.Left; break;
            case Directions.Right when Direction != Directions.Left: Direction = Directions.Right; break;

            case Directions.Up when Direction != Directions.Down: Direction = Directions.Up; break;
            case Directions.Down when Direction != Directions.Up: Direction = Directions.Down; break;
        }
    }

    internal void AddTail()
    {
        var newTail = new SnakeTail(XHead, YHead);

        _snakeTails.Enqueue(newTail);


        Printer.PrintSnakeTail(newTail);
    }

    internal void RemoveLastTail()
    {
        if (_snakeTails.Count > 0)
        {
            SnakeTail removedTail = _snakeTails.Dequeue();

            Printer.RemovePixel(removedTail.X, removedTail.Y);
        }
    }

    internal bool IsCrashedIntoTail()
    {
        if (_snakeTails.Any(tail => tail.X == XHead && tail.Y == YHead))
            return true;

        return false;
    }

    internal bool IsTailPoint(int x, int y)
    {
        if (x == XHead && y == YHead)
            return true;

        if (_snakeTails.Any(tail => tail.X == x && tail.Y == y))
            return true;

        return false;
    }

    public override string ToString() => Direction switch
    {
        Directions.Up => @"/\",
        Directions.Down => @"\/",
        Directions.Left => "< ",
        Directions.Right => " >",
        _ => throw new()
    };


    internal readonly record struct SnakeTail(int X, int Y);
}
