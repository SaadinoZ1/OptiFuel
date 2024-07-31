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
using ValidationBL = OptiFuelMaui.Models.ValidationBL;

namespace OptiFuelMaui.ViewModels
{
    public class PlanningViewModel: INotifyPropertyChanged
    {
        private Planning _selectedPlanning;

        private readonly ApiService _apiService;
        public ObservableCollection<Planning> Plannings { get; set; }
        public ICommand SelectPlanningCommand { get; }
        public ICommand NavigateToAddPlanningCommand { get;}


        public PlanningViewModel()
        {
            _apiService = new ApiService();
            Plannings = new ObservableCollection<Planning>();
            SelectPlanningCommand = new Command<Planning>(OnPlanningSelected);
            NavigateToAddPlanningCommand = new Command(OnNavigateToAddPlanning);
            LoadPlannings();
        }

        private async void LoadPlannings()
        {
            try
            {
                var plannings = await _apiService.GetPlanningsAsync();
                if (plannings != null && plannings.Count > 0)
                {
                    foreach (var planning in plannings)
                    {
                        Plannings.Add(planning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error loading plannings: {ex.Message}");
            }
        }

        private async void OnPlanningSelected(Planning planning)
        {
            if (planning == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ValidationBL)}?PlanningId={planning.Id}");
        }
        private async void OnNavigateToAddPlanning()
        {
            await Shell.Current.GoToAsync(nameof(AddPlanning));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}

