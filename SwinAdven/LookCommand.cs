using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwinAdven
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" }) { }

        //property
        public override string Execute(Player p, string[] text)
        {
            if (text.Length == 1 && text[0] == "look")
            {
                return p.Location.FullDescription;
            }
            if (text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that";
            }
            if (text[0] != "look")
            {
                return "Error in look input";
            }
            if (text[1] != "at")
            {
                return "What do you want to look at?";
            }
            if (text.Length == 5)
            {
                if (text[3] != "in")
                {
                    return "What do you want to look in?";
                }
            }
            IHaveInventory container;
            if (text.Length == 3)
            {
                container = p;
            }
            else
            {
                string containerId = text[4];
                container = FetchContainer(p, containerId);
                if (container == null)
                {
                    return $"I can't find the {containerId}";
                }
            }

            string itemId = text[2];
            return LookAtIn(itemId, container);
        }

        private IHaveInventory FetchContainer(Player p, string containerId) 
        {
            GameObject container = p.Locate(containerId);
            return container as IHaveInventory;
        }

        public string LookAtIn(string thingId, IHaveInventory container)
        {
            GameObject FoundItem = container.Locate(thingId);
            if (FoundItem == null)
            {
                if (container is Player)
                {
                    return $"I can't find the {thingId}";
                }
                else
                {
                    return $"I can't find the {thingId} in the {container.Name}";
                }
            }
            else
            {
                return FoundItem.FullDescription;
            }
        }
    }
}
