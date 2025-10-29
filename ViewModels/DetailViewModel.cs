using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;
using GoalTrackerMVVM.Services;

namespace GoalTrackerMVVM.ViewModels
{
    [QueryProperty(nameof(Goal), "Goal")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly GoalDatabase _database;

        [ObservableProperty]
        Goal goal;

        [ObservableProperty]
        private bool isEditing;

        public DetailViewModel(GoalDatabase database)
        {
            _database = database;
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

            await _database.SaveGoalAsync(Goal);
            IsEditing = false;
            
            await Shell.Current.DisplayAlert("Success", "Goal updated successfully!", "OK");
        }

        [RelayCommand]
        async Task UpdateProgressAsync(double newProgress)
        {
            if (Goal is null)
                return;

            Goal.Progress = newProgress;
            await _database.SaveGoalAsync(Goal);
        }
    }
}
