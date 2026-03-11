using Lesson24DiLifetimeDemo.Components;
using Lesson24DiLifetimeDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// TODO: DI Pattern Step 2: Register the MessageService 
builder.Services.AddTransient<MessageService>();
// Singleton = one for the app
builder.Services.AddSingleton<SingletonService>();
// Scoped = one per user
builder.Services.AddScoped<ScopedService>();
// Transient = new each request
builder.Services.AddTransient<TransientService>();

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
