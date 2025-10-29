namespace GoalTrackerMVVM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            // Set window size to smartphone dimensions (iPhone 14 Pro size)
            window.Width = 393;
            window.Height = 852;

            // Optional: Set minimum size to prevent resizing too small
            window.MinimumWidth = 320;
            window.MinimumHeight = 600;

            // Optional: Set maximum size to keep it phone-sized
            window.MaximumWidth = 430;
            window.MaximumHeight = 932;

            return window;
        }
    }
}