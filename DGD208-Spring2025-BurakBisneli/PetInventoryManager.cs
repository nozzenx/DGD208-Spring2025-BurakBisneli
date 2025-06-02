namespace DGD208_Spring2025_BurakBisneli;

public class PetInventoryManager
{
    public List<Pet> AdoptablePets = new List<Pet>();
    
    public List<Pet> CurrentPets = new List<Pet>();

    public PetInventoryManager()
    {
        AddPetsToAdoptablePetsForTest();
    }
    
    private void AddPetsToAdoptablePetsForTest()
    {
        Pet cat = new Pet("Cat");
        Pet dog = new Pet("Dog");
        Pet pig = new Pet("Pig");
        Pet rabbit = new Pet("Rabbit");
        Pet snake = new Pet("Snake");
        Pet turtle = new Pet("Turtle");
        Pet fish = new Pet("Fish");
        Pet horse = new Pet("Horse");
        Pet sheep = new Pet("Sheep");    
        Pet chicken = new Pet("Chicken");
        AdoptablePets.Add(cat);
        AdoptablePets.Add(dog);
        AdoptablePets.Add(pig);
        AdoptablePets.Add(rabbit);
        AdoptablePets.Add(snake);
        AdoptablePets.Add(turtle);
        AdoptablePets.Add(fish);
        AdoptablePets.Add(horse);
        AdoptablePets.Add(sheep);
        AdoptablePets.Add(chicken);
    }

    public void AdoptPet(Pet pet)
    {
        CurrentPets.Add(pet);
        AdoptablePets.Remove(pet);
    }
    
}