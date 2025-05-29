using System.Diagnostics;

namespace DGD208_Spring2025_BurakBisneli;

public class Menu // I used (https://www.youtube.com/watch?v=YyD1MRJY0qI) this tutorial to achieve this menu.
{
    private bool _isSelectedInMainMenu;
    private bool _isSelectedInPetsMenu;
    
    private ConsoleKeyInfo _key;
    
    private const string ColorMagenta = "\e[35m";
    private const string DefaultColor = "\e[0m";
    private const string ColorRed = "\e[31m";
    private const string ColorCyan = "\e[36m";

    private int _option = 1;
    
    public void StartMenu()
    {
        
        Console.WriteLine($"{ColorMagenta} Interactive Pet Simulator {DefaultColor}");
        Console.WriteLine($"\n{ColorCyan}Use up and down arrow keys to navigate and enter to select.{DefaultColor}");
        (int left, int top) = Console.GetCursorPosition();
        MainMenu(left, top);
        
        Console.WriteLine($"Your option is {_option}");
    }

    private void MainMenu(int left, int top)
    {
        while (!_isSelectedInMainMenu)
        {
            Console.SetCursorPosition(left, top);
            
            
            Console.WriteLine($"{(_option == 1 ? ColorRed : DefaultColor)}Adopt new pet{DefaultColor}");
            Console.WriteLine($"{(_option == 2 ? ColorRed : DefaultColor)}See current pets{DefaultColor}");
            Console.WriteLine($"{(_option == 3 ? ColorRed : DefaultColor)}Credits{DefaultColor}");
            
            _key = Console.ReadKey(false);

            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    _option = (_option == 3 ? 1 : _option + 1);
                    break;
                case ConsoleKey.UpArrow:
                    _option = (_option == 1 ? 3 : _option - 1);
                    break;
                case ConsoleKey.Enter:
                    _isSelectedInMainMenu = true;
                    
                    switch (_option)
                    {
                        case 1:
                            Console.WriteLine($"You are on adopt pet menu");
                            break;
                        case 2:
                            Console.WriteLine($"You are on see current pets menu");
                            Console.Clear();
                            SeeCurrentPetsMenu(left, top);
                            break;
                        case 3:
                            Console.WriteLine($"You are on credits menu");
                            break;
                    }
                    break;
            }
        }
    }

    private void SeeCurrentPetsMenu(int left, int top)
    {
        Console.WriteLine($"{ColorMagenta} PETS {DefaultColor}");
        
        while (!_isSelectedInPetsMenu)
        {
            Console.SetCursorPosition(left, top);
            
            
            Console.WriteLine($"{(_option == 1 ? ColorRed : DefaultColor)}Ghost{DefaultColor}");
            Console.WriteLine($"{(_option == 2 ? ColorRed : DefaultColor)}Max{DefaultColor}");
            Console.WriteLine($"{(_option == 3 ? ColorRed : DefaultColor)}Oppo{DefaultColor}");
            
            _key = Console.ReadKey(false);

            switch (_key.Key)
            {
                case ConsoleKey.DownArrow:
                    _option = (_option == 3 ? 1 : _option + 1);
                    break;
                case ConsoleKey.UpArrow:
                    _option = (_option == 1 ? 3 : _option - 1);
                    break;
                case ConsoleKey.Enter:
                    _isSelectedInPetsMenu = true;
                    
                    switch (_option)
                    {
                        case 1:
                            Console.WriteLine($"You are on adopt pet menu");
                            break;
                        case 2:
                            Console.WriteLine($"You are on see current pets menu");
                            break;
                        case 3:
                            Console.WriteLine($"You are on credits menu");
                            break;
                    }
                    break;
            }
        }
    }
}