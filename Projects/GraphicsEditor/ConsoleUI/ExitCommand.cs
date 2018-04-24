namespace ConsoleUI
{
    public class ExitCommand : ICommand
    {
        Application app;

        public ExitCommand(Application app)
        {
            this.app = app;
        }

        public string Name => "exit";
        public string Help => "Exit the program";
        public string[] Synonyms => new string[] { "quit", "bye" };
        public string Description => "Exit the program";
        
        public void Execute(params string[] parameters)
        {
            app.Exit();
        }
    }
}
