using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace panik
{
    public class Game
    {
        // Variabel för att hantera nuvarande rummet
        private Room currentRoom;

        // Lista för att hantera samlade items
        public List<Item> inventory;

        // Variabel för att hantera rum som ska läggas till vid senare tillfälle
        private Room hiddenRoom;

        public Game() 
        {
            // Skapa nu lista för ryggsäcken
            inventory = new List<Item>();

            // Skapa items
            Item bun = new Item("Bulle", "En varm, nygräddad kanelbulle");
            Item wand = new Item("Trollstav", "En glittrande, magisk trollstav");
            Item bone = new Item("Ben", "Är det ett tuggben eller kan det vara Benjamins lårben..?");
            Item feather = new Item("Fjäder", "En vacker färgskiftande fjäder som skimrar i solljuset");
            Item potion = new Item("Moloktrutens Elixir", "En brygd som kan få håret att ändra färg! Men bara på vänster sida, förstås...");

            // Skapa karaktärer
            Character wizard = new Character("Fritz Flamsfot", "trollkarl", "Som jag har väntat på dig äventyrare!", wand, potion);
            Character foodlady = new Character("Agneta Äppelkind", "mattant", "Vad fint att du kom! \nDoftar det inte ljuvligt av nybakat bröd och bullar?", bun);
            Character skeleton = new Character("Benjamin", "skelett", "Det är tyvärr för sent att rädda mig härifrån.. \nJag är inte ens skinn och ben längre, bara ben.", bone, bun, "Tack så mycket, det här får du av mig.");
            Character scientist = new Character("Doktor Bubbelgurgel Pannvrid", "galen alkemist", "Ah-ha! En besökare! \nVälkommen till Bubbelgurgels domäner! \nHär sprakar, puffar och, tja... exploderar det mest!", potion, feather, "Må stjärnstoftet följa dina fotspår och minns: i Bubbelgurgels värld är inget för konstigt!");
            Character parrot = new Character("Sir Snorkelfjäder", "utan tvivel, världens vackraste papegoja", "Jag antar att du är här för att klura ut ett och annat, eller hur? \nVillrådig! Fumlig! Stackars lilla varelse… \nMen, men, Sir Snorkelfjäder är inte hjärtlös. Nej, nej, jag kan unna dig en av mina gåtor.\nOm du nu har tillräckligt med vett för att förstå, vill säga…", feather);
            Character guardDog = new Character("Fluffy", "stor, skräckinjagande hund", "Morr...", null, bone);

            // Skapa rum
            Room entrance = new Room("Hallen", "Du befinner dig i en stor flådig hall med högt i tak.");
            Room attic = new Room("Vinden", "Litet vindsrum med dammiga lakan som täcker möblerna.", wizard);
            Room kitchen = new Room("Köket", "Ett litet mysigt kök med en stor öppen spis, rustika träskåp och en gjutjärnsspis.", foodlady);
            Room dungeon = new Room("Fängelsehålan", "Det är kallt och ruggigt i den stenbeklädda fängelsehålan", skeleton);
            Room livingRoom = new Room("Vardagsrumet", "Ett pampigt vardagsrum med en stor öppen spis");
            Room stairs = new Room("Trapporna", "En stor vacker trappa som leder upp till övervåningen och ett litet hålrum i vägen med en trappa ned till källaren");
            Room basement = new Room("Källaren", "Det är ett litet, mörkt rum gjort i sten.");
            Room lab = new Room("Laboratoriet", "Det är ett rum fyllt av provrör, tänger och glascylindrar fyllda med märkliga saker.", scientist);
            Room library = new Room("Biblioteket", "Det är ett dammigt rum med bokhyllor från golv till tak, fyllda med magiska böcker.");
            Room banquetHall = new Room("Festsalen", "Ett grandiost bankettrum med stora böljande gardiner och en scen.", parrot);
            Room upstairs = new Room("Övervåningens hall", "Du ser ett dunkelt rum med flera dörrar... Är det en riddarrustning där borta i hörnet?");
            Room atticStairs = new Room("Vindstrappan", "Det är mörkt och drar kallt uppifrån trappan framför dig..", guardDog);
            

            // Lägg till utgångar till rummen
            entrance.Exits.Add("Framåt", stairs);
            entrance.Exits.Add("Höger", kitchen);
            entrance.Exits.Add("Vänster", livingRoom);
            stairs.Exits.Add("Framåt",upstairs);
            stairs.Exits.Add("Höger", banquetHall);
            stairs.Exits.Add("Vänster", basement);
            stairs.Exits.Add("Bakåt", entrance);
            upstairs.Exits.Add("Framåt", atticStairs);
            upstairs.Exits.Add("Höger", library);
            upstairs.Exits.Add("Bakåt", stairs);
            basement.Exits.Add("Framåt", dungeon);
            basement.Exits.Add("Höger", lab);
            basement.Exits.Add("Bakåt", stairs);
            lab.Exits.Add("Bakåt", basement);
            dungeon.Exits.Add("Bakåt", basement);
            kitchen.Exits.Add("Bakåt", entrance);
            kitchen.Exits.Add("Vänster", banquetHall);
            banquetHall.Exits.Add("Höger", kitchen);
            banquetHall.Exits.Add("Bakåt", stairs);
            livingRoom.Exits.Add("Bakåt", entrance);
            atticStairs.Exits.Add("Bakåt", upstairs);
            attic.Exits.Add("Bakåt", atticStairs);


            // Starta spelet i hallen
            currentRoom = entrance;

            hiddenRoom = attic;
            
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
                // Rensa konsol och vänta0.8 sek innan fortsatt utskrift
                Console.Clear();
                Thread.Sleep(800);
                Console.WriteLine("\n\n");
                // Beskriv det nuvarande rummet
                currentRoom.Describe();
                if (currentRoom.Character != null)
                {
                    Dialog(currentRoom.Character, currentRoom);   
                }

                // Fråga vart spelaren vill gå och lista möjliga vägar
                Console.WriteLine("\n\n-------------------");
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
                Console.WriteLine("Du kan inte gå åt det hållet.\nTryck på valfri tanget för att fortsätta");
                Console.ReadKey();
            }
        }

        // Dialoger med karaktärerna
        private void Dialog(Character character, Room currentRoom)
        {
            Console.WriteLine("Det är någon här!\n\n");
            Thread.Sleep(500);
            switch (character.Name) {
                // Prata med mattanten
                case "Agneta Äppelkind":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Jag heter {character.Name}, jag är slottets egna {character.Description}. {character.Dialogue}");
                    if (character.Item != null)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine($"\nHär, ta med denna {character.Item.Name} på ditt äventyr!");
                        GetItem(character);
                    }
                    Console.ResetColor();
                    break;
                // Skelettet Benjamin
                case "Benjamin":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Jag heter {character.Name}, som du ser är jag ett {character.Description}... {character.Dialogue}");
                    if (character.encountered == false)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("\nMen det hade ju varit gott med en bulle...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        Console.WriteLine("\nHar du möjligtvis en bulle med dig?");
                        ExchangeDialogue(character);
                    }
                    Console.ResetColor();
                    break;
                // Galna alkemisten
                case "Doktor Bubbelgurgel Pannvrid":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{character.Dialogue} \nMånga ser mig som en {character.Description}, mitt namn är {character.Name}");
                    if (character.encountered == false)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("\nJo, du förstår... Jag är på jakt efter en väldigt specifik fjäder...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        Console.WriteLine("\nDu har möjligtvis inte sett någon här i slottet?");
                        ExchangeDialogue(character);
                    }
                    Console.ResetColor();
                    break;
                // Papegojan
                case "Sir Snorkelfjäder":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Det var länge sedan jag såg någon utomstående... \nMitt namn är {character.Name} och jag är {character.Description}");
                    Thread.Sleep(500);
                    if (character.encountered == false)
                    {
                        Console.WriteLine($"\n{character.Dialogue}");
                        Riddle(character);
                    }
                    Console.ResetColor();
                    break;
                // Vakthunden
                case "Fluffy":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{character.Dialogue}");
                    Thread.Sleep(500);
                    if (character.encountered == false)
                    {
                        Console.WriteLine($"\n{character.Dialogue}");
                        Console.WriteLine($"\nHunden verkar vilja ha något...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        ExchangeDialogue(character);
                    }
                    if(character.encountered == true)
                    {
                        Console.WriteLine($"{character.Name} viftar på svansen, tar benet och går iväg..");
                        currentRoom.Character = null;
                        currentRoom.Exits.Add("Framåt", hiddenRoom);
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
            Console.WriteLine($"\nDu gav föremålet \"{character.RequestedItem.Name}\" till {character.Name}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n{character.ThankYouDialogue}");
            Console.ResetColor();
            inventory.Remove(character.RequestedItem);
            character.RequestedItem = null;
        }
        // Metod som skriver ut att användaren får föremål, lägger till inventory och tar bort det från karaktären
        private void GetItem(Character character)
        {
            Console.ResetColor();
            Console.WriteLine($"\nDu har fått föremålet \"{character.Item.Name}\". {character.Item.Description}");
            inventory.Add(character.Item);
            currentRoom.Character.Item = null;
        }
        private void ExchangeDialogue(Character character)
        {
            while (true)
            {
                Console.ResetColor();
                Thread.Sleep(500);
                Console.WriteLine($"\nVill du ge \"{character.RequestedItem.Name}\" till {character.Name}? (Ja/Nej)");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "Ja":
                        inventory.Remove(character.RequestedItem);
                        GiveItem(character);
                        if (character.Item != null)
                        {
                            GetItem(character);
                        }
                        character.encountered = true;
                        return;
                    case "Nej":
                        Console.WriteLine("\n....");
                        return;
                    default:
                        Console.WriteLine("\nAnge Ja eller Nej");
                        continue;
                }
            }
            
        }
        private void Riddle(Character character) 
        {
            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine("\nRedo...?");
                Console.WriteLine("\n[Vad blir blötare ju mer det torkar?]");
                Console.ResetColor();
                string answer = Console.ReadLine();
                Thread.Sleep(500);
                if (answer.ToLower() == "handduk" || answer.ToLower().Contains("handduk"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nPff, rena turen! \nHär... Ta den här.");
                    GetItem(character);
                    character.encountered = true;
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nÅh, stackars själ, förstår du ens frågan?");
                    continue;
                }
            }
        }

    }
}
