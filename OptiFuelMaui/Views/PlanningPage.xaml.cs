using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using System.Collections.ObjectModel;
using OptiFuelMaui.ViewModels;

namespace OptiFuelMaui.Views;

public partial class PlaningPage : ContentPage
{
    private readonly PlanningViewModel _viewModel;
    public ObservableCollection<Planning> Plannings { get; set; }
    public PlaningPage()
    {
        InitializeComponent();
        _viewModel = new PlanningViewModel();
        Plannings = new ObservableCollection<Planning>();
        BindingContext = _viewModel;
     
    }
}