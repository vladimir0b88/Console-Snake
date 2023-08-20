
internal abstract class PickableItem
{
    internal int X { get; }
    internal int Y { get; }

    internal PickableItem(int x, int y)
    {
        X = x;
        Y = y;
    }

    internal virtual bool HasTouchedItem(int x, int y)
    {
        if (X == x && Y == y)
            return true;

        return false;
    }

    internal abstract void Pick();
}


internal class Food : PickableItem
{
    internal Food(int x, int y) : base(x, y) { }

    internal override void Pick()
    {
        Beep();
    }

    void Beep()
    {
        Task.Run(() => Console.Beep(1500, 300));
    }
}

internal class WallDestroyer : PickableItem
{
    internal WallDestroyer(int x, int y) : base(x, y) { }
    internal override void Pick()
    {
        for (int i = 0; i < GameRules.RemoveWallsAmount; i++)
            Borders.RemoveFirstWall();

        Beep();
    }

    void Beep()
    {
        Task.Run(() =>
        {
            Console.Beep(700, 120);
            Console.Beep(900, 120);
            Console.Beep(850, 150);
        });
    }
}