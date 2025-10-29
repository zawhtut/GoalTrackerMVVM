using GoalTrackerMVVM.ViewModels;

namespace GoalTrackerMVVM.Views;

public partial class DetailPage : ContentPage
{
    public DetailPage(DetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
