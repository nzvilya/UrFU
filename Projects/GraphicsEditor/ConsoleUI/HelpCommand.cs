using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class HelpCommand : ICommand
    {
        private readonly Application _app;

        public HelpCommand(Application app)
        {
            _app = app;
        }

        public string Name => "help";
        public string Help => "Brief help on all commands";
        public IEnumerable<string> Synonyms => new[] { "?" };
        public string Description => "Displays a list of commands with short help";

        public void Execute(params string[] parameters)
        {
            foreach (var cmd in _app.Commands)
            {
                Console.WriteLine($"{cmd.Name}: {cmd.Help}");
            }
        }
    }
}
