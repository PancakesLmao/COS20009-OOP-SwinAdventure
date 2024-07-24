using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Dictionary<Direction, Path> _paths;
        private string _desc;
        private string _name;

        public Location(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _name = name;
            _desc = desc;
            _inventory = new Inventory();
            _paths = new Dictionary<Direction, Path>();
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
        public void AddPath(Direction direction, Path path)
        {
            _paths[direction] = path;
        }

        public Path GetPath(Direction direction)
        {
            if (_paths.TryGetValue(direction, out Path path))
            {
                return path;
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
