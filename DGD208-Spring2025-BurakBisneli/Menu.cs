namespace DGD208_Spring2025_BurakBisneli;

public class Menu // I used (https://www.youtube.com/watch?v=YyD1MRJY0qI) this tutorial to achieve the base menu and used Claude Sonnet 4 to get ideas to adapt it myself.
{
    private ConsoleKeyInfo _key;
    
    private static class Colors
    {
        public const string Magenta = "\e[35m";
        public const string Default = "\e[0m";
        public const string Green = "\e[32m";
        public const string Cyan = "\e[36m";
    }
    
    private int _left, _top;
    
    
    public void StartMenu()
    {
        ShowMainMenu();
    }

    
    private int ShowMenu(string title, List<MenuItem> menuItems, string subtitle = null)
    {
        Console.Clear();
        
        if (!string.IsNullOrEmpty(title))
        {
            Console.WriteLine($"{Colors.Magenta}{title}{Colors.Default}");
        }
        
        if (!string.IsNullOrEmpty(subtitle))
        {
            Console.WriteLine($"{Colors.Cyan}{subtitle}{Colors.Default}\n");
        }

        (int left, int top) = Console.GetCursorPosition();
        _left = left;
        _top = top;
        
        int selectedOption = 0;
        bool isSelected = false;

        while (!isSelected)
        {
            Console.SetCursorPosition(_left, _top);
            
            // Display menu items
            for (int i = 0; i < menuItems.Count; i++)
            {
                string color = selectedOption == i ? Colors.Green : Colors.Default; // Changing the selected option color
                Console.WriteLine($"{color}{menuItems[i].Text}{Colors.Default}");
            }
            
            _key = Console.ReadKey(false);

            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption == menuItems.Count - 1) ? 0 : selectedOption + 1;
                    break;
                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption == 0) ? menuItems.Count - 1 : selectedOption - 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }
        
        return selectedOption;
    }

    
    private void ShowMainMenu() 
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Start Game", ShowGameMenu),
            new MenuItem("Credits", ShowCredits),
            new MenuItem("Exit Game", ExitGame)
        };

        
        int selection = ShowMenu("           Interactive Pet Simulator", menuItems, 
            "Use up and down arrow keys to navigate and enter to select.");
        
        menuItems[selection].Action.Invoke();
        
        
        
    }

    private void ShowGameMenu()
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Adopt Pet", ShowAdoptPetMenu),
            new MenuItem("See Current Pets", ShowCurrentPetsMenu),
            new MenuItem("See Current Items", ShowCurrentItemsMenu),
            new MenuItem("Back To Main Menu", () => {  })
        };

        
        
        int selection = ShowMenu("Game Menu", menuItems);
        
        if (selection == 3) // Back to main menu
        {
            ShowMainMenu();
        }
        else
        {
            menuItems[selection].Action.Invoke();
        }
        
    }

    private void ShowAdoptPetMenu()
    {
        var menuItems = new List<MenuItem>();
        
        // Add pets to menu
        foreach (Pet pet in PetDatabase.Pets)
        {
            menuItems.Add(new MenuItem($"{pet.Name} ({pet.Type})", () => ConfirmAdoptPet(pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => { ShowGameMenu(); }));
        
        
        int selection = ShowMenu("Choose a Pet to Adopt", menuItems);
        
        if (selection == menuItems.Count - 1) // Back option
        {
            ShowGameMenu();
        }
        else
        {
            menuItems[selection].Action.Invoke();
        }
       
    }

    private void ConfirmAdoptPet(Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => AdoptPet(pet)),
            new MenuItem("No", () => { ShowAdoptPetMenu(); })
        };

        int selection = ShowMenu($"You will adopt {pet.Name}", menuItems);
        menuItems[selection].Action.Invoke();
    }

    private void AdoptPet(Pet pet)
    {
        PetInventoryManager.AdoptPet(pet);
        
        Console.Clear();
        Console.WriteLine($"{Colors.Cyan}You adopted {pet.Name}!!{Colors.Default}");
        Console.WriteLine("Press any key to back to main menu...");
        Console.ReadKey();
        ShowAdoptPetMenu();
    }

    private void ShowCurrentPetsMenu()
    {
        if (PetInventoryManager.CurrentPets.Count == 0)
        {
            Console.Clear();
            Console.WriteLine($"{Colors.Cyan}You don't have any pets yet!{Colors.Default}");
            Console.WriteLine("Press any key to back to main menu...");
            Console.ReadKey();
            ShowGameMenu();
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (Pet pet in PetInventoryManager.CurrentPets)
        {
            menuItems.Add(new MenuItem($"{pet.Name} | Hunger: {pet.Hunger} | Sleep: {pet.Sleep} | Fun: {pet.Fun}", () => ShowPetOptions(pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => { ShowGameMenu(); }));

        
        int selection = ShowMenu("Your Current Pets", menuItems);
        
        if (selection == menuItems.Count - 1) // Back option
        {
            ShowGameMenu();
        }
        else
        {
            menuItems[selection].Action.Invoke();
        }
        
    }

    private void ShowPetOptions(Pet pet)
    {
        Console.Clear();
        var menuItems = new List<MenuItem>();
        menuItems.Add(new MenuItem($"Feed {pet.Name}",() => {ConfirmFeedPet(pet);}));
        menuItems.Add(new MenuItem($"Play with {pet.Name}",() => {ConfirmPlayPet(pet);}));
        menuItems.Add(new MenuItem($"Use Item", () => {ShowCurrentItemsMenu(pet);}));
        menuItems.Add(new MenuItem($"Back", () => { }));
        
        
        
        int selection = ShowMenu($"{pet.Name} | Hunger: {pet.Hunger} | Sleep: {pet.Sleep} | Fun: {pet.Fun}", menuItems);
        if (selection == menuItems.Count - 1) // Back option
        {
            ShowCurrentPetsMenu(); 
        }

        else
        {
            menuItems[selection].Action.Invoke();
        }
        
    }
    
    private void ConfirmFeedPet(Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => FeedPet(pet).Wait()),
            new MenuItem("No", () => { ShowPetOptions(pet); })
        };

        int selection = ShowMenu($"You will feed {pet.Name} (+20 Hunger, -10 Sleep) Duration: 3s", menuItems);
        menuItems[selection].Action.Invoke();
    }
    
    private void ConfirmPlayPet(Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => PlayWithPet(pet).Wait()),
            new MenuItem("No", () => { ShowPetOptions(pet); })
        };

        int selection = ShowMenu($"You will play with {pet.Name} (+20 Fun, -15 Hunger, -15 Sleep) Duration: 3s", menuItems);
        menuItems[selection].Action.Invoke();
    }

    private async Task FeedPet(Pet pet)
    {
        
        bool feeding = true;
        while (feeding)
        {
            Console.Clear();
            Console.WriteLine("Feeding pet");
            await PetCareManager.FeedPet(pet);
            Console.WriteLine("Pet fed.");
            feeding = false;
        }
        ShowPetOptions(pet);
        
        
    }

    private async Task PlayWithPet(Pet pet)
    {
        bool playing = true;
        while (playing)
        {
            Console.Clear();
            Console.WriteLine("Playing with pet");
            await PetCareManager.PlayWithPet(pet);
            Console.WriteLine("Pet played.");
            playing = false;
        }
        ShowPetOptions(pet);
    }
    

    private void ShowCurrentItemsMenu(Pet pet) // This needs to show items that can use with specific pet.
    {
        Console.Clear();
        if (ItemDatabase.AllItems.Count == 0)
        {
            Console.Clear();
            Console.WriteLine($"{Colors.Cyan}You don't have any items!{Colors.Default}");
            Console.WriteLine("Press any key to back to main menu...");
            Console.ReadKey();
            ShowGameMenu();
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (Item item in ItemDatabase.AllItems)
        {
            if(item.CompatibleWith.Contains(pet.Type)) // This line makes it just show items can use it with specific pet.
                menuItems.Add(new MenuItem($"{item.Name} (+{item.EffectAmount} {item.AffectedStat})", () => ConfirmUsingItem(item, pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => {  }));

       
        
        int selection = ShowMenu("Your Current Items", menuItems);
        
        if (selection == menuItems.Count - 1) // Back option
        {
            ShowPetOptions(pet);
        }
        else
        {
            menuItems[selection].Action.Invoke();
        }
        
    }
    
    private void ConfirmUsingItem(Item item,Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => UseItem(item, pet).Wait()),
            new MenuItem("No", () => { ShowPetOptions(pet); })
        };

        int selection = ShowMenu($"You will use {item.Name} (+{item.EffectAmount} {item.AffectedStat}) Duration: {item.Duration}", menuItems);
        menuItems[selection].Action.Invoke();
    }
    
    private async Task UseItem(Item item, Pet pet)
    {
        bool usingItem = true;
        while (usingItem)
        {
            Console.Clear();
            Console.WriteLine($"Using {item.Name} (+{item.EffectAmount} {item.AffectedStat})");
            await PetCareManager.UseItem(pet, item);
            Console.WriteLine($"{item.Name} used.");
            ItemDatabase.AllItems.Remove(item);
            usingItem = false;
        }
        ShowPetOptions(pet);
        
    }
    
    private void ShowCurrentItemsMenu()
    {
        Console.Clear();
        if (ItemDatabase.AllItems.Count == 0)
        {
            Console.Clear();
            Console.WriteLine($"{Colors.Cyan}You don't have any items!{Colors.Default}");
            Console.WriteLine("Press any key to back to main menu...");
            Console.ReadKey();
            ShowGameMenu();
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (Item item in ItemDatabase.AllItems)
        {
            menuItems.Add(new MenuItem($"{item.Name} (+{item.EffectAmount} {item.AffectedStat})", () => { ShowGameMenu();})); 
        }
        
        
        menuItems.Add(new MenuItem("Back", () => { ShowGameMenu(); }));

        bool stayInPetsItemsMenu = true;
        
        int selection = ShowMenu("Your Current Items", menuItems);
        
        if (selection == menuItems.Count - 1) // Back option
        {
            ShowGameMenu();
        }
        else
        {
            menuItems[selection].Action.Invoke();
        }
        
    }

    private void ShowCredits()
    {
        Console.Clear();
        Console.WriteLine($"{Colors.Magenta}Credits{Colors.Default}");
        Console.WriteLine("Created by: Burak Bisneli");
        Console.WriteLine("Tutorial reference for menu: https://www.youtube.com/watch?v=YyD1MRJY0qI");
        Console.WriteLine("Used LLM's: Claude Sonnet 4, ChatGPT (for getting idea and asking things that i don't know)");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        ShowMainMenu();
    }

    private void ExitGame()
    {
        Console.Clear();
        Console.WriteLine($"{Colors.Cyan}Made by Burak Bisneli.{Colors.Default}");
        Environment.Exit(0);
    }
}
