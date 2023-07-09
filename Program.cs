
Console.ReadKey();
while (true)
{
    GameEngine.StartGame();

    // Рестарт игры после нажатия клавиши
    Console.ReadKey();
    Thread.Sleep(500);
}
