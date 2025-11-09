namespace GoalTrackerMVVM;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());

#if WINDOWS
        // Set window size to smartphone dimensions (iPhone 14 Pro size)
        window.Width = 393;
        window.Height = 852;

        // Set window position on screen (X, Y)
        window.X = 300;  // Distance from left edge of screen
        window.Y = 30;   // Distance from top edge of screen

        // Optional: Set minimum size to prevent resizing too small
        window.MinimumWidth = 320;
        window.MinimumHeight = 600;

        // Optional: Set maximum size to keep it phone-sized
        window.MaximumWidth = 430;
        window.MaximumHeight = 932;
#endif

        return window;
    }
}