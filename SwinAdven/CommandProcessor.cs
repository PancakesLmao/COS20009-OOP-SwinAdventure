using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class CommandProcessor
    {
        private List<Command> _commands;

        public CommandProcessor()
        {
            _commands = new List<Command>();
        }
        public void AddCommand(Command command)
        {
            _commands.Add(command);
        }

        public string Process(Player player, string[] text)
        {
            string cmdWords = text[0].ToLower();
            foreach (Command cmd in _commands)
            {
                if (cmd.AreYou(cmdWords))
                {
                    return cmd.Execute(player, text);
                }
            }
            return "I don't understand " + cmdWords;
        }
    }
}
