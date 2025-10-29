using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;
using GoalTrackerMVVM.Views;

namespace GoalTrackerMVVM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Goal> Goals { get; }

        public MainViewModel()
        {
            Goals = new ObservableCollection<Goal>
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
        void DeleteGoal(Goal goal)
        {
            if (goal is null)
                return;

            Goals.Remove(goal);
        }
    }
}
