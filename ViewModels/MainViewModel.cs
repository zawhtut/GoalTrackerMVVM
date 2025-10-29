using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;
using GoalTrackerMVVM.Views;
using GoalTrackerMVVM.Services;

namespace GoalTrackerMVVM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly GoalDatabase _database;

        public ObservableCollection<Goal> Goals { get; }

        [ObservableProperty]
        private bool isRefreshing;

        public MainViewModel(GoalDatabase database)
        {
            _database = database;
            Goals = new ObservableCollection<Goal>();
            
            // Load goals from database
            Task.Run(async () => await LoadGoalsAsync());
        }

        private async Task LoadGoalsAsync()
        {
            var goals = await _database.GetGoalsAsync();
            
            // If database is empty, add sample data
            if (goals.Count == 0)
            {
                await SeedDatabaseAsync();
                goals = await _database.GetGoalsAsync();
            }

            // Update UI on main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Goals.Clear();
                foreach (var goal in goals)
                {
                    Goals.Add(goal);
                }
            });
        }

        private async Task SeedDatabaseAsync()
        {
            var sampleGoals = new List<Goal>
            {
                new Goal()
                {
                    Name = "Exam",
                    Motivation = "There are no shortcuts to any place worth going.",
                    TargetDate = "July 30, 2025",
                    Progress = 0.5,
                },
                new Goal()
                {
                    Name = "Fitness",
                    Motivation = "Strength does not come from what you can do. It comes from overcoming the things you once thought you couldn't.",
                    TargetDate = "October 30, 2025",
                    Progress = 0.2
                },
                new Goal()
                {
                    Name = "Learn Piano",
                    Motivation = "Practice makes progress.",
                    TargetDate = "December 01, 2025",
                    Progress = 0.1
                },
                new Goal()
                {
                    Name = "Read 10 Books",
                    Motivation = "A reader lives a thousand lives before he/she dies.",
                    TargetDate = "December 31, 2025",
                    Progress = 0.4
                },
                new Goal()
                {
                    Name = "Meditation Habit",
                    Motivation = "Peace comes from within. Do not seek it without.",
                    TargetDate = "September 10, 2025",
                    Progress = 0.7
                }
            };

            foreach (var goal in sampleGoals)
            {
                await _database.SaveGoalAsync(goal);
            }
        }

        // Navigate to Add page to user input
        [RelayCommand]
        async Task GoToAddAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddGoalPage)}");
        }

        // Need user input -> user push an item
        // Navigate to details page
        // Give goal object to details page
        [RelayCommand]
        async Task GoToDetailsAsync(Goal goal)
        {
            if (goal is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DetailPage)}", true,
                new Dictionary<string, object>
                {
                    { "Goal", goal },
                });
        }

        // Task 8 - Delete goal command
        [RelayCommand]
        async Task DeleteGoalAsync(Goal goal)
        {
            if (goal is null)
                return;

            await _database.DeleteGoalAsync(goal);
            Goals.Remove(goal);
        }

        // Refresh goals from database
        [RelayCommand]
        async Task RefreshGoalsAsync()
        {
            IsRefreshing = true;
            await LoadGoalsAsync();
            IsRefreshing = false;
        }
    }
}
