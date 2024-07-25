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
