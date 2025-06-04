namespace DGD208_Spring2025_BurakBisneli;

public abstract class Item
{
    protected int HungerAmount;
    protected int FunAmount;
    protected int UsingTime;


    protected Item(int hungerAmount, int funAmount, int usingTime)
    {
        HungerAmount = hungerAmount;
        FunAmount = funAmount;
        UsingTime = usingTime;
    }
    

    public virtual void Use(Pet pet)
    {
        PetInventoryManager.CurrentItems.Remove(this);
    }
}