using System.Diagnostics;

namespace DGD208_Spring2025_BurakBisneli;

public class Menu // I used (https://www.youtube.com/watch?v=YyD1MRJY0qI) this tutorial to achieve the base menu and used Claude Sonnet 4 to get ideas to adapt it myself.
{
    private ConsoleKeyInfo _key;
    private bool _isGameRunning = false;
    
    private static class Colors
    {
        public const string Magenta = "\e[35m";
        public const string Default = "\e[0m";
        public const string Red = "\e[31m";
        public const string Cyan = "\e[36m";
    }
    
    private int _left, _top;
    private PetCareManager _petCareManager = new PetCareManager();
    
    public void StartMenu()
    {
        _isGameRunning = true;
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
                string color = selectedOption == i ? Colors.Red : Colors.Default; // Changing the selected option color
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

        while (_isGameRunning)
        {
            int selection = ShowMenu("           Interactive Pet Simulator", menuItems, 
                "Use up and down arrow keys to navigate and enter to select.");
            
            menuItems[selection].Action.Invoke();
            
            // Exit the loop if user chose to exit
            if (selection == 2) break;
        }
    }

    private void ShowGameMenu()
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Adopt Pet", ShowAdoptPetMenu),
            new MenuItem("See Current Pets", ShowCurrentPetsMenu),
            new MenuItem("See Current Items", ShowCurrentItemsMenu),
            new MenuItem("Back To Main Menu", () => { /* Return to main menu */ })
        };

        bool stayInGameMenu = true;
        while (stayInGameMenu)
        {
            int selection = ShowMenu("Game Menu", menuItems);
            
            if (selection == 3) // Back to main menu
            {
                stayInGameMenu = false;
            }
            else
            {
                menuItems[selection].Action.Invoke();
            }
        }
    }

    private void ShowAdoptPetMenu()
    {
        var menuItems = new List<MenuItem>();
        
        // Add pets to menu
        foreach (var pet in PetInventoryManager.AdoptablePets)
        {
            menuItems.Add(new MenuItem(pet.Name, () => ConfirmAdoptPet(pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => {  }));

        bool stayInAdoptMenu = true;
        while (stayInAdoptMenu)
        {
            int selection = ShowMenu("Choose a Pet to Adopt", menuItems);
            
            if (selection == menuItems.Count - 1) // Back option
            {
                stayInAdoptMenu = false;
            }
            else
            {
                menuItems[selection].Action.Invoke();
                stayInAdoptMenu = false;
            }
        }
    }

    private void ConfirmAdoptPet(Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => AdoptPet(pet)),
            new MenuItem("No", () => {  })
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
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (var pet in PetInventoryManager.CurrentPets)
        {
            menuItems.Add(new MenuItem($"{pet.Name} | Hunger: {pet.Hunger} | Fun: {pet.Fun}", () => ShowPetOptions(pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => {  }));

        bool stayInPetsMenu = true;
        while (stayInPetsMenu)
        {
            int selection = ShowMenu("Your Current Pets", menuItems);
            
            if (selection == menuItems.Count - 1) // Back option
            {
                stayInPetsMenu = false;
            }
            else
            {
                menuItems[selection].Action.Invoke();
                stayInPetsMenu = false; // For refreshing the menu
            }
        }
    }

    private void ShowPetOptions(Pet pet)
    {
        Console.Clear();
        var menuItems = new List<MenuItem>();
        menuItems.Add(new MenuItem($"Feed {pet.Name}",() => {FeedPet(pet);}));
        menuItems.Add(new MenuItem($"Play with {pet.Name}",() => {PlayWithPet(pet);}));
        menuItems.Add(new MenuItem($"Use Item", () => {ShowCurrentItemsMenu(pet);}));
        menuItems.Add(new MenuItem($"Back", () => { }));
        
        bool stayInPetOptionsMenu = true;
        while (stayInPetOptionsMenu)
        {
            int selection = ShowMenu($"{pet.Name} | Hunger: {pet.Hunger} | Fun: {pet.Fun}", menuItems);
            if (selection == menuItems.Count - 1) // Back option
            {
                stayInPetOptionsMenu = false; 
                ShowCurrentPetsMenu(); // For refreshing the menu
            }
              
            else
                menuItems[selection].Action.Invoke();
            
        }
    }

    private void FeedPet(Pet pet)
    {
        _petCareManager.Feed(pet, 10);
    }

    private void PlayWithPet(Pet pet)
    {
        _petCareManager.Play(pet, 10);
    }
    

    private void ShowCurrentItemsMenu(Pet pet)
    {
        if (PetInventoryManager.CurrentItems.Count == 0)
        {
            Console.Clear();
            Console.WriteLine($"{Colors.Cyan}You don't have any items!{Colors.Default}");
            Console.WriteLine("Press any key to back to main menu...");
            Console.ReadKey();
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (var item in PetInventoryManager.CurrentItems)
        {
            menuItems.Add(new MenuItem($"{item.Name} {item.Description}", () => ConfirmUsingItem(item, pet)));
        }
        
        menuItems.Add(new MenuItem("Back", () => {  }));

        bool stayInPetsItemsMenu = true;
        while (stayInPetsItemsMenu)
        {
            int selection = ShowMenu("Your Current Items", menuItems);
            
            if (selection == menuItems.Count - 1) // Back option
            {
                stayInPetsItemsMenu = false;
                ShowPetOptions(pet);
            }
            else
            {
                menuItems[selection].Action.Invoke();
                stayInPetsItemsMenu = false;
            }
        }
    }
    
    private void ConfirmUsingItem(Item item,Pet pet)
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem("Yes", () => UseItem(item, pet)),
            new MenuItem("No", () => {  })
        };

        int selection = ShowMenu($"You will use {item.Name} {item.Description}", menuItems);
        menuItems[selection].Action.Invoke();
    }
    
    private void UseItem(Item item, Pet pet)
    {
        item.Use(pet);
        Console.Clear();
        Console.WriteLine($"{Colors.Cyan}You used {item.Name}!!{Colors.Default}");
        Console.WriteLine("Press any key to back to main menu...");
        Console.ReadKey();
        ShowCurrentItemsMenu(pet);
    }
    
    private void ShowCurrentItemsMenu()
    {
        if (PetInventoryManager.CurrentItems.Count == 0)
        {
            Console.Clear();
            Console.WriteLine($"{Colors.Cyan}You don't have any items!{Colors.Default}");
            Console.WriteLine("Press any key to back to main menu...");
            Console.ReadKey();
            return;
        }

        var menuItems = new List<MenuItem>();
        
        // Add current pets to menu
        foreach (var item in PetInventoryManager.CurrentItems)
        {
            menuItems.Add(new MenuItem($"{item.Name} {item.Description}", () => { })); 
        }
        
        
        menuItems.Add(new MenuItem("Back", () => {  }));

        bool stayInPetsItemsMenu = true;
        while (stayInPetsItemsMenu)
        {
            int selection = ShowMenu("Your Current Items", menuItems);
            
            if (selection == menuItems.Count - 1) // Back option
            {
                stayInPetsItemsMenu = false;
            }
            else
            {
                menuItems[selection].Action.Invoke();
                stayInPetsItemsMenu = false; // For refreshing the menu
            }
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
    }

    private void ExitGame()
    {
        Console.Clear();
        Console.WriteLine($"{Colors.Cyan}Made by Burak Bisneli.{Colors.Default}");
        _isGameRunning = false;
    }
}
