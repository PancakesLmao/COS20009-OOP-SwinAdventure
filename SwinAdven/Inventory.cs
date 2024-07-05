using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class Inventory 
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        //methods
        public bool HasItem(string id)
        {
            foreach (var item in _items)
            {
                if (item.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }
        public void Put(Item item)
        {
            _items.Add(item);
        }
        public Item Take(string id) 
        {
            Item item = Fetch(id);
            if (item == null)
            {
                return null;
            }
            else 
            {
                _items.Remove(item);
            }
            return item;
        }
        public Item Fetch(string id) 
        {
            //loop through an entire List
            foreach (var item in _items)
            {
                if (item.AreYou(id))
                {
                    return item;
                }
            }

            return null;
        }

        //property
        public string ItemList
        {
            get 
            {
                string ListOfItem = "";
                foreach(var item in _items)
                {
                    //4.2P mistake: ItemList of Inventory contain item's short description
                    ListOfItem += "    " + item.ShortDescription + "\n";
                }
                return ListOfItem;
            }
        }
    }
}
