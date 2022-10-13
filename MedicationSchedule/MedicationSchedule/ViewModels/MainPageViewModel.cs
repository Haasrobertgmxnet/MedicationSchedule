using MedicationSchedule.ViewModels.Models;
using Prism.Navigation;

namespace MedicationSchedule.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private MedicationList _medications;
        public MainPageViewModel(INavigationService navigationService, MedicationList medications)
            : base(navigationService)
        {
            _medications = medications;
            Title = "Main Page";
        }

        public MedicationList Medications { get => _medications; set => _medications = value; }
    }
}
