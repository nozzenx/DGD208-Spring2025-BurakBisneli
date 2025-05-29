using DGD208_Spring2025_BurakBisneli;

var menu = new Menu();
var petCareManager = new PetCareManager();
var pet = new Pet("Ghost");


// menu.Start();



async Task Main()
{
    menu.StartMenu();
    pet.Talk();

    await petCareManager.Feed(pet, 10);
}

await Main();
