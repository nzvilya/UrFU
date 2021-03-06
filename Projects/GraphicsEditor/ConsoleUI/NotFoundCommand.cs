﻿using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class NotFoundCommand : ICommand
    {
        public string Name { get; set; }
        public string Help => "Command not found";
        public IEnumerable<string> Synonyms => new string[] { };
        public string Description => " ";

        public void Execute(params string[] parameters)
        {
            Console.WriteLine($"Command {Name} not found");
        }
    }
}
 