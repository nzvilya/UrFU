using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleUI
{
    public class Application
    {
        NotFoundCommand notFounded = new NotFoundCommand();
        List<ICommand> commands = new List<ICommand>();
        Dictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();
        bool keepRunning = true;

        public IList<ICommand> Commands => commands;

        public void Exit()
        {
            keepRunning = false;
        }

        public ICommand FindCommand(string name)
        {
            if (commandMap.ContainsKey(name))
            {
                return commandMap[name];
            }
            notFounded.Name = name;
            return notFounded;
        }

        public void AddCommand(ICommand cmd)
        {
            commands.Add(cmd);
            if (commandMap.ContainsKey(cmd.Name))
            {
                throw new Exception(String.Format($"Commands {cmd.Name} has already been added"));
            }
            commandMap.Add(cmd.Name, cmd);

            foreach (var synonym in cmd.Synonyms)
            {
                if (commandMap.ContainsKey(synonym))
                {
                    Console.WriteLine($"ERROR: Ignores the synonym {synonym} for the {cmd.Name} " +
                        $"command because the name {synonym} has already been used");
                }
                commandMap.Add(synonym, cmd);
            }
        }

        public void Run(TextReader reader)
        {
            string[] cmdLine, parameters;
            while (keepRunning)
            {
                Console.Write("> ");
                var cmd = reader.ReadLine();
                if (cmd == null)
                {
                    break;
                }
                cmdLine = cmd.Split(
                    new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries
                );
                if (cmdLine.Length == 0)
                {
                    continue;
                }

                parameters = new string[cmdLine.Length - 1];
                Array.Copy(cmdLine, 1, parameters, 0, cmdLine.Length - 1);
                FindCommand(cmdLine[0]).Execute(parameters);
            }
        }
    }
}
