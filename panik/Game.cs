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
        public List<Item> inventory = new List<Item>();

        // Variabel för att hantera rum som ska läggas till vid senare tillfälle
        private Room hiddenRoom;

        //Variabel för att ändra karaktär i rum
        private Character princessCharacter;

        public Game() 
        {

            // Skapa items
            Item bun = new Item("Bulle", "En varm, nygräddad kanelbulle");
            Item wand = new Item("Trollstav", "En glittrande, magisk trollstav");
            Item bone = new Item("Ben", "Är det ett tuggben eller kan det vara Benjamins lårben..?");
            Item feather = new Item("Fjäder", "En vacker färgskiftande fjäder som skimrar i solljuset");
            Item potion = new Item("Moloktrutens Elixir", "En brygd som kan få håret att ändra färg! Men bara på vänster sida, förstås...");

            // Skapa karaktärer
            Character wizard = new Character("Fritz Flamsfot", "mästare av magi och mysterier", "Som jag har väntat på dig äventyrare!", wand, potion, "Här, ta denna glittrande trollstav! \nDen kan kanske få något att hända... eller så kanske inte! Haha!");
            Character foodlady = new Character("Agneta Äppelkind", "mattant", "Vad fint att du kom! \nDoftar det inte ljuvligt av nybakat bröd och bullar?", bun);
            Character skeleton = new Character("Benjamin", "skelett", "Det är tyvärr för sent att rädda mig härifrån.. \nJag är inte ens skinn och ben längre, bara ben.", bone, bun, "Tack så mycket, det här får du av mig.");
            Character scientist = new Character("Doktor Bubbelgurgel Pannvrid", "galen alkemist", "Ah-ha! En besökare! \nVälkommen till Bubbelgurgels domäner! \nHär sprakar, puffar och, tja... exploderar det mest!", potion, feather, "Må stjärnstoftet följa dina fotspår och minns: i Bubbelgurgels värld är inget för konstigt!");
            Character parrot = new Character("Sir Snorkelfjäder", "utan tvivel, världens vackraste papegoja", "Jag antar att du är här för att klura ut ett och annat, eller hur? \nVillrådig! Fumlig! Stackars lilla varelse… \nMen, men, Sir Snorkelfjäder är inte hjärtlös. Nej, nej, jag kan unna dig en av mina gåtor.\nOm du nu har tillräckligt med vett för att förstå, vill säga…", feather);
            Character guardDog = new Character("Fluffy", "stor, skräckinjagande hund", "Morr...", null, bone);
            Character princess = new Character("Prinsessan Asp", "elegant och vacker prinsessa", "Åh, tack för att du räddade mig!");
            Character frog = new Character("groda", "liten och slemmig", "Qvaaaaak", null, wand);
            Character dad = new Character("Lord Fjällås", "slottets ägare", "Äntligen är du här! \nMin dotter... Hon skulle hjälpa trollkarlen med sitt gråa hår, men misslyckades. \nHan blev så arg att han förvandlade henne till en groda. \nSnälla, rädda henne!");
 

            // Skapa rum
            Room entrance = new Room("Hallen", $"där kalla stenplattor ekar under dina steg och \nsvaga skuggor dansar i ljuset från en ensam lykta på väggen. \nEn gammal träbänk står intill väggen, märkt av tidens tand, \noch en svag doft av jord och fukt ligger i luften. \nFramför dig leder en bred trappa uppåt, \nmedan dörrar till vänster och höger tycks inbjuda till mörkare vrår av slottet.", dad);
            Room attic = new Room("Vinden", "där lågt i tak och lutande bjälkar ger platsen en \nhemlig, nästan kvävande atmosfär. \nStoft dansar i tunna strålar av ljus som sipprar in \nfrån ett smalt fönster, och rummet är fyllt av hyllor med \nmystiska flaskor, pergamentrullar och udda föremål som tycks glöda svagt.", wizard);
            Room kitchen = new Room("Köket", "där doften av kryddor och örter omfamnar dig och \ndu hör vedens svaga knastrande från den stora ugnen. \nLjuset från flera stearinljus dansa över stenväggarna och \nskapar en mjuk, varm atmosfär.", foodlady);
            Room dungeon = new Room("Fängelsehålan", "där luften är tjock av fukt och en svag, unken doft av mögel. \nVäggarna är kalla och våta, och långa järnkedjor hänger från stenblocken, \ndinglande som tysta påminnelser om tidigare fångar. \nEn svag ljusstrimma sipprar in genom ett smalt fönstergaller högt uppe på väggen, \nmen försvinner snabbt i mörkret som tycks kväva varje ljud och rörelse.", skeleton);
            Room livingRoom = new Room("Vardagsrumet", "där en öppen spis sprakar och kastar ett varmt, \ndansande ljus över de slitna stenväggarna. \nEn stor fåtölj med mjuka, nötta kuddar står vänd mot elden, och \nett litet bord bredvid bär spår av gamla tekoppar och en tjock bok som lämnats uppslagen. \nRummets mörka träpaneler och mjuka mattor ger en känsla av ro..", dad);
            Room stairs = new Room("Trappuppgången", "där en bred, elegant trappa i mörkt trä leder majestätiskt upp till övervåningen. \nTrappans räcke är utsirat med detaljerade sniderier som fångar ljuset från \nde höga fönstren och skapar skuggor som dansar över golvet. \nTill vänster, nästan dold i skuggan, finns ett smalt skrymsle som leder nedåt mot källarens dolda djup. \nTill höger lockar ljudet av avlägsna röster och klingande glas från festsalen.");
            Room basement = new Room("Källaren", "där väggarna är täckta av flagnande kalk och \nfuktpärlor som långsamt sipprar ned längs stenen. \nLjuset här är svagt, och skuggorna från en ensam lykta kastar darrande mönster över rummet. \nFramför dig finns två tunga dörrar: \nEn rakt fram, där kalla järnbeslag antyder vägen till fängelsehålan, \noch en till höger, som är täckt med fläckar av sot och \nsyraspill – spår som verkar leda till ett laboratorium.");
            Room lab = new Room("Laboratoriet", "där luften är tung av kemikalier och en svag doft av bränt socker möter din näsa. \nHyllor sträcker sig längs väggarna, fyllda med flaskor av alla former och färger, \nnågra glittrar som stjärnor, medan andra är grumliga och mystiska. \nPå det stora träbordet i rummets mitt ligger ett kaos av uråldriga böcker, \nkvicksilver och otydliga anteckningar, som tycks vara nedskrivna av ett galet geni.", scientist);
            Room library = new Room("Biblioteket", "där tysta viskningar av historia och kunskap svävar i luften. \nHöga bokhyllor sträcker sig upp mot taket, fyllda med gamla volymer och \ndammiga tomma pärmar som bär på hemligheter från svunna tider. \nEtt mjukt ljus flödar från en stor fönsterbåge som släpper in strålar av sol, \noch i rummets mitt står ett stort träbord, omgiven av tunga läderstolar, \ndär böcker och pergament ligger utspridda.");
            Room banquetHall = new Room("Festsalen", "där taket svänger högt över dig, dekorerat med eleganta stuckaturer som \nfångar ljuset från stora, hängande ljuskronor. \nRummets väggar är klädda med djupa, rika färger och tunga \ndraperier som verkar viska om festligheter och storslagna sammankomster.", parrot);
            Room upstairs = new Room("Övervåningens hall", "där skuggorna samlas i hörnen och ett svagt ljus strömmar in \ngenom ett stort fönster som ger rummet en dämpad glöd. \nTill höger leder en tung dörr in till biblioteket, vars mörka hyllor tycks \ngömma hemligheter som inte vill bli funna. \nRakt fram står en dörr som öppnar till vindstrappan, där trappstegen försvinner i \ndunklet och viskar om det okända. \nTill vänster finns en annan dörr, prydd med mystiska inskriptioner, \nsom antyder att något oväntat döljer sig där bakom.");
            Room atticStairs = new Room("Vindstrappan", "där dimman av kall luft sveper mot dig och varje steg tycks försvinna in i det okända. \nMörka, knotiga träbjälkar sträcker sig över huvudet, och trappstegen knarrar \noroande under din vikt, som om de protesterar mot att bli betraktade.", guardDog);
            Room princessChamber = new Room("Prinsessans kammare", "där en drömmande atmosfär genomsyrar rummet. \nMjuka, pastellfärgade väggar är prydda med delikata tapeter som \nskildrar scener från sagor och legender. \nI ett hörn står en liten skrivbord med en öppen dagbok, där \ntankar och drömmar väntar på att bli lästa.", frog);

            // Lägg till utgångar till rummen
            entrance.Exits.Add("Upp", stairs);
            entrance.Exits.Add("Höger", kitchen);
            entrance.Exits.Add("Vänster", livingRoom);
            stairs.Exits.Add("Upp",upstairs);
            stairs.Exits.Add("Höger", banquetHall);
            stairs.Exits.Add("Vänster", basement);
            stairs.Exits.Add("Ned", entrance);
            upstairs.Exits.Add("Upp", atticStairs);
            upstairs.Exits.Add("Höger", library);
            upstairs.Exits.Add("Vänster", princessChamber);
            upstairs.Exits.Add("Ned", stairs);
            basement.Exits.Add("Upp", dungeon);
            basement.Exits.Add("Höger", lab);
            basement.Exits.Add("Ned", stairs);
            lab.Exits.Add("Ned", basement);
            dungeon.Exits.Add("Ned", basement);
            kitchen.Exits.Add("Ned", entrance);
            kitchen.Exits.Add("Vänster", banquetHall);
            banquetHall.Exits.Add("Höger", kitchen);
            banquetHall.Exits.Add("Ned", stairs);
            livingRoom.Exits.Add("Ned", entrance);
            atticStairs.Exits.Add("Ned", upstairs);
            attic.Exits.Add("Ned", atticStairs);
            princessChamber.Exits.Add("Ned", upstairs);
            library.Exits.Add("Ned", upstairs);


            // Starta spelet i hallen
            currentRoom = entrance;

            // Rum som ska öppnas senare i spelet
            hiddenRoom = attic;

            // Karaktär som ska bytas ut i ett av rummen
            princessCharacter = princess;
            
        }

        

        // Starta och spela spelet
        public void Start()
        {
            Console.WriteLine("                                  |>>>");
            Console.WriteLine("                                  |");
            Console.WriteLine("                    |>>>      _  _|_  _         |>>>");
            Console.WriteLine("                    |        |;| |;| |;|        |");
            Console.WriteLine("                _  _|_  _    \\.    .  /    _  _|");
            Console.WriteLine("               |;|_|;|_|;|    \\:. ,  /    |;|_|;|_|;|");
            Console.WriteLine("               \\..      /    ||;   . |    \\.    .  /");
            Console.WriteLine("                \\.  ,  /     ||:  .  |     \\:  .  /");
            Console.WriteLine("                 ||:   |_   _ ||_ . _ | _   _||:   |");
            Console.WriteLine("                 ||:  .|||_|;|_|;|_|;|_|;|_|;||:.  |");
            Console.WriteLine("                 ||:   ||.    .     .      . ||:  .|");
            Console.WriteLine("                 ||: . || .     . .   .  ,   ||:   |       \\,/");
            Console.WriteLine("                 ||:   ||:  ,  _______   .   ||: , |            /`\\");
            Console.WriteLine("                 ||:   || .   /+++++++\\    . ||:   |");
            Console.WriteLine("                 ||:   ||.    |+++++++| .    ||: . |");
            Console.WriteLine("              __ ||: . ||: ,  |+++++++|.  . _||_   |");
            Console.WriteLine("     ____--`~    '--~~__|.    |+++++__|----~    ~`---,              ___");
            Console.WriteLine("-~--~                   ~---__|,--~'                  ~~----_____-~'   `~----~~\n\n");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("              Välkommen till äventyret på slottet Blåbärsdal");
            Console.WriteLine("                 Tryck på en tangent för att börja spela!");
            Console.ReadKey();
            bool isPlaying = true;

            while (isPlaying)
            {
                // Rensa konsol och vänta 0.5 sek innan fortsatt utskrift
                Console.Clear();
                Thread.Sleep(500);
                Console.WriteLine("\n\n");
                // Beskriv det nuvarande rummet
                currentRoom.Describe();
                if (currentRoom.Character != null)
                {
                    Dialogue(currentRoom.Character, currentRoom);   
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
                ConvertedDirection = "Upp";
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
                ConvertedDirection = "Ned";
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
        private void Dialogue(Character character, Room currentRoom)
        {
            Thread.Sleep(300);
            Console.WriteLine("\nDet är någon här!\n\n");
            Thread.Sleep(300);
            switch (character.Name) {
                // Lord Fjällås
                case "Lord Fjällås":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    if (currentRoom.Name == "Hallen")
                    {
                        Console.WriteLine($"Mitt namn är {character.Name}, det är jag som är {character.Description}");
                        Console.WriteLine($"{character.Dialogue}\n");
                        Console.ResetColor();
                        Console.WriteLine($"{character.Name} lunkar sakta in i vardagsrummet...\n");
                        Console.WriteLine("Tryck på en tangent för att fortsätta");
                        Console.ReadKey();
                        currentRoom.Character = null;
                        character.Dialogue = "Åååh min dotter...";
                    } else
                    {
                        Console.WriteLine($"{character.Dialogue}\n");
                        Console.ResetColor();
                        Console.WriteLine($"Jämrar {character.Name}");
                    }
                    break;
                // Prata med mattanten
                case "Agneta Äppelkind":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Jag heter {character.Name}, jag är slottets egna {character.Description}. {character.Dialogue}");
                    if (character.Item != null)
                    {
                        Thread.Sleep(300);
                        Console.WriteLine($"\nHär, ta med denna {character.Item.Name} på ditt äventyr!");
                        GetItem(character);
                    }
                    Console.ResetColor();
                    break;
                // Skelettet Benjamin
                case "Benjamin":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Jag heter {character.Name}, som du ser är jag ett {character.Description}... {character.Dialogue}");
                    if (character.Encountered == false)
                    {
                        Thread.Sleep(300);
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
                    if (character.Encountered == false)
                    {
                        Thread.Sleep(300);
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
                    Thread.Sleep(300);
                    if (character.Encountered == false)
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
                    Thread.Sleep(300);
                    if (character.Encountered == false)
                    {
                        Console.WriteLine($"\n{character.Dialogue}");
                        Console.ResetColor();
                        Console.WriteLine($"\nHunden verkar vilja ha något...");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        ExchangeDialogue(character);
                    }
                    if(character.Encountered == true)
                    {
                        Console.ResetColor();
                        Console.WriteLine($"{character.Name} viftar på svansen, tar benet och går iväg..");
                        currentRoom.Character = null;
                        currentRoom.Exits.Add("Upp", hiddenRoom);
                    }
                    Console.ResetColor();
                    break;
                // Trollkarlen
                case "Fritz Flamsfot":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Jag är {character.Name}, {character.Description}. \n{character.Dialogue}");
                    Thread.Sleep(300);
                    if (character.Encountered == false)
                    {
                        Console.WriteLine($"\nSnälla säg att du har fått med dig min hårfärg?");
                    }
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        ExchangeDialogue(character);
                    }
                    if(character.Encountered == true)
                    {
                        Console.ForegroundColor= ConsoleColor.DarkYellow;
                        Console.WriteLine($"{character.ThankYouDialogue}");
                        Console.ResetColor();
                    }
                    Console.ResetColor();
                    break;
                // Groda
                case "groda":
                    Console.WriteLine($"Du har stött på en {character.Description} {character.Name}\n");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{character.Dialogue}\n");
                    Console.ResetColor();
                    Console.WriteLine("Det verkar ligga någon förtrollning över grodan...");
                    if (character.RequestedItem != null && inventory.Contains(character.RequestedItem))
                    {
                        ExchangeDialogue(character);
                    }
                    if (character.Encountered == true)
                    {
                        currentRoom.Character = princessCharacter;
                        EndGame(currentRoom.Character);
                    }
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
                Thread.Sleep(300);
                Console.WriteLine($"\nVill du ge \"{character.RequestedItem.Name}\" till {character.Name}? (Ja/Nej)");
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "ja":
                        inventory.Remove(character.RequestedItem);
                        GiveItem(character);
                        if (character.Item != null)
                        {
                            GetItem(character);
                        }
                        character.Encountered = true;
                        return;
                    case "nej":
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
                Thread.Sleep(300);
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
                    character.Encountered = true;
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
        private void EndGame(Character character) 
        { 
            Console.Clear();
            Thread.Sleep(200);
            Console.WriteLine(".... Poof!\n");
            Console.WriteLine($"Framför dig ser du nu en {character.Description}\n");
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine($"{character.Dialogue} \nDet är bäst att jag springer ned till min pappa med en gång!\n");
            Console.ResetColor();
            Console.WriteLine($"Du har lyckats rädda prinsessan från hennes förbannelse! \n\nSlutet gott, allting gott...\n\n");
            Console.WriteLine("Tack för att du spelade, tryck på valfri tangent för att avsluta spelet");
            Console.ReadKey();
            System.Environment.Exit(0);
        }
    }
}
