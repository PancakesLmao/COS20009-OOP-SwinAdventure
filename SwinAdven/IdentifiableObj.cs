using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdven
{
    public class IdentifiableObj
    {
        //string List field
        List<string> _identifiers;

        //constructor
        public IdentifiableObj(string[] idents)
        {
            //create Object
            _identifiers = new List<string>();
            foreach (var id in idents)
            {
                AddIdentifier(id);
            }
        }

        //property
        public string FirstID
        {
            get 
            {
                if (_identifiers.Count > 0)
                {
                    return _identifiers[0];
                }
                else
                {
                    //empty string
                    return "";
                 }
            }
        }

        //method
        public bool AreYou(string id)
        {
            //check if the identifier is in the List
            return _identifiers.Contains(id.ToLower());
        }
        public void AddIdentifier (string id)
        {
            //converts the identifier to lower case and stores it in _identifiers
            _identifiers.Add(id.ToLower());
        }
    }
}
