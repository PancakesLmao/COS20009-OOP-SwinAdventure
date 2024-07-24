using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" })
        {
        }
        public override string Execute(Player p, string[] text)
        {
            if (!AreYou(text[0]))
            {
                return "Invalid command.";
            }
            if (text.Length < 2)
            {
                if (text[0].ToLower() == "leave")
                {
                    Location previousLocation = p.PreviousLocation();
                    if (previousLocation != null)
                    {
                        p.MoveTo(previousLocation);
                        return $"You have returned to {previousLocation.Name}.";
                    }
                    else
                    {
                        return "You are at the starting location and cannot go back.";
                    }
                }
                return "Move where?";
            }

            string inputDirection = text[1];
            if (Enum.TryParse<Direction>(inputDirection, true, out Direction direction))
            {
                Path path = p.Location.GetPath(direction);
                if (path != null && !path.IsLocked)
                {
                    p.Location = path.Destination;
                    return $"You head {inputDirection}.\nYou have arrived in a {p.Location.Name}.";
                }
                else if (path == null)
                {
                    return "There is no path in that direction.";
                }
                else
                {
                    return "The path is locked.";
                }
            }
            else
            {
                return "Invalid path identifier";
            }
        }
    }
}
