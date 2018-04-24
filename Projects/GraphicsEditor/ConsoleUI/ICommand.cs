using System.Collections.Generic;

namespace ConsoleUI
{
    public interface ICommand
    {
        string Name { get; }
        string Help { get; }
        string Description { get; }
        IEnumerable<string> Synonyms { get; }
        void Execute(params string[] parameters);
    }
}
