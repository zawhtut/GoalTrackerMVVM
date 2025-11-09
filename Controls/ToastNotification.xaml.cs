namespace GoalTrackerMVVM.Controls;

public partial class ToastNotification : Border
{
    public ToastNotification()
    {
        InitializeComponent();
    }

    public async Task ShowAsync(string message, int durationMilliseconds = 2000)
    {
        MessageLabel.Text = message;
        IsVisible = true;

        // Fade in
        await this.FadeTo(1, 250, Easing.CubicOut);

        // Wait for duration
        await Task.Delay(durationMilliseconds);

        // Fade out
        await this.FadeTo(0, 250, Easing.CubicIn);

        IsVisible = false;
    }
}
