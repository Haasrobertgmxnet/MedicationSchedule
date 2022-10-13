using MedicationSchedule.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace MedicationSchedule.Views
{
    public partial class MainPage
    {
        private MainPageViewModel _viewModel;
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            MessagingCenter.Subscribe<object, string>(this, "OnSleepKey", (sender, parameter) =>
            {
                Debug.WriteLine(parameter);
                _ = _viewModel.Medications.SaveData();
            });
        }
    }
}
