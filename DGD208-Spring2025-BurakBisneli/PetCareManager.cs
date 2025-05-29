namespace DGD208_Spring2025_BurakBisneli;

public class PetCareManager
{
    public static PetCareManager Instance;

    public PetCareManager()
    {
        Instance = this;
    }

    public async Task Feed(Pet pet, int amount)
    {
        Console.WriteLine($"Feeding {pet.Name}.");
        pet.IncreaseHunger(amount);
        await Task.Delay(5000);
        Console.WriteLine($"{pet.Name} ate food. Current Hunger: {pet.Hunger}");
    }
}