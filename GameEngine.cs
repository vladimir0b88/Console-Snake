
using System.Diagnostics;
using System.Xml.Linq;

internal static class GameEngine
{
    private static Stopwatch _stopWatch = new();
    internal static void StartGame()
    {
        Console.Clear();
        Console.CursorVisible = false;


        Borders.SetSize();

        Printer.PrintBorders();

        var snake = new Snake(6, 10);
        Printer.PrintSnakeHead(snake);


        
        while (true)
        {
            SelectSnakeDirection(snake);


            Printer.PrintSnakeTail(snake.AddTail());

            if (snake.TailCount > 50)
                Printer.RemoveSnakeTail(snake.RemoveLastTail());


            snake.MoveToDirection();
            Printer.PrintSnakeHead(snake);

            if (Borders.CrashedIntoBorder(snake.XHead, snake.YHead))
                break;
        }


    }

    static void SelectSnakeDirection(Snake snake)
    {
        Snake.Directions oldDirection = snake.Direction;

        _stopWatch.Restart();

        // За время SnakeSpeed в мс
        // находим первое направление, отличающееся от исходного,
        // и изменяем направление у змеи
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
}
