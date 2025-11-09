namespace GoalTrackerMVVM.ViewModels;

[QueryProperty(nameof(Goal), "Goal")]
public partial class DetailViewModel : ObservableObject
{
    private readonly IGoalService _goalService;
    private ToastNotification _toastNotification;

    [ObservableProperty]
    Goal goal;

    [ObservableProperty]
    private bool isEditing;

    public DetailViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    /// <summary>
    /// Set the toast notification control from the view
    /// </summary>
    public void SetToastNotification(ToastNotification toastNotification)
    {
        _toastNotification = toastNotification;
    }

    /// <summary>
    /// Refresh goal data from database
    /// This should be called when the page appears to get the latest data
    /// </summary>
    public async Task RefreshGoalAsync()
    {
        if (Goal is null || Goal.Id <= 0)
            return;

        try
        {
            var updatedGoal = await _goalService.GetGoalByIdAsync(Goal.Id);
            if (updatedGoal is not null)
            {
                // Update all properties to trigger UI refresh
                Goal = updatedGoal;
                OnPropertyChanged(nameof(Goal));
            }
        }
        catch (Exception ex)
        {
            await ShowToastAsync($"Failed to refresh: {ex.Message}");
        }
    }

    [RelayCommand]
    async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    void ToggleEdit()
    {
        IsEditing = !IsEditing;
    }

    [RelayCommand]
    async Task SaveChangesAsync()
    {
        if (Goal is null)
            return;

        try
        {
            await _goalService.UpdateGoalAsync(Goal);
            IsEditing = false;

            // Refresh the goal from database to ensure UI shows saved data
            await RefreshGoalAsync();

            // Show success toast
            await ShowToastAsync("Changes saved successfully");
        }
        catch (Exception ex)
        {
            await ShowToastAsync($"Error: {ex.Message}");
        }
    }

    [RelayCommand]
    async Task UpdateProgressAsync(double newProgress)
    {
        if (Goal is null)
            return;

        try
        {
            // Round to nearest 5%
            Goal.Progress = _goalService.RoundProgressToNearestFivePercent(newProgress);
            await _goalService.UpdateGoalAsync(Goal);
            
            // Refresh to show the exact saved value
            await RefreshGoalAsync();
        }
        catch (Exception ex)
        {
            await ShowToastAsync($"Error updating progress: {ex.Message}");
        }
    }

    /// <summary>
    /// Show a custom in-app toast notification
    /// </summary>
    private async Task ShowToastAsync(string message)
    {
        if (_toastNotification != null)
        {
            await _toastNotification.ShowAsync(message, 2000);
        }
    }
}
