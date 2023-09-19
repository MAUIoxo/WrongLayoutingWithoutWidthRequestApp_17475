using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WrongLayoutingApp.Pages;
using WrongLayoutingApp.Pages.Views;
using WrongLayoutingApp.ViewModels;
using Sharpnado.CollectionView;
using Sharpnado.Tabs;

namespace WrongLayoutingApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSharpnadoTabs(loggerEnable: false)
            .UseSharpnadoCollectionView(loggerEnable: false)
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<View1>();
        builder.Services.AddSingleton<View1ViewModel>();

        builder.Services.AddTransient<View2>();
        builder.Services.AddSingleton<View2ViewModel>();

        return builder.Build();
	}
}
