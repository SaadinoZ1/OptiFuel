using OptiFuelMaui.Models;
using OptiFuelMaui.ViewModels;

namespace OptiFuelMaui.Views;

public partial class EditPlanningPage : ContentPage
{
	public EditPlanningPage()
	{
		InitializeComponent();
		var viewModel = new EditPlanningViewModel();
		BindingContext = viewModel;
		viewModel.PlanningUpdated += OnPlanningUpdated;
	}

    private void OnPlanningUpdated(Planning updatedPlanning)
    {
        var mainViewModel = (PlanningViewModel)Application.Current.MainPage.BindingContext;
        mainViewModel.LoadPlannings();
    }


}