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
        public string Dialogue { get; set; } = "";
        public bool encountered { get; set; } = false;
        public Item? Item { get; set; }

        public Item? RequestedItem { get; set; }
        public string? ThankYouDialogue {  get; set; }

        public Character(string name,string description, string dialog, Item item = null, Item requestedItem = null, string thankYouDialogue = null) 
        {
            Name = name;
            Description = description;
            Dialogue = dialog;
            Item = item;
            RequestedItem = requestedItem;
            ThankYouDialogue = thankYouDialogue;
        }
    }
}
