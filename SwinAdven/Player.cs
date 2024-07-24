using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private string _desc;
        private string _name;
        private Location _location;
        private Location _oldLocation;
        public Player(string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            _desc = desc;
            _name = name;
            _inventory = new Inventory();
            _location = null;
            _oldLocation = null;
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
            else if (_location != null && _location.Locate(id) != null)
            {
                return _location.Locate(id);
            }

            return null;
        }
        //properties
        public override string FullDescription
        {
            get { return $"{_name}, {_desc} is carrying:\n{_inventory.ItemList}"; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
        }
        public void MoveTo(Location newLocation)
        {
            _oldLocation = _location;
            _location = newLocation;
        }
        //Player's location
        public Location Location 
        { 
            get { return _location; } 
            set 
            {
                _oldLocation = _location;
                _location = value; 
            }
        }
        public Location PreviousLocation()
        {
            return _oldLocation;
        }
    }
}
