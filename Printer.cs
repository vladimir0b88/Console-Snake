internal class Printer
{
    private const string _airPixel = "  ";

    internal static void PrintBorders()
    {
        // Цвет границ
        var borderColor = ConsoleColor.White;

        // Верхняя и нижняя границы
        for (int i = Borders.HorizontalBorder.x0; i < Borders.HorizontalBorder.x1; i += 2)
        {
            PrintPixel(i, Borders.HorizontalBorder.yTop, borderColor);
            PrintPixel(i, Borders.HorizontalBorder.yBottom, borderColor);
        }

        // Левая и правая границы
        for (int i = Borders.VerticalBorder.y0; i < Borders.VerticalBorder.y1; i++)
        {
            PrintPixel(Borders.VerticalBorder.xLeft, i, borderColor);
            PrintPixel(Borders.VerticalBorder.xRight, i, borderColor);
        }

    }

    internal static void PrintSnakeHead(Snake snake)
    {
        var snakeColor = ConsoleColor.DarkBlue;
        PrintPixel(snake.XHead, snake.YHead, snakeColor, snake.ToString());
    }

    internal static void PrintSnakeTail(Snake.SnakeTail tail)
    {
        var tailColor = ConsoleColor.Blue;
        PrintPixel(tail.X, tail.Y, tailColor);
    }

    internal static void RemoveSnakeTail(Snake.SnakeTail tail)
    {
        var tailColor = ConsoleColor.Black;
        PrintPixel(tail.X, tail.Y, tailColor);
    }

    static void PrintPixel(int x, int y, ConsoleColor pixelColor, string pixel = _airPixel)
    {
        Console.SetCursorPosition(x, y);
        Console.BackgroundColor = pixelColor;
        Console.Write(pixel);
    }
}
