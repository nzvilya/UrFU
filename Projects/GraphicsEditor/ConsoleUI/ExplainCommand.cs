using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class ExplainCommand : ICommand
    {
        Application app;

        public ExplainCommand(Application app)
        {
            this.app = app;
        }

        public string Name => "explain";
        public string Help => "Information command";
        public string[] Synonyms => new string[] { "elaborate" };
        public string Description =>"Displays all available information on the command or commands.Command names are passed as parameters  ";

        public void Execute(params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                ICommand cmd = app.FindCommand(parameter);
                List<string> synonyms = new List<string>(cmd.Synonyms);
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
