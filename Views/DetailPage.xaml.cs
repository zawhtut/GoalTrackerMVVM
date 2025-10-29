using GoalTrackerMVVM.ViewModels;

namespace GoalTrackerMVVM.Views;

public partial class DetailPage : ContentPage
{
    private readonly DetailViewModel _viewModel;

    public DetailPage(DetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private async void OnProgressChanged(object sender, ValueChangedEventArgs e)
    {
        if (_viewModel.IsEditing)
        {
            // Round to nearest 5% (0.05)
            double roundedValue = Math.Round(e.NewValue * 20) / 20; // Multiply by 20 to get 5% steps, then divide back

            // Only update if the value actually changed after rounding
            if (Math.Abs(roundedValue - _viewModel.Goal.Progress) > 0.001)
            {
                _viewModel.Goal.Progress = roundedValue;

                // Update slider to show rounded value
                if (sender is Slider slider && Math.Abs(slider.Value - roundedValue) > 0.001)
                {
                    slider.Value = roundedValue;
                }

                await _viewModel.UpdateProgressCommand.ExecuteAsync(roundedValue);
            }
        }
    }
}
