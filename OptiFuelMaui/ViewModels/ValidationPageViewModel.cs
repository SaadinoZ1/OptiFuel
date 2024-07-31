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
    [QueryProperty(nameof(PlanningId),"PlanningId")]
    public  class ValidationPageViewModel: BaseViewModel
    {
        private readonly ApiService _apiService;
        private int _planningId;
        private Planning _selectedPlanning;


        public int PlanningId
        {
            get => _planningId;
            set
            {
                _planningId = value;
                LoadDataCommand.Execute(null);
            }
        }

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
        }

        

    }


}
