using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }
        //methods
        public GameObject Locate(string id)
        {
            if (AreYou(id))
                return this;

            return _inventory.Fetch(id);
        }

        //Inventory property
        public Inventory Inventory { get { return _inventory; } }

        //FullDesc property
        public override string FullDescription
        {
            get
            {
                return $"{Name} consist of:\n{_inventory.ItemList}";
            }
        }
    }
}
