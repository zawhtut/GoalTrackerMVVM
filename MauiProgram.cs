using Microsoft.Extensions.Logging;
using GoalTrackerMVVM.Views;
using GoalTrackerMVVM.ViewModels;
using GoalTrackerMVVM.Services;
using GoalTrackerMVVM.BusinessLogic;

namespace GoalTrackerMVVM;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Data Access Layer
        builder.Services.AddSingleton<IGoalDatabase, GoalDatabase>();

        // Register Business Logic Layer
        builder.Services.AddSingleton<IGoalService, GoalService>();

        // Register ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<AddGoalViewModel>();

        // Register Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<AddGoalPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
