using MauiIcons.Cupertino;
using MauiIcons.SegoeFluent;
using Microsoft.Extensions.Logging;
using SynetraMobile.Services;
using SynetraMobile.ViewModels;

namespace SynetraMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSegoeFluentMauiIcons()
                .UseCupertinoMauiIcons()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                   
                });

            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
