using Lab6v15;

Console.OutputEncoding = System.Text.Encoding.UTF8;


Clothing clothing = new Clothing("Бавовна", "M");
Shirt shirt = new Shirt("Льон", "L", 62.5);
Pants pants = new Pants("Джинс", "L", 84);


Console.WriteLine("=== Унікальні методи похідних класів ===");
shirt.ButtonUp();
pants.ZipUp();
Console.WriteLine();


Console.WriteLine("=== Поліморфізм (virtual / override) ===");
Clothing[] wardrobe = { clothing, shirt, pants };

foreach (Clothing item in wardrobe)
{
    Console.WriteLine($"--- Тип об'єкта: {item.GetType().Name} ---");
    item.Wear(); 
    Console.WriteLine();
}


Console.WriteLine("=== Різниця між override та new ===");

Clothing shirtAsClothing = shirt; 
Shirt shirtAsShirt = shirt;       

Console.WriteLine("Метод з override (Wear) через посилання Clothing:");
shirtAsClothing.Wear(); 

Console.WriteLine();
Console.WriteLine("Метод з new (GetClothingType):");
Console.WriteLine($"Через посилання Clothing -> {shirtAsClothing.GetClothingType()}"); // Clothing.GetClothingType()
Console.WriteLine($"Через посилання Shirt    -> {shirtAsShirt.GetClothingType()}");    // Shirt.GetClothingType()

Console.WriteLine();
Clothing pantsAsClothing = pants;
Console.WriteLine("Pants не приховує метод, тому результат однаковий:");
Console.WriteLine($"Через посилання Clothing -> {pantsAsClothing.GetClothingType()}");
Console.WriteLine($"Через посилання Pants    -> {pants.GetClothingType()}");