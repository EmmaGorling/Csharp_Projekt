using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace panik
{
    public class Room
    {
        public string Name { get; set; } = ""; 
        public string Description { get; set; } = "";
        public Dictionary<string, Room> Exits { get; set; } = new Dictionary<string, Room>();
        public Character? Character { get; set; } = null;

        public Room(string name, string description, Character character = null) 
        { 
            Name = name;
            Description = description;
            Character = character;
        }

        public void Describe()
        {
            Console.WriteLine($"Du har kommit till {Name.ToLower()}. \n{Description}");
        }
    }
}
