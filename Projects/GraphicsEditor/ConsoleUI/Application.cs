using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleUI
{
    public class Application
    {
        private readonly NotFoundCommand _notFounded = new NotFoundCommand();
        private readonly List<ICommand> _commands = new List<ICommand>();
        private readonly Dictionary<string, ICommand> _commandMap = new Dictionary<string, ICommand>();
        private bool _keepRunning = true;

        public IEnumerable<ICommand> Commands => _commands;

        public void Exit()
        {
            _keepRunning = false;
        }

        public ICommand FindCommand(string name)
        {
            if (_commandMap.ContainsKey(name))
            {
                return _commandMap[name];
            }
            _notFounded.Name = name;
            return _notFounded;
        }

        public void AddCommand(ICommand cmd)
        {
            _commands.Add(cmd);
            if (_commandMap.ContainsKey(cmd.Name))
            {
                throw new Exception(string.Format($"Commands {cmd.Name} has already been added"));
            }
            _commandMap.Add(cmd.Name, cmd);

            foreach (var synonym in cmd.Synonyms)
            {
                if (_commandMap.ContainsKey(synonym))
                {
                    Console.WriteLine($"ERROR: Ignores the synonym {synonym} for the {cmd.Name} " +
                        $"command because the name {synonym} has already been used");
                }
                _commandMap.Add(synonym, cmd);
            }
        }

        public void Run(TextReader reader)
        {
            while (_keepRunning)
            {
                Console.Write("> ");
                var cmd = reader.ReadLine();
                if (cmd == null)
                {
                    break;
                }
                var cmdLine = cmd.Split(
                    new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries
                );
                if (cmdLine.Length == 0)
                {
                    continue;
                }

                var parameters = new string[cmdLine.Length - 1];
                Array.Copy(cmdLine, 1, parameters, 0, cmdLine.Length - 1);
                FindCommand(cmdLine[0]).Execute(parameters);
            }
        }
    }
}
