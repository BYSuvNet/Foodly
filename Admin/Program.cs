using System.Net.Http.Json;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5000");

var menu = await client.GetFromJsonAsync<List<MenuItem>>("/dishes") ?? new List<MenuItem>();

Console.WriteLine("-- MENU --\n");
foreach (var item in menu)
{
    Console.WriteLine($"{item.Id}: {item.Name} - {item.Price} kr");
}
Console.WriteLine("\n");

while (true)
{
    Console.Write("New dish> ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Price> ");
    float price = float.Parse(Console.ReadLine() ?? "0");

    MenuItem mi = new MenuItem() { Name = name, Price = price };

    var result = await client.PostAsJsonAsync("/dishes", mi);

    Console.WriteLine("\nDish added. Press a key to continue...");
    Console.ReadLine();
}


class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
}