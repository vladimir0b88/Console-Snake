
internal class GameStatistics
{
    internal int Score { get; set; } = 0;

    internal int EatenFood { get; set; } = 0;
    internal int RemovedWalls { get; set; } = 0;

    internal int TailLenght { get; set; } = 0;

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