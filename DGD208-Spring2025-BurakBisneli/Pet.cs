namespace DGD208_Spring2025_BurakBisneli;

public class Pet
{
    private int _hunger;
    private int _sleep;
    private int _fun;


    public void IncreaseHunger(int amount)
    {
        _hunger += amount;
    }

    public void DecreaseHunger(int amount)
    {
        _hunger -= amount;
    }

    public void IncreaseSleep(int amount)
    {
        _sleep += amount;
    }

    public void DecreaseSleep(int amount)
    {
        _sleep -= amount;
    }

    public void IncreaseFun(int amount)
    {
        _fun += amount;
    }

    public void DecreaseFun(int amount)
    {
        _fun -= amount;
    }
}