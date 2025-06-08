using System.ComponentModel;

namespace DGD208_Spring2025_BurakBisneli;

public static class PetCareManager // I used some Claude Sonnet 4 for making this async stat decreasing.
{
    

    
    
    // Modified to be async and include time delays
    public static async Task UseItem(Pet pet, Item item)
    {
        int durationRounded = Convert.ToInt32(Math.Round(item.Duration));
        await Task.Delay(durationRounded * 1000);
        switch (item.AffectedStat)
        {
            case PetStat.Fun:
                ChangeStats(pet, 0, 0, item.EffectAmount);
                break;
            case PetStat.Hunger:
                ChangeStats(pet, item.EffectAmount, 0, 0);
                break;
            case PetStat.Sleep:
                ChangeStats(pet, 0, item.EffectAmount, 0);
                break;
        }
    }

    // Keep the synchronous version for immediate stat changes
    public static void ChangeStats(Pet pet, int hunger, int sleep, int fun)
    {
        pet.Hunger = Math.Clamp(pet.Hunger + hunger, 0, 100);
        pet.Sleep = Math.Clamp(pet.Sleep + sleep, 0, 100);
        pet.Fun = Math.Clamp(pet.Fun + fun, 0, 100);
    }
    
    // Add async methods for feeding and playing
    public static async Task FeedPet(Pet pet, int durationSeconds = 3)
    {
        
        await Task.Delay(durationSeconds * 1000);
        pet.Hunger += 20;
        pet.Sleep -= 10;
        
    }
    
    public static async Task PlayWithPet(Pet pet, int durationSeconds = 3)
    {
        await Task.Delay(durationSeconds * 1000);
        pet.Fun += 20;
        pet.Hunger -= 15;
        pet.Sleep -= 15;
        Console.WriteLine("\n\n\nPet played.");
    }
}