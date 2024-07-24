using System;

namespace SwinAdven
{
    public class Path : IdentifiableObj
    {
        private Location _destination;
        private bool isLocked;

        public Path(string[] ids, Location destination, bool isLocked = false) : base(ids)
        {
            _destination = destination;
            this.isLocked = isLocked;
        }

        public bool Move(Player p)
        {
            if (isLocked)
            {
                Console.WriteLine("The path is locked.");
                return false;
            }

            p.Location = _destination;
            Console.WriteLine($"You have moved to {_destination.Name}");
            return true;
        }

        public void SetDestination(Location destination)
        {
            _destination = destination;
        }

        public Location Destination
        {
            get { return _destination; }
        }

        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
    }
}
