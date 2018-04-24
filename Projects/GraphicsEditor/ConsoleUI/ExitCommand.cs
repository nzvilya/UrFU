using System.Collections.Generic;

namespace ConsoleUI
{
    public class ExitCommand : ICommand
    {
        private readonly Application _app;

        public ExitCommand(Application app)
        {
            _app = app;
        }

        public string Name => "exit";
        public string Help => "Exit the program";
        public IEnumerable<string> Synonyms => new[] { "quit", "bye" };
        public string Description => "Exit the program";
        
        public void Execute(params string[] parameters)
        {
            _app.Exit();
        }
    }
}
