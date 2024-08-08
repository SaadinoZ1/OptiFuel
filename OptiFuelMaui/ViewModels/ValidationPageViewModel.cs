using MvvmHelpers;
using OptiFuelMaui.Dtos;
using OptiFuelMaui.Models;
using OptiFuelMaui.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private byte[] _blFile;
        private byte[] _certificatJumelageFile;
        public ObservableCollection<CommissionDto> CommissionMembers { get; set; }
        private ImageSource BlImage;
        private ImageSource CertificatImage;

        public ImageSource blImage
        {
            get => BlImage;
            set => SetProperty(ref BlImage, value);
        }

        public ImageSource certificatImage
        {
            get => CertificatImage;
            set => SetProperty(ref CertificatImage, value);
        }


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
            CommissionMembers = new ObservableCollection<CommissionDto>
            {
                new CommissionDto
                {
                    ContactId = Guid.NewGuid(), CodeG = "", CodeS = ""
                },
            };

        }


        private async Task LoadDataAsync()
        {
            try
            {
                SelectedPlanning = await _apiService.GetPlanningAsync(_planningId).ConfigureAwait(false);
                OnPropertyChanged(nameof(SelectedPlanning));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading planning: {ex.Message}");
            }
        }

        private async Task CaptureBLAsync()
        {
             var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status == PermissionStatus.Granted)
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    using var stream = await photo.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    {
                        await stream.CopyToAsync(memoryStream);
                        _blFile = memoryStream.ToArray();
                        BlImage = ImageSource.FromStream(() => new MemoryStream(_blFile));
                    }
                }

            }

        }

        private async Task CaptureCertificatAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status == PermissionStatus.Granted)
            {

                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    using var stream = await photo.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    {
                        await stream.CopyToAsync(memoryStream);
                        _certificatJumelageFile = memoryStream.ToArray();
                        CertificatImage = ImageSource.FromStream(() => new MemoryStream(_certificatJumelageFile));
                    }
                }
            }

        }

        private async Task ConfirmAsync()
        {
            var validationBLDto = new ValidationBLDto
            {
                PlanningId = _planningId,
                BLFile = _blFile,
                CertificatJumelageFile = _certificatJumelageFile,
                QuantitésBL = SelectedPlanning.QuantiteALivrer,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Commissions = CommissionMembers
            };

            var success = await _apiService.AddValidationBLAsync(validationBLDto).ConfigureAwait(false);

            if (success)
            {
                Console.WriteLine("Validation BL added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add Validation BL");
            }


        }

    }

}



   