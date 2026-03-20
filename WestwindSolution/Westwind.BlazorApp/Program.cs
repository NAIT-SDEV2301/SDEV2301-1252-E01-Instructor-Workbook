// Required for EF Core and SQL Server support
using Microsoft.EntityFrameworkCore;
using Westwind.BlazorApp.Components;

// Required for Business Logic Layer services
using WestwindSystem.BLL.DonW;
using WestwindSystem.BLL.SamW;

// Required for DbContext class
using WestwindSystem.DAL;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// DATABASE + SERVICE CONFIGURATION (REFERENCE IMPLEMENTATION)
// ------------------------------------------------------------
// This section configures:
// 1. Database connection (SQL Server)
// 2. EF Core DbContext
// 3. Business Logic Layer (BLL) services
//
// IMPORTANT:
// - This code is NOT generated.
// - Students must understand this section.
// - In your own project, you will modify:
//     - connection string name
//     - DbContext class
//     - services you register
// ------------------------------------------------------------

// NOTE: Connection string should be stored in User Secrets for local development
// Do NOT modify appsettings.json for machine-specific database settings
// Read the connection string named "WestwindDatabase" from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("WestwindDatabase");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Missing connection string 'WestwindDatabase'. Configure appsettings.json or User Secrets.");

// Register EF Core DbContext using SQL Server
// IDbContextFactory is used for Blazor apps to create a new DbContext per operation
builder.Services.AddDbContextFactory<WestWindContext>(options =>
    options.UseSqlServer(connectionString));

// Register BLL services for dependency injection
// These services will be injected into Blazor components
builder.Services.AddScoped<ShipperServices>();
builder.Services.AddScoped<ShipmentServices>();

// IMPORTANT RULE:
// Blazor components should NOT use DbContext directly.
// Always call a BLL service instead.


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

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
