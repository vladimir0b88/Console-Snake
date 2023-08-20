

var optionsMenu = new Menu();

optionsMenu.AddMenuChoices(new MenuElement[]
{
    new MenuElement("Snake speed", () => { Console.WriteLine("1111"); Console.ReadKey(); }),
    new MenuElement("Difficulty", () => { Console.WriteLine("2222"); Console.ReadKey(); }),
    new MenuElement("Wall destroyer", () => { Console.WriteLine("3333"); Console.ReadKey(); }),
    new MenuElement("Exit", optionsMenu.Close),
});



var mainMenu = new Menu();

mainMenu.AddMenuChoices(new MenuElement[]{
    new MenuElement("play", GameEngine.StartGame),
    new MenuElement("options", optionsMenu.Open),
    new MenuElement("help", () => { Console.WriteLine("3333"); Console.ReadKey(); }),
    new MenuElement("leaderboard", () => { Console.WriteLine("4444"); Console.ReadKey(); }),
    new MenuElement("exit", mainMenu.Close)
});


mainMenu.Open();


