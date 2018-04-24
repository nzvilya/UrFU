using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class ExplainCommand : ICommand
    {
        private readonly Application _app;

        public ExplainCommand(Application app)
        {
            _app = app;
        }

        public string Name => "explain";
        public string Help => "Information command";
        public IEnumerable<string> Synonyms => new[] { "elaborate" };
        public string Description =>"Displays all available information on the command or commands." +
                                    "Command names are passed as parameters";

        public void Execute(params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var cmd = _app.FindCommand(parameter);
                var synonyms = new List<string>(cmd.Synonyms);
                if (cmd.Name == parameter)
                {
                    Console.WriteLine($"{cmd.Name}: {cmd.Help}");
                }
                else
                {
                    Console.WriteLine($"{parameter}: {cmd.Help}");
                    synonyms.Remove(parameter);
                    synonyms.Add(cmd.Name);
                }

                if (synonyms.Count > 0)
                {
                    Console.WriteLine($"Synonyms: {string.Join(", ", synonyms)}");
                }

                if (cmd.Description != string.Empty)
                {
                    Console.WriteLine(cmd.Description);
                }
            }
        }
    }
}
