using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
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
    public class PlanningViewModel: INotifyPropertyChanged
    {
        private Planning _selectedPlanning;

        private readonly ApiService _apiService;
        public ObservableCollection<Planning> Plannings { get; set; }
        public ICommand SelectPlanningCommand { get; }
        public ICommand NavigateToAddPlanningCommand { get;}
        public ICommand NavigateToEditPlanningCommand { get; }
        public IAsyncCommand<Planning> EditPlanningCommand { get; }
        public IAsyncCommand<Planning> AddPlanningCommand { get; }
        public Planning SelectedPlanning
        {
            get => _selectedPlanning;
            set
            {
                _selectedPlanning = value;
                OnPropertyChanged();
                if(_selectedPlanning != null)
                {
                    Shell.Current.GoToAsync($"EditPlanningPage?planningId={_selectedPlanning.Id}");
                }
            }
        }
        public PlanningViewModel()
        {
            _apiService = new ApiService();
            Plannings = new ObservableCollection<Planning>();
            SelectPlanningCommand = new Microsoft.Maui.Controls.Command<Planning>(OnPlanningSelected);
            NavigateToAddPlanningCommand = new Microsoft.Maui.Controls.Command(OnNavigateToAddPlanning);
            NavigateToEditPlanningCommand = new Microsoft.Maui.Controls.Command(OnNavigateToEditPlanning);
            EditPlanningCommand = new AsyncCommand<Planning>(EditPlanningAsync);
            AddPlanningCommand = new AsyncCommand<Planning>(AddPlanningAsync);
            LoadPlannings();
        }

        public async void LoadPlannings()
        {
            try
            {
                var plannings = await _apiService.GetPlanningsAsync();
                Console.WriteLine($"Number of plannings fetched: {plannings.Count}");
                Plannings.Clear();
                if (plannings != null && plannings.Count > 0)
                {
                    foreach (var planning in plannings)
                    {
                        Plannings.Add(planning);
                        Console.WriteLine($"Added planning: {planning.Centre}, Date: {planning.Date}");
                    }
                    OnPropertyChanged(nameof(Plannings));
                }
                else
                {
                    Console.WriteLine("No plannings found.");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error loading plannings: {ex.Message}");
            }
        }

        private async Task AddPlanningAsync(Planning planning)
        {

            var newPlanning = await _apiService.AddPlanningAsync(planning);
            if (newPlanning != null)
            {
                Plannings.Add(newPlanning);
                OnPropertyChanged(nameof(Plannings));
            }

        }


        private async Task EditPlanningAsync(Planning planning)
        {
            var updatedPlanning = await _apiService.EditPlanningAsync(planning.Id, planning);
            if (updatedPlanning != null)
            {
                var index = Plannings.IndexOf(planning);
                if (index >= 0)
                {
                    Plannings[index] = updatedPlanning;

                    OnPropertyChanged(nameof(Plannings)); // Notifie le changement
                }


            }
        }

            private async void OnPlanningSelected(Planning planning)
        {
            if (planning == null)
                return;

            await Shell.Current.GoToAsync($"///ValidationBL?PlanningId={planning.Id}");
        }
        private async void OnNavigateToAddPlanning()
        {
            var AddPlanningViewModel = new AddPlanningViewModel();
            AddPlanningViewModel.PlanningAdded += (newPlanning) =>
            {
                Plannings.Add(newPlanning);
                OnPropertyChanged(nameof(Plannings));
            };
            await Shell.Current.GoToAsync(nameof(AddPlanning));
        }

        private async void OnNavigateToEditPlanning(object param)
        {

            if (param is Planning planning)
            {
                var editPlanningViewModel = new EditPlanningViewModel();
                editPlanningViewModel.PlanningUpdated += (updatedPlanning) =>
                {
                    var index = Plannings.IndexOf(planning);
                    if (index >= 0)
                    {
                        Plannings[index] = updatedPlanning;
                        OnPropertyChanged(nameof(Plannings));
                        Console.WriteLine("OnNavigateToEditPlanning: Planning list updated successfully");
                    }
                };
                await Shell.Current.GoToAsync($"{nameof(EditPlanningPage)}?planningId={planning.Id}");
            }


        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}

