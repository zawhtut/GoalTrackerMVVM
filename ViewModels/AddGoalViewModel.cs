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
            if (string.IsNullOrWhiteSpace(Name))
                return;

            Goal goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation,
                Steps = Steps,
                TargetDate = TargetDate.ToString("MMMM dd, yyyy"),
                Progress = GetProgress(TargetDate)
            };

            // Save to database
            await _database.SaveGoalAsync(goal);

            // Navigate back
            await Shell.Current.GoToAsync("..");
        }

        private double GetProgress(DateTime targetDate)
        {
            var today = DateTime.Today;
            var daysUntilTarget = (targetDate - today).Days;

            // Simple progress calculation: closer dates have lower progress
            if (daysUntilTarget <= 0)
                return 1.0;
            else if (daysUntilTarget <= 30)
                return 0.8;
            else if (daysUntilTarget <= 90)
                return 0.5;
            else if (daysUntilTarget <= 180)
                return 0.3;
            else
                return 0.1;
        }
    }
}
