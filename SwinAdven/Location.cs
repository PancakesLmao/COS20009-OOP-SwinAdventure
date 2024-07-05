using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private string _desc;
        private string _name;

        public Location(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _name = name;
            _desc = desc;
            _inventory = new Inventory();
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else if (_inventory != null && _inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }

            return null;
        }

        //property
        public Inventory Inventory
        {
            get { return _inventory; }
        }
    }
}
