using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoalTrackerMVVM.Models;

namespace GoalTrackerMVVM.ViewModels
{
    [QueryProperty(nameof(Goal), "Goal")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        Goal goal;

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
