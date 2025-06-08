namespace DGD208_Spring2025_BurakBisneli;

public static class PetStatDecreaser
{
    private static void ChangeStats(Pet pet, int hunger, int sleep, int fun)
    {
        pet.Hunger += hunger;
        pet.Sleep += sleep;
        pet.Fun += fun;
    }

    
    
    public static async Task DecreasePetsStatsWithTime(Pet pet)
    {
        while (pet is { Hunger: > 0, Sleep: > 0, Fun: > 0 })
        {
            string lastMessage = string.Empty;
            ChangeStats(pet, -1, -1, -1);
            
            lastMessage = "Pet stats decreased 1";
            
            await Task.Delay(1000);
            // Console.Write("\r" + new string(' ', lastMessage.Length)); // This line is for blinking text. 
            await Task.Delay(1000);
            // Console.Write("\r" + lastMessage);
        }
        Console.WriteLine($"!!{pet.Name} is dead.!!");
        PetInventoryManager.CurrentPets.Remove(pet);
    }
}