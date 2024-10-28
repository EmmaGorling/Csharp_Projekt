using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace panik
{
    public class Game
    {
        // Variabel för att hantera nuvarande rummet
        private Room currentRoom;

        public List<Item> inventory;

        public Game() 
        {
            // Skapa nu lista för ryggsäcken
            inventory = new List<Item>();

            // Skapa items
            Item bun = new Item("Bulle", "En varm, nygräddad kanelbulle");
            Item wand = new Item("Trollstav", "En glittrande magiskt trollstav");
            Item bone = new Item("Ben", "Är det ett tuggben eller kan det vara Benjamins lårben..?");

            // Skapa karaktärer
            Character wizard = new Character("Fritz Flamsfot", "trollkarl", "Som jag har väntat på dig äventyrare! Har du möjligt vis det magiska trollspöt med dig?");
            Character foodlady = new Character("Agneta Äppelkind", "mattant", "Vad fint att du kom! Doftar det inte ljuvligt av nybakat bröd och bullar?", bun);
            Character skeleton = new Character("Benjamin", "skelett", "Det är tyvärr för sent att rädda mig härifrån.. Jag är inte ens skinn och ben längre, bara ben. Har du möjligtvis en bulle med dig?", bone);

            // Skapa rum
            Room entrance = new Room("Hallen", "En stor och flådig hall med högt i tak.");
            Room attic = new Room("Vinden", "Litet vindsrum med dammiga lakan som täcker möblerna.", wizard);
            Room kitchen = new Room("Köket", "Ett litet mysigt kök med en stor öppen spis, rustika träskåp och en gjutjärnsspis.", foodlady);
            Room dungeon = new Room("Fängelsehålan", "En kall, ruggig fängelsehåla med stenväggar", skeleton);
            Room livingRoom = new Room("Vardagsrumet", "Ett pampigt vardagsrum med en stor öppen spis");


            // Lägg till utgångar till rummen
            entrance.Exits.Add("Höger", kitchen);
            entrance.Exits.Add("Vänster", livingRoom);
            kitchen.Exits.Add("Bakåt", entrance);


            // Starta spelet i hallen
            currentRoom = entrance;

   
        }


        // Starta och spela spelet
        public void Start()
        {
            Console.WriteLine("Välkommen till äventyret på slottet Borgen Blåbärsdal");
            bool isPlaying = true;

            while (isPlaying)
            {
                // Beskriv det nuvarande rummet
                currentRoom.Describe();

                Console.WriteLine($"Vart vill du gå nu?");
                foreach (var exit in currentRoom.Exits)
                {
                    Console.WriteLine($"{exit.Key}: {exit.Value.Name}");
                }
                
                string direction = Console.ReadLine();
                

                switch (direction) 
                {
                    case "Framåt":
                    case "Bakåt":
                    case "Höger":
                    case "Vänster":
                        Move(direction); 
                        break;
                    default:
                        Console.WriteLine("Jag förstår inte vart du vill gå. Ange något av följande alternativ:");
                        foreach(var exit in currentRoom.Exits.Keys)
                        {
                            Console.WriteLine($"{exit}");
                        }
                    break;
                }
            }
        }

        // Gå åt något håll och byta rum
        private void Move(string direction)
        {
            if (currentRoom.Exits.ContainsKey(direction))
            {
                currentRoom = currentRoom.Exits[direction];
            }
            else 
            {
                Console.WriteLine("Du kan inte gå åt det hållet.");
            }
        }
    }
}
