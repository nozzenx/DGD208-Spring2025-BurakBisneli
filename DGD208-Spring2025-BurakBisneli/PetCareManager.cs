namespace DGD208_Spring2025_BurakBisneli;

public class PetCareManager
{
    public async Task Feed(Pet pet, int amount)
    {
        Console.WriteLine($"Feeding {pet.Name}.");
        pet.IncreaseHunger(amount);
        await Task.Delay(5000);
        Console.WriteLine($"{pet.Name} ate food. Current Hunger: {pet.Hunger}");
    }
    
    public async Task Play(Pet pet, int amount)
    {
        Console.WriteLine($"Playing with {pet.Name}.");
        pet.IncreaseFun(amount);
        await Task.Delay(5000);
        Console.WriteLine($"You played with {pet.Name}. Current Fun: {pet.Fun}");
    }
}