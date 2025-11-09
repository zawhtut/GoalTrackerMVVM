using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;
using GoalTrackerMVVM.BusinessLogic;

namespace GoalTrackerMVVM.ViewModels;

public partial class AddGoalViewModel : ObservableObject
{
    private readonly IGoalService _goalService;

    // Separate Observable Property for every input field
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string motivation;

    [ObservableProperty]
    private string steps;

    [ObservableProperty]
    private DateTime targetDate = DateTime.Today; // default value

    public AddGoalViewModel(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [RelayCommand]
    async Task SubmitNewGoalAsync()
    {
        try
        {
            Goal goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation ?? string.Empty,
                Steps = Steps ?? string.Empty,
                TargetDate = TargetDate.ToString("MMMM dd, yyyy"),
                Progress = _goalService.CalculateInitialProgress(TargetDate)
            };

            // Validate using business logic
            var (isValid, errorMessage) = _goalService.ValidateGoal(goal);
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            // Save using business logic layer
            await _goalService.CreateGoalAsync(goal);

            // Clear form fields
            ClearForm();

            // Navigate back
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to create goal: {ex.Message}", "OK");
        }
    }

    private void ClearForm()
    {
        Name = string.Empty;
        Motivation = string.Empty;
        Steps = string.Empty;
        TargetDate = DateTime.Today;
    }
}
