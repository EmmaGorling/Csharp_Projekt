using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace panik
{
    public class Character
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Dialog { get; set; } = "";
        public Item? Item { get; set; }

        public Character(string name,string description, string dialog, Item item = null) 
        {
            Name = name;
            Description = description;
            Dialog = dialog;
            Item = item;
        }
    }
}
