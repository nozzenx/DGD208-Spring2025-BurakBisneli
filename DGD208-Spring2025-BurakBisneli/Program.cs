using DGD208_Spring2025_BurakBisneli;

var menu = new Menu();


async Task Main()
{
    PetCareManager.StartStatDecay();
    
    menu.StartMenu(); 
}

await Main();
