namespace DGD208_Spring2025_BurakBisneli;

public abstract class Item
{
    public readonly string Name;
    public readonly string Description;
    protected int HungerAmount;
    protected int FunAmount;
    protected int UsingTime;


    protected Item(string name, string description, int hungerAmount, int funAmount, int usingTime)
    {
        Name = name;
        Description = description;
        HungerAmount = hungerAmount;
        FunAmount = funAmount;
        UsingTime = usingTime;
    }
    

    public virtual void Use(Pet pet)
    {
        PetInventoryManager.CurrentItems.Remove(this);
    }
}