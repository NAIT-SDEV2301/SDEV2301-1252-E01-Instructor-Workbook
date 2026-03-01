using Microsoft.EntityFrameworkCore;
using PokemonPractice.Data.Data;
using PokemonPractice.Data.Services;
using PokemonPractice.Data.Models;

static string PromptString(string label)
{
    bool validInput = false;
    string value = string.Empty;
    while (!validInput)
    {
        Console.Write(label);
        // Return an empty string if there is no input value
        value = (Console.ReadLine() ?? "").Trim();
        validInput = string.IsNullOrWhiteSpace(value) ? false : true;
        if (!validInput)
        {
            Console.WriteLine("Please enter a value.");
            Console.WriteLine(label);
        }
    }
    return value;
}

static int PromptInt(string label)
{
    bool validInput = false;
    int value = 0;
    while (!validInput)
    {
        var stringInput = PromptString(label);
        if (int.TryParse(stringInput, out value))
        {
            validInput = true;
        }
        else
        {
            Console.WriteLine($"Please enter a number.");
        }

    }
    return value;
}

static int PromptIntRange(string label, int minValue, int maxValue)
{
    bool validInput = false;
    int value = 0;
    while (!validInput)
    {
        value = PromptInt(label);
        if (value >= minValue && value <= maxValue)
        {
            validInput = true;
        }
        else
        {
            Console.WriteLine($"Please enter a number between {minValue} and {maxValue}.");
        }
        
    }
    return value;
}

static void ListPokemonsWithTypes(PokemonService service)
{
    // Display list of pokemon with types
    var pokemons = service.ListPokemonWithTypes();
    foreach (var pokemon in pokemons)
    {
        var typeNames = pokemon.Types.Select(t => t.Name);
        Console.WriteLine($"{pokemon.Name} - Types: {string.Join(", ", typeNames)}");
    }
}

static void ListPokemons(PokemonService service)
{
    // Display list of pokemon 
    Console.WriteLine("Pokémons:");
    var pokemons = service.ListPokemons();
    foreach (var pokemon in pokemons)
    {
        // Use the overridden ToString() method of the Pokemon class to display its details.
        Console.WriteLine($"{pokemon}");
    }
}

static void ListPoketypes(PokeTypeService service)
{
    // Display list of pokeypes 
    Console.WriteLine("PokéTypes:");
    var poketypes = service.ListPokeTypes();
    foreach (var type in poketypes)
    {
        // Use the overridden ToString() method of the Poketype class to display its details.
        Console.WriteLine($"{type}");
    }
}

static Pokemon? AddPokemon(PokemonService service, string pokemon)
{
    try
    {
        return service.AddPokemon(pokemon);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return null;
}

static PokeType? AddPokeType(PokeTypeService service, string poketype)
{
    try
    {
        return service.AddType(poketype);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return null;
}

static void LinkTypeToPokemon(PokemonTypeService service, int pokemonId, int typeId)
{
    try
    {
        service.LinkTypeToPokemon(pokemonId, typeId);
        Console.WriteLine("Type linked to Pokémon successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void UnLinkTypeFromPokemon(PokemonTypeService service, int pokemonId, int typeId)
{
    try
    {
        service.UnlinkTypeFromPokemon(pokemonId, typeId);
        Console.WriteLine("Type unlinked from Pokémon successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// Force SQLite to use a single database file in the project root.
// Prevents "no such table" errors caused by different working
// directories between `dotnet run` and Visual Studio.
var dbPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "pokemon.db");
dbPath = Path.GetFullPath(dbPath);

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite($"Data Source={dbPath}")
    .Options;

using var db = new AppDbContext(options);
db.Database.EnsureCreated();

var pokemonService = new PokemonService(db);
var typeService = new PokeTypeService(db);
var linkService = new PokemonTypeService(db);
var databaseSeeder = new DatabaseSeeder(db,pokemonService, typeService, linkService);

// Seeds the database with initial data if it's empty. This is a safe operation to call on every run.
databaseSeeder.SeedIfEmpty();

var MainMenuPrompt = """
Pokemon Types Application
-------------------------
1. Add Pokémon
2. Add Type
3. Link Type to Pokémon
4. Unlink Type from Pokémon
5. List Pokémon with Types
6. List Pokémons
7. List PokeTypes
0. Exit
Please enter your choice (0-7):
""";

var exitProgram = false;
const int MinMenuChoice = 0;
const int MaxMenuChoice = 7;
int menuChoice = -1;
while (!exitProgram)
{
    menuChoice = PromptIntRange(MainMenuPrompt, MinMenuChoice, MaxMenuChoice);
    switch(menuChoice)
    {
        case 1:
            var pokemonName = PromptString("Enter Pokémon name: ");
            AddPokemon(pokemonService, pokemonName);
            break;
        case 2:
            var typeName = PromptString("Enter Type name: ");
            AddPokeType(typeService, typeName);
            break;
        case 3:
            ListPokemons(pokemonService);
            var pokemonIdToLink = PromptInt("Enter Pokémon ID to link: ");
            ListPoketypes(typeService);
            var typeIdToLink = PromptInt("Enter Type ID to link: ");
            LinkTypeToPokemon(linkService, pokemonIdToLink, typeIdToLink);
            break;
        case 4:
            ListPokemons(pokemonService);
            var pokemonIdToUnlink = PromptInt("Enter Pokémon ID to unlink: ");
            ListPoketypes(typeService);
            var typeIdToUnlink = PromptInt("Enter Type ID to unlink: ");
            UnLinkTypeFromPokemon(linkService, pokemonIdToUnlink, typeIdToUnlink);
            break;
        case 5:
            ListPokemonsWithTypes(pokemonService);
            break;
        case 6:
            ListPokemons(pokemonService);
            break;
        case 7:
            ListPoketypes(typeService);
            break;
        case 0:
            exitProgram = true;
            Console.WriteLine("Exiting program. Goodbye!");
            break;
    }
    Console.WriteLine();
    Console.WriteLine("Press any key to continue.");
    Console.ReadKey();
    Console.Clear();
}
