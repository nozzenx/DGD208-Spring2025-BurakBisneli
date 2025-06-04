namespace DGD208_Spring2025_BurakBisneli;

public static class PetCareManager // I used some Claude Sonnet 4 for making this async stat decreasing.
{
    
    private static bool _isActionInProgress = false;
    
    public static bool IsActionInProgress => _isActionInProgress;

    
    private static System.Timers.Timer _statDecayTimer;
    private static bool _isStatDecayRunning = false;
    
    public static bool IsStatDecayRunning => _isStatDecayRunning;

    
    public static void StartStatDecay()
    {
        if (_isStatDecayRunning) return;
        
        _statDecayTimer = new System.Timers.Timer(3000); // 3 seconds
        _statDecayTimer.Elapsed += OnStatDecayTimer;
        _statDecayTimer.Start();
        _isStatDecayRunning = true;
        
        Console.WriteLine("Automatic stat decay started!");
    }
    
    
    public static void StopStatDecay()
    {
        _statDecayTimer?.Stop();
        _statDecayTimer?.Dispose();
        _isStatDecayRunning = false;
        
        Console.WriteLine("Automatic stat decay stopped!");
    }
    
  
    private static void OnStatDecayTimer(object sender, System.Timers.ElapsedEventArgs e)
    {
       
        foreach (Pet pet in PetInventoryManager.CurrentPets)
        {
            ChangeStats(pet, -1, -1, -1);
        }
        
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
        Console.Write($"[{DateTime.Now:HH:mm:ss}] All pets' stats decreased by 1");
    }

    
    
    // Modified to be async and include time delays
    public static async Task<bool> UseItem(Pet pet, Item item)
    {
        if (_isActionInProgress)
        {
            Console.WriteLine("Another action is already in progress!");
            return false;
        }

        _isActionInProgress = true;
        
        try
        {
            Console.WriteLine($"Using {item.Name} on {pet.Name}... (Duration: {item.Duration}s)");
            
            // Wait for the action duration
            int duration = Convert.ToInt32(item.Duration);
            await Task.Delay(duration * 1000);

            switch (item.AffectedStat)
            {
                case PetStat.Hunger :
                    ChangeStats(pet, item.EffectAmount, 0, 0);
                    break;
                case PetStat.Fun:
                    ChangeStats(pet, 0, 0, item.EffectAmount);
                    break;
                case PetStat.Sleep:
                    ChangeStats(pet, 0, item.EffectAmount, 0);
                    break;
            }
            
            Console.WriteLine($"Used {item.Name} successfully!");
            
            return true;
        }
        finally
        {
            _isActionInProgress = false;
        }
    }

    // Keep the synchronous version for immediate stat changes
    public static void ChangeStats(Pet pet, int hunger, int sleep, int fun)
    {
        pet.Hunger = Math.Clamp(pet.Hunger + hunger, 0, 100);
        pet.Sleep = Math.Clamp(pet.Sleep + sleep, 0, 100);
        pet.Fun = Math.Clamp(pet.Fun + fun, 0, 100);
    }
    
    // Add async methods for feeding and playing
    public static async Task<bool> FeedPet(Pet pet, int durationSeconds = 3)
    {
        if (_isActionInProgress)
        {
            Console.WriteLine("Another action is already in progress!");
            return false;
        }

        _isActionInProgress = true;
        
        try
        {
            Console.WriteLine($"Feeding {pet.Name}... (Duration: {durationSeconds}s)");
            await Task.Delay(durationSeconds * 1000);
            
            // Apply feeding effects
            ChangeStats(pet, 30, 0, 5);
            Console.WriteLine($"Fed {pet.Name} successfully!");
            
            return true;
        }
        finally
        {
            _isActionInProgress = false;
        }
    }
    
    public static async Task<bool> PlayWithPet(Pet pet, int durationSeconds = 3)
    {
        if (_isActionInProgress)
        {
            Console.WriteLine("Another action is already in progress!");
            return false;
        }

        _isActionInProgress = true;
        
        try
        {
            Console.WriteLine($"Playing with {pet.Name}... (Duration: {durationSeconds}s)");
            await Task.Delay(durationSeconds * 1000);
            
            // Apply playing effects
            ChangeStats(pet, -5, -10, 25);
            Console.WriteLine($"Played with {pet.Name} successfully!");
            
            return true;
        }
        finally
        {
            _isActionInProgress = false;
        }
    }
}