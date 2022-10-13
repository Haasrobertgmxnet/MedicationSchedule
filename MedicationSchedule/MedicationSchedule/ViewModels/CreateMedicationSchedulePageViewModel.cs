using MedicationSchedule.Models;
using MedicationSchedule.ViewModels.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedicationSchedule.ViewModels
{
    public class CreateMedicationSchedulePageViewModel : ViewModelBase
    {
        private DateTime _minDate;
        private DateTime _maxDate;
        private DateTime _selectedDate;
        private List<string> _sides;
        private string _selectedSide;

        private static MedicationList _medications = null;
        private static uint _number = 20;
        public uint Number { 
            get => _number;
            set {
                SetProperty(ref _number, value);
            }
        }

        private ICommand _createMedications;
        public ICommand CreateMedications
        {
            get => _createMedications;
            set => SetProperty(ref _createMedications, value);
        }
        public MedicationList Medications 
        { 
            get => _medications;
            set
            {
                SetProperty(ref _medications, value);
            }
        }

        public DateTime MinDate 
        { 
            get => _minDate;
            set
            {
                SetProperty(ref _minDate, value);
            }
        }

        public DateTime MaxDate 
        { 
            get => _maxDate;
            set
            {
                SetProperty(ref _maxDate, value);
            }
        }

        public DateTime SelectedDate 
        { 
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        public List<string> Sides 
        { 
            get => _sides;
            set
            {
                SetProperty(ref _sides, value);
            }
        }

        public string SelectedSide 
        { 
            get => _selectedSide;
            set
            {
                SetProperty(ref _selectedSide, value);
            }
        }

        public CreateMedicationSchedulePageViewModel(INavigationService navigationService, MedicationList medications)
            : base(navigationService)
        {
            MinDate = DateTime.Now;
            MaxDate = MaxDate.AddDays(4);
            Sides = new List<string>() { "Links", "Rechts" };
            _medications = medications;
            CreateMedications = new CreateMedicationsCommand(this);
        }

        private class CreateMedicationsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private readonly CreateMedicationSchedulePageViewModel viewModel;

            internal CreateMedicationsCommand(CreateMedicationSchedulePageViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                DateTime selectedDate = viewModel.SelectedDate;
                Side side = (viewModel.SelectedSide == "Links") ? Side.Left : Side.Right;
                _medications.CreateMedicationList(selectedDate, _number, side);
                //_ = _medications.SaveData();
            }
        }
    }
}
