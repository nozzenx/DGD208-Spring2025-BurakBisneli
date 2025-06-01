namespace DGD208_Spring2025_BurakBisneli;

public class PetInventoryManager
{
    public List<Pet> CurrentPets = new List<Pet>();
    
    
    public void AddPet(Pet pet)
    {
        CurrentPets.Add(pet);
    }
    
    public void RemovePet(Pet pet)
    {
        CurrentPets.Remove(pet);
    }
}