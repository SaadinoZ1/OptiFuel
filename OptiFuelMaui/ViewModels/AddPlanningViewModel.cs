using MvvmHelpers;
using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptiFuelMaui.ViewModels
{

    public class AddPlanningViewModel:BaseViewModel
    {
        private readonly ApiService _apiService;

        public Planning NewPlanning { get; set; }
        public ICommand AddPlanningCommand { get; }

        public AddPlanningViewModel()
        {
            _apiService = new ApiService();
            NewPlanning = new Planning();
            AddPlanningCommand = new Command(async () => await AddPlanning());
        }

        private async Task AddPlanning()
        {
            var success = await _apiService.AddPlanningAsync(NewPlanning);
            if (success)
            {
                // Handle successful addition, e.g., navigate back or show a message
                await App.Current.MainPage.DisplayAlert("Success", "Planning added successfully", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                // Handle error
                await App.Current.MainPage.DisplayAlert("Error", "Failed to add planning", "OK");
            }
        }


    }
}
