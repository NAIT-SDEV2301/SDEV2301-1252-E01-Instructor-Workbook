using Microsoft.EntityFrameworkCore;    // for .UseSqlite() extension method
using PokemonPractice.BlazorApp.Components;
using PokemonPractice.Data.Data;
using PokemonPractice.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));
//builder.Services.AddScoped<PokemonPractice.Data.Services.PokemonService>();
builder.Services.AddScoped<PokemonService>(); // requires `using PokemonPractice.Data.Services;`
builder.Services.AddScoped<PokeTypeService>();
builder.Services.AddScoped<PokemonTypeService>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

/*
 * On first run, EF Core will:
 *  1) Create app.db if it doesn’t exist.
 *  2) Create the Products table according to the Product entity.
 *  3) On future runs, it will not drop the database; it will just ensure it exists.
 */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

    var dbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await dbSeeder.SeedIfEmptyAsync();
    // NOTE:
    // We are using EnsureCreated() only for simplicity in this lesson.
    // In real-world applications, always use EF Core migrations instead.
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
