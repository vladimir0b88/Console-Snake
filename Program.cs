

var optionsMenu = new Menu();

optionsMenu.AddMenuChoices(new MenuElement[]
{
    new MenuElement("Snake speed", () => { Console.WriteLine("1111"); Console.ReadKey(); }),
    new MenuElement("Difficulty", () => { Console.WriteLine("2222"); Console.ReadKey(); }),
    new MenuElement("Sound", () => { Console.WriteLine("3333"); Console.ReadKey(); }),
    new MenuElement("Exit", optionsMenu.Close),
});



var mainMenu = new Menu();

mainMenu.AddMenuChoices(new MenuElement[]
{
    new MenuElement("Play", GameEngine.StartGame),
    new MenuElement("Options", optionsMenu.Open),
    new MenuElement("Help", () => { Console.WriteLine("3333"); Console.ReadKey(); }),
    new MenuElement("Leaderboard", () => { Console.WriteLine("4444"); Console.ReadKey(); }),
    new MenuElement("Exit", mainMenu.Close)
});

    
mainMenu.Open();


