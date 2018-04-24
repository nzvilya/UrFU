using System;
using ConsoleUI;
using DrawablesUI;

namespace GraphicsEditor
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var picture = new Picture();
            var ui = new DrawableGui(picture);
            var app = new Application();
           
            app.AddCommand(new ExitCommand(app));
            app.AddCommand(new ExplainCommand(app));
            app.AddCommand(new HelpCommand(app));

            picture.Changed += ui.Refresh;
            ui.Start();
            app.Run(Console.In);
            ui.Stop();
        }
    }
}
