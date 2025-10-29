using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;
using GoalTrackerMVVM.Services;

namespace GoalTrackerMVVM.ViewModels
{
    public partial class AddGoalViewModel : ObservableObject
    {
        private readonly GoalDatabase _database;

        // Separate Observable Property for every input field
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string motivation;

        [ObservableProperty]
        private string steps;

        [ObservableProperty]
        private DateTime targetDate = DateTime.Today; // default value

        public AddGoalViewModel(GoalDatabase database)
        {
            _database = database;
        }

        [RelayCommand]
        async Task SubmitNewGoalAsync()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please enter a goal name.", "OK");
                return;
            }

            Goal goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation ?? string.Empty,
                Steps = Steps ?? string.Empty,
                TargetDate = TargetDate.ToString("MMMM dd, yyyy"),
                Progress = GetProgress(TargetDate)
            };

            // Save to database
            await _database.SaveGoalAsync(goal);

            // Clear form fields
            ClearForm();

            // Navigate back
            await Shell.Current.GoToAsync("..");
        }

        private void ClearForm()
        {
            Name = string.Empty;
            Motivation = string.Empty;
            Steps = string.Empty;
            TargetDate = DateTime.Today;
        }

        private double GetProgress(DateTime targetDate)
        {
            var today = DateTime.Today;
            var daysUntilTarget = (targetDate - today).Days;

            double initialProgress;

            // Simple progress calculation: closer dates have lower initial progress
            if (daysUntilTarget <= 0)
                initialProgress = 1.0;
            else if (daysUntilTarget <= 30)
                initialProgress = 0.8;
            else if (daysUntilTarget <= 90)
                initialProgress = 0.5;
            else if (daysUntilTarget <= 180)
                initialProgress = 0.3;
            else
                initialProgress = 0.1;

            // Round to nearest 5% (0.05)
            return Math.Round(initialProgress * 20) / 20;
        }
    }
}
