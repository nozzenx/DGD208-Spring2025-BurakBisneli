namespace DGD208_Spring2025_BurakBisneli;

public class Menu // I used (https://www.youtube.com/watch?v=YyD1MRJY0qI) this tutorial to achieve this menu.
{
    private bool _isSelected;
    
    private ConsoleKeyInfo _key;
    
    private const string ColorMagenta = "\e[35m";
    private const string DefaultColor = "\e[0m";
    private const string ColorRed = "\e[31m";
    private const string ColorCyan = "\e[36m";

    private int _option = 1;
    
    public void Start()
    {
        
        Console.WriteLine($"{ColorMagenta} Interactive Pet Simulator {DefaultColor}");
        Console.WriteLine($"\n{ColorCyan}Use up and down arrow keys to navigate and enter to select.{DefaultColor}");
        (int left, int top) = Console.GetCursorPosition();

        while (!_isSelected)
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
                    _isSelected = true;
                    break;
            }
        }
        Console.WriteLine($"Your option is {_option}");
    }
}