internal class Printer
{
    private const string _airPixel = "  ";

    internal static void PrintBorders()
    {
        // Цвет границ
        var wallColor = ConsoleColor.White;

        // Верхняя и нижняя границы
        for (int i = Borders.HorizontalBorder.x0; i < Borders.HorizontalBorder.x1; i += 2)
        {
            PrintPixel(i, Borders.HorizontalBorder.yTop, wallColor);
            PrintPixel(i, Borders.HorizontalBorder.yBottom, wallColor);
        }

        // Левая и правая границы
        for (int i = Borders.VerticalBorder.y0; i < Borders.VerticalBorder.y1; i++)
        {
            PrintPixel(Borders.VerticalBorder.xLeft, i, wallColor);
            PrintPixel(Borders.VerticalBorder.xRight, i, wallColor);
        }

    }

    internal static void PrintWall(Borders.Wall wall)
    {
        var wallColor = ConsoleColor.DarkGray;

        PrintPixel(wall.X, wall.Y, wallColor);
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

    internal static void PrintFood(Food food)
    {
        var foodColor = ConsoleColor.DarkRed;

        PrintPixel(food.X, food.Y, foodColor);
    }

    internal static void PrintWallDestroyer(WallDestroyer wallDestroyer)
    {
        var destoyerColor = ConsoleColor.Green;

        PrintPixel(wallDestroyer.X, wallDestroyer.Y, destoyerColor);
    }

    internal static void RemovePixel(int x, int y)
    {
        var voidColor = ConsoleColor.Black;

        PrintPixel(x, y, voidColor);
    }

    static void PrintPixel(int x, int y, ConsoleColor pixelColor, string pixel = _airPixel)
    {
        Console.SetCursorPosition(x, y);
        Console.BackgroundColor = pixelColor;
        Console.Write(pixel);
    }
}
