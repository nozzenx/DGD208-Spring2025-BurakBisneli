namespace DGD208_Spring2025_BurakBisneli.Items;

public class Comb : Item
{
    public Comb(int hungerAmount, int funAmount, int usingTime) : base(hungerAmount, funAmount, usingTime)
    {
    }

    public override void Use(Pet pet)
    {
        pet.IncreaseFun(15);
        base.Use(pet);
    }
}