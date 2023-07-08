﻿

internal static class GameRules
{
    // Время за которое змея преодолевает один пиксель в мс
    internal const int SnakeSpeed = 100;

    // Кол-во добавляемых хвостов после еды
    internal const int SnakeTailIncrement = 31;

    // Кол-во еды для создания стенок
    internal const int NumberOfFoodsToCreateWall = 1;

    // Кол-во создаваемых стенок
    internal const int NumberOfCreateWalls = 15;

    // Кол-во удаляемых стен WallDestroyer
    internal const int NumberOfRemoveWalls = 5;

}
