using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public interface IHaveInventory
    {
        //method
        GameObject Locate(string id);
        //property
        public string Name { get; }
    }

}
