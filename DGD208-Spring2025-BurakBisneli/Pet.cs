namespace DGD208_Spring2025_BurakBisneli;

public class Pet
{
    public Action? OnAdopted { get; set; }

    public Pet()
    {
        OnAdopted += async void () => await PetStatDecreaser.DecreasePetsStatsWithTime(this);
    }
       
    public string Name { get; set; } = "Default";
    public PetType Type { get; set; }

    private int _hunger = 50;
    private int _sleep = 50;
    private int _fun = 50;

    // Add event for stat changes

    public int Hunger 
    { 
        get => _hunger; 
        set => _hunger = Math.Clamp(value, 0, 100);
    }
    
    public int Sleep 
    { 
        get => _sleep; 
        set => _sleep = Math.Clamp(value, 0, 100);
    }
    
    public int Fun 
    { 
        get => _fun; 
        set => _fun = Math.Clamp(value, 0, 100);
    }
}