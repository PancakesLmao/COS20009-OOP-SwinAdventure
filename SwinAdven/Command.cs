using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public abstract class Command : IdentifiableObj
    {
        public Command(string[] ids) : base(ids) { }

        public abstract string Execute(Player p, string[] text);

    }
}
