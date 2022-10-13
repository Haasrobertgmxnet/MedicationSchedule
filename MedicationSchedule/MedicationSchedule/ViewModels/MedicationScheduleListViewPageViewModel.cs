using MedicationSchedule.ViewModels.Models;
using Prism.Navigation;

namespace MedicationSchedule.ViewModels
{
    public class MedicationScheduleListViewPageViewModel : ViewModelBase
    {
        private MedicationList _medications;

        public MedicationList Medications
        {
            get
            {
                return _medications;
            }

            set
            {
                SetProperty(ref _medications, value);
            }
        }

        public MedicationScheduleListViewPageViewModel(INavigationService navigationService, MedicationList medications)
            : base(navigationService)
        {
            _medications = medications;
        }
    }
}
