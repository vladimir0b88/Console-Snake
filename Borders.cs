internal static class Borders
{
    internal static (int x0, int x1, int yTop, int yBottom) HorizontalBorder { get; private set; }

    internal static (int y0, int y1, int xLeft, int xRight) VerticalBorder { get; private set; }

    static internal void SetSize()
    {
        HorizontalBorder = (x0: 0,
                            x1: Console.WindowWidth,
                            yTop: 0,
                            yBottom: Console.WindowHeight - 1);

        VerticalBorder = (y0: 0,
                          y1: Console.WindowHeight,
                          xLeft: 0,
                          xRight: Console.WindowWidth - 2);
    }

    static internal bool CrashedIntoBorder(int xHead, int yHead)
    {
        if (yHead == HorizontalBorder.yTop ||
            yHead == HorizontalBorder.yBottom)
            return true;

        if (Math.Abs(xHead - VerticalBorder.xLeft) <= 2 ||
            Math.Abs(xHead - VerticalBorder.xRight) <= 2)
            return true;


        return false;
    }
}