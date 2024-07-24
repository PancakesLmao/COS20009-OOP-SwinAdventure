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
            // 1. Check the number of elements in the array
            if (text.Length != 3 && text.Length != 5)
            {
                return "I don't know how to look like that";
            }

            // 2. Check if the first word is "look"
            if (text[0] != "look")
            {
                return "Error in look input";
            }

            // 3. Check if the second word is "at"
            if (text[1] != "at")
            {
                return "What do you want to look at?";
            }

            // 4. Handle 5-element case with "in"
            if (text.Length == 5)
            {
                if (text[3] != "in")
                {
                    return "What do you want to look in?";
                }
            }

            // Determine container based on array length
            IHaveInventory container;
            if (text.Length == 3)
            {
                container = p; // Default to the player as container
            }
            else
            {
                // text.Length == 5, so get container ID from text[4]
                string containerId = text[4];
                container = FetchContainer(p, containerId);
                if (container == null)
                {
                    return $"I can't find the {containerId}";
                }
            }

            // The item ID is always the 3rd word
            string itemId = text[2];

            // Perform the look at in
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
