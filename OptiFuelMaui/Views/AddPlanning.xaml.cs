using OptiFuelMaui.ViewModels;

namespace OptiFuelMaui.Views;

public partial class AddPlanning : ContentPage
{
	private readonly AddPlanningViewModel _viewModel;
	public AddPlanning()
	{
		InitializeComponent();
		_viewModel = new AddPlanningViewModel();
		BindingContext = _viewModel;
	}
}