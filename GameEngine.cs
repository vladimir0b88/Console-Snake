
using System.Diagnostics;
using System.Xml.Linq;

internal static class GameEngine
{
    private static Stopwatch _stopWatch = new();
    private static List<PickableItem> _pickableItems = new();

    private static int _eatenFoodCount = 0;

    internal static void StartGame()
    {
        ResetGame();

        var snake = new Snake(6, 10);
        Printer.PrintSnakeHead(snake);

        GenerateFood(snake);

        GenerateWallDestroyer(snake);


        while (true)
        {
            SelectSnakeDirection(snake);


            snake.AddTail();
            snake.MoveToDirection();


            if (CheckGameCompletionConditions(snake))
                break;


            PickUpItem(snake);


            if (snake.MaxTailCount <= snake.TailCount)
                snake.RemoveLastTail();
        }
    }


    static void ResetGame()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.CursorVisible = false;


        _pickableItems.Clear();
        _eatenFoodCount = 0;
        Borders.RemoveAllWalls();

        Borders.SetSize();
        Printer.PrintBorders();
    }


    static bool CheckGameCompletionConditions(Snake snake)
    {
        if (Borders.IsCrashIntoBorders(snake.XHead, snake.YHead) ||
               snake.IsCrashedIntoTail())
            return true;

        return false;
    }


    static void SelectSnakeDirection(Snake snake)
    {
        // За время SnakeSpeed в мс
        // находим первое направление, отличающееся от исходного,
        // и изменяем направление у змеи

        Snake.Directions oldDirection = snake.Direction;

        _stopWatch.Restart();

        while (_stopWatch.ElapsedMilliseconds <= GameRules.SnakeSpeed)
            if (snake.Direction == oldDirection)
            {
                Snake.Directions newDirection = ChangeSnakeDirection(snake.Direction);

                snake.SetDirection(newDirection);
            }

    }


    static Snake.Directions ChangeSnakeDirection(Snake.Directions curDirection)
    {
        if (Console.KeyAvailable)
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow: curDirection = Snake.Directions.Up; break;
                case ConsoleKey.DownArrow: curDirection = Snake.Directions.Down; break;
                case ConsoleKey.LeftArrow: curDirection = Snake.Directions.Left; break;
                case ConsoleKey.RightArrow: curDirection = Snake.Directions.Right; break;
            }

        return curDirection;
    }


    static void PickUpItem(Snake snake)
    {
        switch (_pickableItems.FirstOrDefault(item => item.HasTouchedItem(snake.XHead, snake.YHead)))
        {
            case Food food:
                {
                    _pickableItems.Remove(food);
                    _eatenFoodCount++;

                    // Увеличиваем длину хвоста
                    snake.MaxTailCount += GameRules.SnakeTailIncrement;

                    // Создаем стенки
                    if (_eatenFoodCount % GameRules.NumberOfFoodsToCreateWall == 0)
                        GenerateWalls(snake);


                    GenerateFood(snake);
                    break;
                }

            case WallDestroyer destroyer:
                {
                    _pickableItems.Remove(destroyer);

                    destroyer.Pick();

                    GenerateWallDestroyer(snake);
                    break;
                }

        }
    }



    static (int x, int y) GenerateFreePoint(Snake snake)
    {
        Random rnd = new();
        int x, y;
        do
        {
            x = 2 * rnd.Next(Borders.VerticalBorder.xLeft + 2, Borders.VerticalBorder.xRight / 2);
            y = rnd.Next(Borders.HorizontalBorder.yTop + 2, Borders.HorizontalBorder.yBottom - 1);
        }
        while (Borders.IsCrashIntoBorders(x, y) ||
                snake.IsTailPoint(x, y) ||
                _pickableItems.Any(item => item.X == x && item.Y == y));

        return (x, y);
    }


    static void GenerateFood(Snake snake)
    {
        (int x, int y) point = GenerateFreePoint(snake);

        Food food = new(point.x, point.y);

        _pickableItems.Add(food);

        Printer.PrintFood(food);
    }


    static void GenerateWalls(Snake snake)
    {
        for (int i = 0; i < GameRules.NumberOfCreateWalls; i++)
        {
            (int x, int y) point = GenerateFreePoint(snake);
            Borders.AddWall(point.x, point.y);
        }

    }


    static void GenerateWallDestroyer(Snake snake)
    {
        (int x, int y) point = GenerateFreePoint(snake);

        WallDestroyer destroyer = new(point.x, point.y);

        _pickableItems.Add(destroyer);

        Printer.PrintWallDestroyer(destroyer);
    }


}
