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
            Item feather = new Item("Fjäder", "En vacker färgskiftande fjäder som skimrar i solljuset");
            Item potion = new Item("Moloktrutens Elixir", "En brygd som kan få håret att ändra färg! Men bara på vänster sida, förstås...");

            // Skapa karaktärer
            Character wizard = new Character("Fritz Flamsfot", "trollkarl", "Som jag har väntat på dig äventyrare! Har du möjligt vis det magiska trollspöt med dig?");
            Character foodlady = new Character("Agneta Äppelkind", "mattant", "Vad fint att du kom! \nDoftar det inte ljuvligt av nybakat bröd och bullar?", bun);
            Character skeleton = new Character("Benjamin", "skelett", "Det är tyvärr för sent att rädda mig härifrån.. \nJag är inte ens skinn och ben längre, bara ben.", bone, bun, "Tack så mycket, det här får du av mig.");
            Character scientist = new Character("Doktor Bubbelgurgel Pannvrid", "galen alkemist", "Ah-ha! En besökare! \nVälkommen till Bubbelgurgels domäner! \nHär sprakar, puffar och, tja... exploderar det mest!", potion, feather, "Må stjärnstoftet följa dina fotspår och minns: i Bubbelgurgels värld är inget för konstigt!");

            // Skapa rum
            Room entrance = new Room("Hallen", "Det är en stor och flådig hall med högt i tak.");
            Room attic = new Room("Vinden", "Litet vindsrum med dammiga lakan som täcker möblerna.", wizard);
            Room kitchen = new Room("Köket", "Ett litet mysigt kök med en stor öppen spis, rustika träskåp och en gjutjärnsspis.", foodlady);
            Room dungeon = new Room("Fängelsehålan", "Det är kallt och ruggigt i den stenbeklädda fängelsehålan", skeleton);
            Room livingRoom = new Room("Vardagsrumet", "Ett pampigt vardagsrum med en stor öppen spis");
            Room stairs = new Room("Trapporna", "En stor vacker trappa som leder upp till övervåningen och ett litet hålrum i vägen med en trappa ned till källaren");
            Room basement = new Room("Källaren", "Det är ett litet, mörkt rum gjort i sten.");
            Room lab = new Room("Laboratoriet", "Det är ett rum fyllt av provrör, tänger och glascylindrar fyllda med märkliga saker.", scientist);

            // Lägg till utgångar till rummen
            entrance.Exits.Add("Framåt", stairs);
            entrance.Exits.Add("Höger", kitchen);
            entrance.Exits.Add("Vänster", livingRoom);
            stairs.Exits.Add("Vänster", basement);
            stairs.Exits.Add("Bakåt", entrance);
            basement.Exits.Add("Framåt", dungeon);
            basement.Exits.Add("Höger", lab);
            basement.Exits.Add("Bakåt", stairs);
            lab.Exits.Add("Bakåt", basement);
            dungeon.Exits.Add("Bakåt", basement);
            kitchen.Exits.Add("Bakåt", entrance);
            livingRoom.Exits.Add("Bakåt", entrance);


            // Starta spelet i hallen
            currentRoom = entrance;

   
        }


        // Starta och spela spelet
        public void Start()
        {
            Console.WriteLine("Välkommen till äventyret på slottet Borgen Blåbärsdal");
            bool isPlaying = true;
            Console.WriteLine("Tryck på en tangent för att börja spela!");
            Console.ReadKey();

            while (isPlaying)
            {
                Console.Clear();
                // Beskriv det nuvarande rummet
                currentRoom.Describe();
                if (currentRoom.Character != null)
                {
                    Dialog(currentRoom.Character);   
                }

                // Fråga vart spelaren vill gå och lista möjliga vägar
                Console.WriteLine("-------------------");
                Console.WriteLine($"Vart vill du gå nu?");
                foreach (var exit in currentRoom.Exits)
                {
                    Console.WriteLine($"{exit.Key}: {exit.Value.Name}");
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Använd piltangenterna");
                Console.ResetColor();

                // Ta in vilket håll användaren vill gå åt
                var direction = Console.ReadKey().Key;
                

                switch (direction) 
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        Move(direction); 
                        break;
                    default:
                        Console.WriteLine("Jag förstår inte vart du vill gå. Ange något av följande alternativ:");
                        foreach(var exit in currentRoom.Exits.Keys)
                        {
                            Console.WriteLine($"{exit}");
                        }
                        Console.WriteLine("Tryck på en tangent för att fortsätta.");
                        Console.ReadKey();
                    break;
                }
            }
        }

        // Gå åt något håll och byta rum
        private void Move(ConsoleKey direction)
        {
            string ConvertedDirection;
            if (direction == ConsoleKey.UpArrow)
            {
                ConvertedDirection = "Framåt";
            }
            else if (direction == ConsoleKey.LeftArrow)
            {
                ConvertedDirection = "Vänster";
            }
            else if (direction == ConsoleKey.RightArrow)
            {
                ConvertedDirection = "Höger";
            }
            else 
            {
                ConvertedDirection = "Bakåt";
            }

            if (currentRoom.Exits.ContainsKey(ConvertedDirection))
            {
                currentRoom = currentRoom.Exits[ConvertedDirection];
            }
            else 
            {
                Console.WriteLine("Du kan inte gå åt det hållet.");
            }
        }

        // Dialoger med karaktärerna
        private void Dialog(Character character)
        {
            Console.WriteLine("Det är någon här!");
            switch (character.Name) {
                // Prata med mattanten
                case "Agneta Äppelkind":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"-Jag heter {character.Name}, jag är slottets egna {character.Description}. {character.Dialogue}");
                    if (character.Item != null)
                    {
                        Console.WriteLine($"Här, ta med denna {character.Item.Name} på ditt äventyr!");
                        GetItem(character);
                    }
                    Console.ResetColor();
                    break;
                // Skelettet Benjamin
                case "Benjamin":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"-Jag heter {character.Name}, som du ser är jag ett {character.Description}... {character.Dialogue}");
                    if (character.encountered == false)
                    {
                        Console.WriteLine("Men det hade ju varit gott med en bulle...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        Console.WriteLine("Har du möjligtvis en bulle med dig?");
                        ExchangeDialogue(character);
                    }
                    Console.ResetColor();
                    break;
                // Galna alkemisten
                case "Doktor Bubbelgurgel Pannvrid":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"-{character.Dialogue} \nMånga ser mig som en {character.Description}, mitt namn är {character.Name}");
                    if (character.encountered == false)
                    {
                        Console.WriteLine("Jo, du förstår... Jag är på jakt efter en väldigt specifik fjäder...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        Console.WriteLine("Du har möjligtvis inte sett någon här i slottet?");
                        ExchangeDialogue(character);
                    }
                    Console.ResetColor();
                    break;
                // Om den specifika karaktären inte finns i switchen
                default: 
                    Console.WriteLine("Hittar ingen karaktär...");
                    break;
            }
        }
        // Metod som skriver ut att användaren ger föremål och tar bort det från inventory
        private void GiveItem(Character character) 
        {
            Console.ResetColor();
            Console.WriteLine($"Du gav föremålet {character.RequestedItem.Name.ToLower()} till {character.Name}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"-{character.ThankYouDialogue}");
            Console.ResetColor();
            inventory.Remove(character.RequestedItem);
            character.RequestedItem = null;
        }
        // Metod som skriver ut att användaren får föremål, lägger till inventory och tar bort det från karaktären
        private void GetItem(Character character)
        {
            Console.ResetColor();
            Console.WriteLine($"Du har fått föremålet {character.Item.Name.ToLower()}, {character.Item.Description.ToLower()}");
            inventory.Add(character.Item);
            currentRoom.Character.Item = null;
        }
        private void ExchangeDialogue(Character character)
        {
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine($"Vill du ge {character.RequestedItem.Name} till {character.Name}? (Ja/Nej)");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "Ja":
                        inventory.Remove(character.RequestedItem);
                        GiveItem(character);
                        GetItem(character);
                        character.encountered = true;
                        return;
                    case "Nej":
                        Console.WriteLine("-Nehepp....");
                        return;
                    default:
                        Console.WriteLine("Ange Ja eller Nej");
                        continue;
                }
            }
            
        }
    }
}
