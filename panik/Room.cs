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
            Console.WriteLine($"Du har kommit in i {Name.ToLower()}. \n{Description}");
            if (Character != null)
            {
                if (Character.Name == "Agneta Äppelkind")
                {
                    Console.WriteLine("Det är någon här!");
                    Console.WriteLine($"Jag heter {Character.Name}, jag är slottets egna {Character.Description}. {Character.Dialog}");
                    if (Character.Item != null)
                    {
                        Console.WriteLine($"Här, ta med denna {Character.Item.Name} på ditt äventyr!");
                        Console.WriteLine($"Du har fått {Character.Item.Description}");

                    }
                }
            }
        }
    }
}
