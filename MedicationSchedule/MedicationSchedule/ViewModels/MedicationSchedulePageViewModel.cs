using GalaSoft.MvvmLight.Command;
using MedicationSchedule.Models;
using MedicationSchedule.ViewModels.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Windows.Input;

namespace MedicationSchedule.ViewModels
{
    public class MedicationSchedulePageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

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

        private static bool _isDoneOld;
        private string _whenStr;
        private string _whichSide;
        private bool _isDone;

        public string WhenStr
        {
            get => _whenStr;
            set
            {
                SetProperty(ref _whenStr, value);
            }
        }

        public string WhichSide
        {
            get => _whichSide;
            set
            {
                SetProperty(ref _whichSide, value);
            }
        }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                SetProperty(ref _isDone, value);
            }
        }

        public ICommand SetDoneCommand { get; private set; }
        public static bool IsDoneOld { get => _isDoneOld; set => _isDoneOld = value; }

        public MedicationSchedulePageViewModel(INavigationService navigationService, MedicationList medication)
            : base(navigationService)
        {
            _medications = medication;

            //SetDoneCommand = new RelayCommand<bool>((value) => SetDone());
            SetDoneCommand = new DelegateCommand(SetDone);

            var medQuery =
            from item in _medications.Items
            where item.When.Date == DateTime.Today.Date
            select item;
            if (medQuery.Any())
            {
                _medications.SelectedItem = medQuery.Last();
                WhenStr = _medications.SelectedItem.WhenStr;
                WhichSide = _medications.SelectedItem.WhichSide == Side.Left ? "Left" : "Right";
                IsDone = _medications.SelectedItem.IsDone;
                IsDoneOld = IsDone;
            }
            else
            {
                WhenStr = "Heutiges Datum nicht in der Liste";
            }
        }

        private void SetDone()
        {
            if (IsDone != IsDoneOld)
            {
                Medications.SetDone();
                IsDoneOld = IsDone;
            }
        }

    }
}
