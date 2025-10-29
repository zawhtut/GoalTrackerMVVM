using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;

namespace GoalTrackerMVVM.ViewModels
{
    public partial class AddGoalViewModel : ObservableObject
    {
        // Separate Observable Property for every input field
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string motivation;

        [ObservableProperty]
        private string steps;

        [ObservableProperty]
        private DateTime targetDate = DateTime.Today; // default value

        [RelayCommand]
        async Task SubmitNewGoal()
        {
            Goal goal = new Goal()
            {
                Name = Name,
                Motivation = Motivation,
                Steps = Steps,
                TargetDate = TargetDate.ToString("MMMM dd, yyyy"),
                Progress = GetProgress(TargetDate)
            };

            // In a real app, you would add this to a service or pass it back
            // For now, we'll just navigate back
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
