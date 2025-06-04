namespace DGD208_Spring2025_BurakBisneli;

public static class PetInventoryManager
{
    public static List<Pet> CurrentPets = new List<Pet>();
    

    static PetInventoryManager()
    {
        
    }
    
    

    public static void AdoptPet(Pet pet)
    {
        CurrentPets.Add(pet);
        PetDatabase.Pets.Remove(pet);
    }
    
}