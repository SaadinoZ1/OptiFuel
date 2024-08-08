using MvvmHelpers;
using MvvmHelpers.Commands;
using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using OptiFuelMaui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptiFuelMaui.ViewModels
{
    [QueryProperty(nameof(PlanningId), "planningId")]

    public class EditPlanningViewModel:BaseViewModel
    {
        private readonly ApiService _apiService;
        private string _planningId;

        public Planning SelectedPlanning { get; set; }
        public ICommand SaveCommand { get; }
        public event Action<Planning> PlanningUpdated;

        public EditPlanningViewModel()
        {
            _apiService = new ApiService();
            SaveCommand = new AsyncCommand(SavePlanningAsync);
        }

        public string PlanningId
        {
            get => _planningId;
            set
            {
                _planningId = value;
                LoadPlanning(value);
            }
        }

        private async void LoadPlanning(string planningId)
        {
            if (Guid.TryParse(planningId, out Guid parsedPlanningId))
            {
                SelectedPlanning = await _apiService.GetPlanningAsync(parsedPlanningId);
                OnPropertyChanged(nameof(SelectedPlanning));
            }
            else
            {
                Console.WriteLine("Invalid GUID format,");
            }
        }

        private async Task SavePlanningAsync()
        {
            try
            {
                var updatedPlanning = await _apiService.EditPlanningAsync(SelectedPlanning.Id, SelectedPlanning);
                if (updatedPlanning != null)
                {
                    Console.WriteLine("SavePlanningAsync:Planning updated successfully");
                    await App.Current.MainPage.DisplayAlert("Success", "Planning updated successfully", "OK");

                    PlanningUpdated?.Invoke(updatedPlanning);

                        await Shell.Current.GoToAsync(nameof(PlanningPage));
                }
                else
                {
                    Console.WriteLine("SavePlanningAsync: Failed to update Planning");
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to update planning", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating planning: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to update planning: {ex.Message}", "OK");
            }
        }
    }
}
