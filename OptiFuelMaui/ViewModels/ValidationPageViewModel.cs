using MvvmHelpers;
using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptiFuelMaui.ViewModels
{
    [QueryProperty(nameof(PlanningId), "PlanningId")]
    public class ValidationPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private Guid _planningId;
        private Planning _selectedPlanning;


        public string PlanningId
        {
            set
            {
                if (Guid.TryParse(value, out var guid))
                {
                    Console.WriteLine($"Received PlanningId: {guid}");
                    _planningId = guid;
            
                LoadDataCommand.Execute(null);
            }
            else 
            {
                Console.WriteLine("Invalid Guid format.");
            }
        } }

        public Planning SelectedPlanning
        {
            get => _selectedPlanning;
            set => SetProperty(ref _selectedPlanning, value);
        }

        public ICommand LoadDataCommand { get; }
        public ICommand CaptureBLCommand { get; }
        public ICommand CaptureCertificatCommand { get; }
        public ICommand ConfirmCommand { get; }

        public ValidationPageViewModel()
        {
            _apiService = new ApiService();
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            CaptureBLCommand = new Command(async () => await CaptureBLAsync());
            CaptureCertificatCommand = new Command(async () => await CaptureCertificatAsync());
            ConfirmCommand = new Command(async () => await ConfirmAsync());
        }


        private async Task LoadDataAsync()
        {
            try
            {
                SelectedPlanning = await _apiService.GetPlanningAsync(_planningId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading planning: {ex.Message}");
            }
        }

        private async Task CaptureBLAsync()
        {
            // Logic to capture BL
        }

        private async Task CaptureCertificatAsync()
        {
            // Logic to capture Certificat
        }

        private async Task ConfirmAsync()
        {
            // Logic to confirm
        }



    }


}
