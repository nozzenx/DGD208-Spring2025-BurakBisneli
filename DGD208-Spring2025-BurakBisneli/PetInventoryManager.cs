namespace DGD208_Spring2025_BurakBisneli;

public static class PetInventoryManager
{
    public static List<Pet> AdoptablePets = new List<Pet>();
    
    public static List<Pet> CurrentPets = new List<Pet>();
    

    static PetInventoryManager()
    {
        AddItemAndPetsForTest();
    }
    
    private static void AddItemAndPetsForTest()
    {
        
    }

    public static void AdoptPet(Pet pet)
    {
        CurrentPets.Add(pet);
        AdoptablePets.Remove(pet);
    }
    
}