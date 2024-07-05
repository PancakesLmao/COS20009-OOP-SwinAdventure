using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public abstract class GameObject : IdentifiableObj
    {
        private string _description;
        private string _name;

        public GameObject(string[] ids, string name, string desc) : base(ids) 
        {
            _description = desc;
            _name = name;
        }

        //properties
        public string Name
        {   
            get { return _name; }
        }
        public string ShortDescription
        {
            //return the name and the first ID of game obj (identifiable obj)
            get { return $"{Name} - {FirstID}"; }
        }

        public virtual string FullDescription
        {
            //return description
            get { return _description; }
        }
    }
}
