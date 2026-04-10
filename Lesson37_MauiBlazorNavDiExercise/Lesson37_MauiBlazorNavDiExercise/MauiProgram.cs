using Lesson37_MauiBlazorNavDiExercise.Services;
using Microsoft.Extensions.Logging;

namespace Lesson37_MauiBlazorNavDiExercise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Singleton: One instance for the whole app
            builder.Services.AddSingleton<IProductService, MockProductService>();   // enables service to be injected
            builder.Services.AddSingleton<IDiscountService, SimpleDiscountService>();
            builder.Services.AddSingleton<IDiscountService, TimeOfDayDiscountService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
