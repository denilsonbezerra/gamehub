using GameHub.Controllers;

Console.Title = "Game Hub";
Console.BackgroundColor = ConsoleColor.DarkGray;
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();

Hub hub = new();
hub.MainMenu();