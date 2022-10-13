using MedicationSchedule.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace MedicationSchedule.Views
{
    public partial class CreateMedicationSchedulePage : ContentPage
    {
        private CreateMedicationSchedulePageViewModel _viewModel;
        public CreateMedicationSchedulePage(CreateMedicationSchedulePageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        protected override void OnDisappearing()
        {
            _ = _viewModel.Medications.SaveData();
        }
    }
}
