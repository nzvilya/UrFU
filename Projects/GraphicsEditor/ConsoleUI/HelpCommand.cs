using System;

namespace ConsoleUI
{
    public class HelpCommand : ICommand
    {
        Application app;

        public HelpCommand(Application app)
        {
            this.app = app;
        }

        public string Name => "help";
        public string Help => "Brief help on all commands";
        public string[] Synonyms => new string[] { "?" };
        public string Description => "Displays a list of commands with short help";

        public void Execute(params string[] parameters)
        {
            foreach (ICommand cmd in app.Commands)
            {
                Console.WriteLine($"{cmd.Name}: {cmd.Help}");
            }
        }
    }
}
