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

    internal static void PrintStatistics(GameStatistics stat)
    {
        int x = Borders.HorizontalBorder.x1 + 3;
        int y = Borders.VerticalBorder.y0 + 2;

        Console.BackgroundColor = ConsoleColor.Black;

        foreach (string str in stat.GetStatistics())
        {
            Console.SetCursorPosition(x, y++);
            Console.Write(str);
        }
    }

    internal static void PrintWall(Borders.Wall wall)
    {
        var wallColor = ConsoleColor.DarkGray;

        PrintPixel(wall.X, wall.Y, wallColor);
    }

    internal static void PrintSnakeHead(Snake snake)
    {
        var snakeColor = ConsoleColor.DarkGreen;

        PrintPixel(snake.XHead, snake.YHead, snakeColor, snake.ToString());
    }

    internal static void PrintSnakeTail(Snake.SnakeTail tail)
    {
        var tailColor = ConsoleColor.Green;

        PrintPixel(tail.X, tail.Y, tailColor);
    }

    internal static void PrintFood(Food food)
    {
        var foodColor = ConsoleColor.DarkRed;

        PrintPixel(food.X, food.Y, foodColor);
    }

    internal static void PrintWallDestroyer(WallDestroyer wallDestroyer)
    {
        var destoyerColor = ConsoleColor.Blue;

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

    internal static void ClearConsole()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.CursorVisible = false;
    }

    internal static void PrintMenuElement(MenuElement element, bool isSelectedElement)
    {
        if (isSelectedElement)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.WriteLine(element);
    }

    internal static void PrintGameOver()
    {
        string text = "Game over";
        int x = (Borders.HorizontalBorder.x1 - (Borders.HorizontalBorder.x0 + text.Length)) / 2;
        int y = (Borders.VerticalBorder.y1 - Borders.VerticalBorder.y0) / 2;

        Console.SetCursorPosition(x, y);

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(text);
    }
}
