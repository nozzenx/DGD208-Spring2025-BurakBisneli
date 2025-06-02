namespace DGD208_Spring2025_BurakBisneli;

public class Pet
{
    public string Name { get; private set; }
    
    public int Hunger { get; private set; } = 50;
    private int _sleep = 50;
    public int Fun = 50;

    public Pet(string name)
    {
        Name = name;
    }

    public void Talk()
    {
        Console.WriteLine($"{Name}");
        Console.WriteLine($"HUNGER : {Hunger}");
        Console.WriteLine($"SLEEP : {_sleep}");
        Console.WriteLine($"FUN : {Fun}");
    }
    
    public void IncreaseHunger(int amount)
    {
        if(Hunger + amount <= 100)
            Hunger += amount;
        else
            Hunger = 100;
        
    }

    public void DecreaseHunger(int amount)
    {
        if(Hunger - amount >= 0)
            Hunger -= amount;
        else
            Hunger = 0;
        
    }

    public void IncreaseSleep(int amount)
    {
        if(_sleep + amount <= 100)
            _sleep += amount;
        else
            _sleep = 100;
        
    }

    public void DecreaseSleep(int amount)
    {
        if(_sleep - amount >= 0)
            _sleep -= amount;
        else
            _sleep = 0;
        
    }   

    public void IncreaseFun(int amount)
    {
        if(Fun + amount <= 100)
            Fun += amount;
        else
            Fun = 100;
        
    }

    public void DecreaseFun(int amount)
    {
        if(Fun - amount >= 0)
            Fun -= amount;
        else
            Fun = 0;
        
    }
}