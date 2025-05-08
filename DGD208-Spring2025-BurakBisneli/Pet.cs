using System.Diagnostics;

namespace DGD208_Spring2025_BurakBisneli;

public class Pet
{
    private string _name;
    
    private int _hunger = 50;
    private int _sleep = 50;
    private int _fun = 50;

    public Pet(string name)
    {
        _name = name;
    }

    public void Talk()
    {
        Console.WriteLine($"{_name}");
    }
    
    public void IncreaseHunger(int amount)
    {
        if(_hunger + amount <= 100)
            _hunger += amount;
        else
            _hunger = 100;
        
    }

    public void DecreaseHunger(int amount)
    {
        if(_hunger - amount >= 0)
            _hunger -= amount;
        else
            _hunger = 0;
        
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
        if(_fun + amount <= 100)
            _fun += amount;
        else
            _fun = 100;
        
    }

    public void DecreaseFun(int amount)
    {
        if(_fun - amount >= 0)
            _fun -= amount;
        else
            _fun = 0;
        
    }
}