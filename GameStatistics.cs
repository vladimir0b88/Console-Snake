
internal class GameStatistics
{
    private int _score = 0;
    internal int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            RefreshStatistics();
        }
    }

    private int _eatenFood = 0;
    internal int EatenFood
    {
        get { return _eatenFood; }
        set
        {
            _eatenFood = value;
            RefreshStatistics();
        }
    }

    private int _removedWalls = 0;
    internal int RemovedWalls
    {
        get { return _removedWalls; }
        set
        {
            _removedWalls = value;
            RefreshStatistics();
        }
    }

    private int _tailLenght = 0;
    internal int TailLenght
    {
        get { return _tailLenght; }
        set
        {
            _tailLenght = value;
            RefreshStatistics();
        }
    }

    internal void RefreshStatistics()
    {
        Printer.PrintStatistics(this);
    }

    internal List<string> GetStatistics()
    {
        var list = new List<string>();

        list.Add($"Score: {Score}");
        list.Add($"Food: {EatenFood}");
        list.Add($"Removed walls: {RemovedWalls}");
        list.Add($"Tail lenght: {TailLenght}");

        return list;
    }
}