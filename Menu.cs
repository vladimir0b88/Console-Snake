
class Menu
{
    private int _selectedElementIndex = 0;
    private List<MenuElement> _menuElements = new();
    private bool _isActiveMenu = true;

    enum MenuAction
    {
        Up,
        Down,
        Enter,
        Exit
    }


    internal void AddMenuChoices(params MenuElement[] menuElements)
    {
        foreach (MenuElement element in menuElements)
            _menuElements.Add(element);
    }

    internal void Open()
    {
        _isActiveMenu = true;

        while (_isActiveMenu)
        {
            Show();

            switch (ReadAction())
            {
                case MenuAction.Up:
                    if (_selectedElementIndex > 0)
                        _selectedElementIndex--; break;

                case MenuAction.Down:
                    if (_selectedElementIndex < _menuElements.Count - 1)
                        _selectedElementIndex++; break;

                case MenuAction.Enter:
                    _menuElements[_selectedElementIndex].Run(); break;

                case MenuAction.Exit:
                    Close(); break;
            }
        }

        Printer.ClearConsole();
    }

    internal void Close() => _isActiveMenu = false;

    void Show()
    {
        Printer.ClearConsole();

        for (int i = 0; i < _menuElements.Count; i++)
            if (i == _selectedElementIndex)
                Printer.PrintMenuElement(_menuElements[i], true);
            else
                Printer.PrintMenuElement(_menuElements[i], false);

    }

    MenuAction ReadAction()
    {
        MenuAction? action = null;

        while (action is null)
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.W:
                    action = MenuAction.Up; break;

                case ConsoleKey.DownArrow or ConsoleKey.S:
                    action = MenuAction.Down; break;

                case ConsoleKey.RightArrow or ConsoleKey.Enter or ConsoleKey.D:
                    action = MenuAction.Enter; break;

                case ConsoleKey.LeftArrow or ConsoleKey.Escape or ConsoleKey.A:
                    action = MenuAction.Exit; break;
            }

        return (MenuAction)action;
    }

}



class MenuElement
{
    internal string Title { get; private set; }
    private Action _menuAction;

    internal MenuElement(string title, Action action)
    {
        Title = title;
        _menuAction = action;
    }

    internal void Run() => _menuAction();

    public override string ToString()
    {
        return Title;
    }
}

