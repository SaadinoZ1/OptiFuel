using MvvmHelpers;
using MvvmHelpers.Commands;
using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using OptiFuelMaui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptiFuelMaui.ViewModels
{

    public class AddPlanningViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        public ObservableCollection<string> CentreNames { get; set; }
        public ObservableCollection<Centre> Centres { get; set; }

        public Planning NewPlanning { get; set; }
        public ICommand AddPlanningCommand { get; }

        private string _selectedCentreName;
        public string SelectedCentreName
        {
            get => _selectedCentreName;
            set
            {
                _selectedCentreName = value;
                var selectedCentre = Centres.FirstOrDefault(c => c.Name == value);
                NewPlanning.Centre = selectedCentre?.Name;
                OnPropertyChanged();
            }
        }

        public event Action<Planning> PlanningAdded;
        public AddPlanningViewModel()
        {
            _apiService = new ApiService();
            NewPlanning = new Planning();
            Centres = new ObservableCollection<Centre>();
            CentreNames = new ObservableCollection<string>();
            AddPlanningCommand = new Microsoft.Maui.Controls.Command(AddPlanningAsync);
            LoadCentres();
        }

        private async void LoadCentres()
        {

            try
            {
                var centres = await _apiService.GetCentresAsync();
                Centres.Clear();
                CentreNames.Clear();
                foreach (var centre in centres)
                {
                    Centres.Add(centre);
                    CentreNames.Add(centre.Name);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error fetching centres: {ex.Message}");
            }

        }

        private async void AddPlanningAsync()
        {
            try
            {
                Console.WriteLine($"Centre: {NewPlanning.Centre}, Date: {NewPlanning.Date}, QuantiteALivrer: {NewPlanning.QuantiteALivrer}");
                if (NewPlanning == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "NewPlanning is null", "OK");
                    return;
                }
                var result = await _apiService.AddPlanningAsync(NewPlanning);
                if (result != null)
                {
                    // Handle successful addition, e.g., navigate back or show a message
                    await App.Current.MainPage.DisplayAlert("Success", "Planning added successfully", "OK");                    
                    PlanningAdded?.Invoke(result);
                    await Shell.Current.GoToAsync(nameof(PlanningPage));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to add planning", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding planning: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to add planning: {ex.Message}", "OK");
            }
        }
    }
}
            
        
    

           


    