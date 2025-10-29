using GoalTrackerMVVM.ViewModels;

namespace GoalTrackerMVVM.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
