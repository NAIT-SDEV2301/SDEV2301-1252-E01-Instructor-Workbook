using Lesson25DemoBlazorApp.Components;
using Microsoft.EntityFrameworkCore;
using Lesson25DemoBlazorApp.Data;
using Lesson25DemoBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registering EF Core 
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite("Data Source=app.db"));
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// We will use a simple EF Core feature called EnsureCreated() to create the database
// and the Products table automatically based on your Product model and AppDbContext.
// 🔽 Add this block right after app is built
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
