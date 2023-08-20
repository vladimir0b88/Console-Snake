using System.Xml.Linq;

internal static class Borders
{
    internal static (int x0, int x1, int yTop, int yBottom) HorizontalBorder { get; private set; }

    internal static (int y0, int y1, int xLeft, int xRight) VerticalBorder { get; private set; }

    private static List<Wall> _walls = new();

    internal static int WallsCount
    {
        get { return _walls.Count; }
    }

    private static int _rightMargin = 20;

    static internal void SetSize()
    {
        HorizontalBorder = (x0: 0,
                            x1: Round(Console.WindowWidth) - _rightMargin,
                            yTop: 0,
                            yBottom: Console.WindowHeight - 1);

        VerticalBorder = (y0: 0,
                          y1: Console.WindowHeight,
                          xLeft: 0,
                          xRight: Round(Console.WindowWidth) - _rightMargin);

        int Round(int num) => num - num % 2;
    }

    static internal bool IsCrashIntoBorders(int xHead, int yHead)
    {
        if (xHead == VerticalBorder.xLeft ||
            xHead == VerticalBorder.xRight)
            return true;

        if (yHead == HorizontalBorder.yTop ||
            yHead == HorizontalBorder.yBottom)
            return true;


        if (_walls.Any(wall => wall.X == xHead && wall.Y == yHead))
            return true;

        return false;
    }

    static internal void AddWall(int x, int y)
    {
        Wall wall = new(x, y);

        _walls.Add(wall);

        Printer.PrintWall(wall);
    }

    static internal void RemoveFirstWall()
    {
        if(_walls.Count <= 0)
            return;


        Wall removedWall = _walls.First();

        _walls.Remove(removedWall);

        Printer.RemovePixel(removedWall.X, removedWall.Y);
    }

    static internal void RemoveAllWalls()
    {
        _walls.Clear();
    }

    internal readonly record struct Wall(int X, int Y);
}

