using System.Diagnostics;

namespace DGD208_Spring2025_BurakBisneli;

public class Menu // I used (https://www.youtube.com/watch?v=YyD1MRJY0qI) this tutorial to achieve this menu.
{
    private ConsoleKeyInfo _key;
    
    private const string ColorMagenta = "\e[35m";
    private const string DefaultColor = "\e[0m";
    private const string ColorRed = "\e[31m";
    private const string ColorCyan = "\e[36m";
    
    private int _left, _top;
    

    

    private PetInventoryManager _petInventoryManager = new PetInventoryManager();
    
    public void StartMenu()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        int option = 1;
        Console.Clear();
        Console.WriteLine($"{ColorMagenta}           Interactive Pet Simulator {DefaultColor}");
        Console.WriteLine($"{ColorCyan}Use up and down arrow keys to navigate and enter to select.\n{DefaultColor}");
        (int left, int top) = Console.GetCursorPosition();
        _left = left;
        _top = top;
        
        bool isSelectedInMenu = false;
        while (!isSelectedInMenu)
        {
            Console.SetCursorPosition(_left, _top);
            
            
            Console.WriteLine($"{(option == 1 ? ColorRed : DefaultColor)}Start Game{DefaultColor}");
            Console.WriteLine($"{(option == 2 ? ColorRed : DefaultColor)}Credits{DefaultColor}");
            Console.WriteLine($"{(option == 3 ? ColorRed : DefaultColor)}Exit Game{DefaultColor}");
            
            _key = Console.ReadKey(false);

            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    option = (option == 3 ? 1 : option + 1);
                    break;
                case ConsoleKey.UpArrow:
                    option = (option == 1 ? 3 : option - 1);
                    break;
                case ConsoleKey.Enter:
                    isSelectedInMenu = true;
                    
                    switch (option)
                    {
                        case 1:
                            // This starts the game.
                            StartGameMenu();
                            break;
                        case 2:
                            // This is credit.
                            break;
                        case 3:
                            break;
                    }
                    break;
            }
        }
    }


    private void StartGameMenu()
    {
        int option = 1;
        Console.Clear();
        bool isSelectedInMenu = false;
        // _isSelectedInGameMenu = false;
        while (!isSelectedInMenu)
        {
            Console.SetCursorPosition(_left, _top);
            
            Console.WriteLine($"{(option == 1 ? ColorRed : DefaultColor)}Adopt Pet{DefaultColor}");
            Console.WriteLine($"{(option == 2 ? ColorRed : DefaultColor)}See Current Pets{DefaultColor}");
            Console.WriteLine($"{(option == 3 ? ColorRed : DefaultColor)}See Current Items\n{DefaultColor}");
            Console.WriteLine($"{(option == 4 ? ColorRed : DefaultColor)}Back To Main Menu{DefaultColor}");
            
            _key = Console.ReadKey(false);
            
            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    option = (option == 4 ? 1 : option + 1);
                    break;
                case ConsoleKey.UpArrow:
                    option = (option == 1 ? 4 : option - 1);
                    break;
                case ConsoleKey.Enter:
                    isSelectedInMenu = true;
                    
                    switch (option)
                    {
                        case 1:
                            //adopt pet menu
                            AdoptingPetMenu();
                            break;
                        case 2:
                            //see current pets menu
                            break;
                        case 3:
                            //see current items menu
                            break;
                        case 4:
                            MainMenu();
                            break;
                    }
                    break;
            }
        }
    }
    
    private void AdoptingPetMenu()
    {
        int option = 1;
        Console.Clear();
        bool isSelectedInMenu = false;
        while (!isSelectedInMenu)
        {
            Console.SetCursorPosition(_left, _top);
            
            // Add every adoptable pet to the menu.
            for (int i = 1; i < _petInventoryManager.AdoptablePets.Count; i++)
            {
                Console.WriteLine($"{(option == i ? ColorRed : DefaultColor)}{_petInventoryManager.AdoptablePets[i].Name}{DefaultColor}");
            }
            Console.WriteLine($"{(option == _petInventoryManager.AdoptablePets.Count ? ColorRed : DefaultColor)}Back{DefaultColor}");
            
            _key = Console.ReadKey(false);
            
            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    option = (option == _petInventoryManager.AdoptablePets.Count ? 1 : option + 1);
                    break;
                case ConsoleKey.UpArrow:
                    option = (option == 1 ? _petInventoryManager.AdoptablePets.Count : option - 1);
                    break;
                case ConsoleKey.Enter:
                    isSelectedInMenu = true;
                    
                    if(option >= _petInventoryManager.AdoptablePets.Count)
                        StartGameMenu();
                    
                    for (int i = 1; i < _petInventoryManager.AdoptablePets.Count; i++)
                    {
                        if (option == i)
                        {
                            Console.WriteLine($"You get {_petInventoryManager.AdoptablePets[i].Name}");
                        }
                    }
                    break;
            }
        }
    }
}