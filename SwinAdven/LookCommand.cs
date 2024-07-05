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
            //2nd condition
            if (text[0] != "look")
            {
                return "Error in look input";
            }
            //3rd condition 
            if (text.Length > 1 && text[1] != "at")
            {
                return "What do you look at?";
            }
            //check array Length
            if (text.Length == 3 || text.Length == 5)
            {
                //4th condition
                if (text.Length == 5 && text[3] != "in")
                {
                    return "What do you want to look in?";
                }
                IHaveInventory container;
                //5th condition
                if (text.Length == 3)
                {
                    container = p;
                    return LookAtIn(text[2], container);
                }
                //6th condition which is array Length == 5 since we put check Array Length as parrent condition
                else
                {
                    string containerId = text[4];
                    container = FetchContainer(p, containerId);
                    if (container == null)
                    {
                        return $"I can't find the {containerId}";
                    }
                }
                //7th
                string itemId = text[2];
                //8th action
                return LookAtIn(itemId, container);
            }
            //locate command
            if (text.Length == 1 && text[0] == "look")
            {
                return p.Location.FullDescription;
            }
            else
            {
                return "I don't know how to look like that";
            }  
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
