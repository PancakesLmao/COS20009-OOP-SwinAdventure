using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base(new string[] { "quit" }) { }

        public override string Execute(Player p, string[] text)
        {
            if (AreYou(text[0]))
            {
                return "Bye.";
            }
            return "Invalid command.";
        }
    }
}
