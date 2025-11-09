namespace GoalTrackerMVVM;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes
        Routing.RegisterRoute(nameof(Views.DetailPage), typeof(Views.DetailPage));
        Routing.RegisterRoute(nameof(Views.AddGoalPage), typeof(Views.AddGoalPage));
    }
}
