using Module02.Lesson13.PokemonExercise;

var pokedex = new List<Pokemon>
{
    new(  1, "Bulbasaur",  "Grass",  "Poison", 49, 49, 45, 318, false),
    new(  4, "Charmander", "Fire",   null,     52, 43, 65, 309, false),
    new(  7, "Squirtle",   "Water",  null,     48, 65, 43, 314, false),
    new( 25, "Pikachu",    "Electric",null,    55, 40, 90, 320, false),
    new( 39, "Jigglypuff", "Normal", "Fairy",  45, 20, 20, 270, false),
    new( 52, "Meowth",     "Normal", null,     45, 35, 90, 290, false),
    new( 63, "Abra",       "Psychic",null,     20, 15, 90, 310, false),
    new( 92, "Gastly",     "Ghost",  "Poison", 35, 30, 80, 310, false),
    new( 95, "Onix",       "Rock",   "Ground", 45,160, 70, 385, false),
    new(129, "Magikarp",   "Water",  null,     10, 55, 80, 200, false),
    new(131, "Lapras",     "Water",  "Ice",    85, 80, 60, 535, false),
    new(133, "Eevee",      "Normal", null,     55, 50, 55, 325, false),
    new(143, "Snorlax",    "Normal", null,    110, 65, 30, 540, false),
    new(149, "Dragonite",  "Dragon", "Flying",134, 95, 80, 600, false),
    new(150, "Mewtwo",     "Psychic",null,    110, 90,130, 680, true),
    new(151, "Mew",        "Psychic",null,    100,100,100, 600, true),
    new(245, "Suicune",    "Water",  null,     75,115, 85, 580, true),
    new(248, "Tyranitar",  "Rock",   "Dark",  134,110, 61, 600, false),
    new(384, "Rayquaza",   "Dragon", "Flying",150, 90, 95, 680, true),
    new(445, "Garchomp",   "Dragon", "Ground",130, 95,102, 600, false),
};

/*
 * A. Filtering (Where)
All Pokémon with Type1 == "Water"
All Pokémon that are IsLegendary == true
All Pokémon with Speed >= 90
All Pokémon with Total < 320
All Pokémon that have a second type (Type2 != null)
 * */
var pokemonWater = pokedex.Where(p => p.Type1 == "Water");
PrintAllPokemon("All Pokémon with Type1 == Water", pokemonWater);
var legend = pokedex.Where(p => p.IsLegendary);
var pokemonSpeed = pokedex.Where(poke => poke.Speed >= 90);
var pokemon320 = pokedex.Where(p => p.Total < 320);
var second = pokedex.Where(p => p.Type2 != null);

/*
 * B. Projection (Select)
A sequence of names only
A sequence of Dex numbers only
A sequence of Type1 values only
*/
var pokemonNames = pokedex.Select(p => p.Name);

var pokemonDexNumbers = pokedex.Select(p => p.Dex);

var pokemonType1 = pokedex.Select(p => p.Type1);


/*
C. Filter + Project (Chaining)
Names of Pokémon with Attack >= 120
Names of Pokémon where Type1 == "Dragon"
Names + Speed (as a single string like "Pikachu - 90") for Pokémon with Speed >= 90
Names of Pokémon that are Water type with Total >= 500
*/
var attack = pokedex.Where(p => p.Attack >= 120).Select(p => p.Name);
var dragon = pokedex.Where(p => p.Type1 == "Dragon").Select(p => p.Name);
var nameSpeed = pokedex.Where(p => p.Speed >= 90)
    .Select(p => $"{p.Name} - {p.Speed}");
var watertypes = pokedex
    //.Where(p => p.Total >= 500)
    //.Where(p => p.Type1 == "Water")
    .Where(p => p.Total >= 500 && p.Type1 == "Water")
    .Select(p => p.Name);
static void PrintAllPokemon(string title, IEnumerable<Pokemon> pokedex)
{
    Console.Clear();
    Console.WriteLine(title);
    Console.WriteLine("======================================================");
    foreach (var item in pokedex)
    {
        Console.WriteLine($"{item.Name}\t{item.Type1}");
    }
}