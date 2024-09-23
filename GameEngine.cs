
using System.Diagnostics;
using System.Xml.Linq;

internal static class GameEngine
{
    private static Stopwatch _stopWatch = new();

    private static List<PickableItem> _pickableItems = new();

    private static GameStatistics _statistics = new();

    private static Snake _snake = new(6, 10);

    internal static void StartGame()
    {
        ResetGame();


        GenerateFood();
        GenerateWallDestroyer();


        while (true)
        {
            SelectSnakeDirection();


            _snake.AddTail();
            _snake.MoveToDirection();


            if (CheckGameCompletionConditions())
            {
                Printer.PrintGameOver();
                Console.ReadKey();
                break;
            }    
                


            PickUpItem();


            if (_snake.MaxTailLength <= _snake.TailCount)
                _snake.RemoveLastTail();
        }

    }


    static void ResetGame()
    {
        Printer.ClearConsole();


        _pickableItems = new List<PickableItem>();


        _snake = new Snake(6, 10);
        Printer.PrintSnakeHead(_snake);


        Borders.SetSize();
        Borders.RemoveAllWalls();
        Printer.PrintBorders();


        _statistics = new GameStatistics();
        _statistics.RefreshStatistics();
    }


    static bool CheckGameCompletionConditions()
    {
        if (Borders.IsCrashIntoBorders(_snake.XHead, _snake.YHead) ||
               _snake.IsCrashedIntoTail())
            return true;

        return false;
    }


    static void SelectSnakeDirection()
    {
        // За время SnakeSpeed в мс
        // находим первое направление, отличающееся от исходного,
        // и изменяем направление у змеи

        Snake.Directions oldDirection = _snake.Direction;

        _stopWatch.Restart();

        while (_stopWatch.ElapsedMilliseconds <= GameRules.SnakeSpeed)
            if (_snake.Direction == oldDirection)
            {
                Snake.Directions newDirection = ChangeSnakeDirection(_snake.Direction);

                _snake.SetDirection(newDirection);
            }

    }


    static Snake.Directions ChangeSnakeDirection(Snake.Directions curDirection)
    {
        if (Console.KeyAvailable)
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.W: curDirection = Snake.Directions.Up; break;
                case ConsoleKey.DownArrow or ConsoleKey.S: curDirection = Snake.Directions.Down; break;
                case ConsoleKey.LeftArrow or ConsoleKey.A: curDirection = Snake.Directions.Left; break;
                case ConsoleKey.RightArrow or ConsoleKey.D: curDirection = Snake.Directions.Right; break;
            }

        return curDirection;
    }


    static void PickUpItem()
    {
        PickableItem? item = _pickableItems.FirstOrDefault(item => item.HasTouchedItem(_snake.XHead, _snake.YHead));
        switch (item)
        {
            case Food food:
                {
                    _pickableItems.Remove(food);
                    _statistics.EatenFood++;

                    food.Pick();

                    // Увеличиваем длину хвоста
                    _snake.MaxTailLength += GameRules.SnakeTailIncrement;
                    _statistics.TailLenght = _snake.MaxTailLength;

                    // Создаем стенки
                    if (_statistics.EatenFood % GameRules.CountOfFoodsToCreateWall == 0)
                        GenerateWalls();

                    _statistics.Score += 10 + (int)(_statistics.TailLenght * 0.20);

                    GenerateFood();
                    break;
                }

            case WallDestroyer destroyer:
                {
                    _pickableItems.Remove(destroyer);

                    int oldWallsCount = Borders.WallsCount;

                    destroyer.Pick();

                    if (oldWallsCount != Borders.WallsCount)
                    {
                        _statistics.RemovedWalls += oldWallsCount - Borders.WallsCount;
                        _statistics.Score += (int)(_statistics.RemovedWalls * 0.20);
                    }

                    GenerateWallDestroyer();
                    break;
                }

        }
    }



    static (int x, int y) GenerateFreePoint()
    {
        Random random = new();
        int x, y;
        do
        {
            int x0 = Borders.VerticalBorder.xLeft + 2;
            int x1 = Borders.VerticalBorder.xRight / 2;
            x = 2 * random.Next(x0, x1);

            int y0 = Borders.HorizontalBorder.yTop + 2;
            int y1 = Borders.HorizontalBorder.yBottom - 1;
            y = random.Next(y0, y1);
        }
        while (Borders.IsCrashIntoBorders(x, y) ||
                _snake.IsTailPoint(x, y) ||
                _pickableItems.Any(item => item.X == x && item.Y == y));

        return (x, y);
    }


    static void GenerateFood()
    {
        (int x, int y) point = GenerateFreePoint();

        Food food = new(point.x, point.y);

        _pickableItems.Add(food);

        Printer.PrintFood(food);
    }


    static void GenerateWalls()
    {
        for (int i = 0; i < GameRules.CreateWallsAmount; i++)
        {
            (int x, int y) point = GenerateFreePoint();

            Borders.AddWall(point.x, point.y);
        }

    }


    static void GenerateWallDestroyer()
    {
        (int x, int y) point = GenerateFreePoint();

        WallDestroyer destroyer = new(point.x, point.y);

        _pickableItems.Add(destroyer);

        Printer.PrintWallDestroyer(destroyer);
    }


}
