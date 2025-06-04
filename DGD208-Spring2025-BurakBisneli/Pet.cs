namespace DGD208_Spring2025_BurakBisneli;

public class Pet
{
    // Name of the item, to be displayed in the game
    public string Name { get; set; }
	
    // Item type, determines which situations it can be used in
    public PetType Type { get; set; }

    public int Hunger { get; set; } = 50;
    public int Sleep { get; set; } = 50;
    public int Fun { get; set; } = 50;
}