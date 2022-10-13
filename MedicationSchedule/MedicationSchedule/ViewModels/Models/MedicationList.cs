using MedicationSchedule.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MedicationSchedule.ViewModels.Models
{
    public class MedicationList
    {
        private static string _fileName = "MedicationList.json";
        private static string _filePath = string.Empty;

        private ObservableCollection<Medication> _items;
        public ObservableCollection<Medication> Items { get => _items; set => _items = value; }
        public Medication SelectedItem { get => _selectedItem; set => _selectedItem = value; }

        DateTime _startDate;
        uint _number;
        private FileStream _stream;
        private bool _isFileEmpty = true;
        private static uint _constructorCalls = 0;
        private static uint _setDoneCalls = 0;
        private Medication _selectedItem = null;

        public MedicationList()
        {
            _constructorCalls = _constructorCalls + 1;
            _filePath = Path.Combine(FileSystem.AppDataDirectory, _fileName);
            if (_filePath == null || !File.Exists(_filePath))
            {
                _stream = File.Create(_filePath);
                _isFileEmpty = true;
            }
            else
            {
                _isFileEmpty = false;
                this.ReadData();
            }
            //Debug.Print("_constructorCalls: " + _constructorCalls);
        }
        public void CreateMedicationList(DateTime startDate = default, uint number = 20, Side side = Side.Right)
        {
            _items = new ObservableCollection<Medication>();
            _startDate = startDate;
            _number = number;
            if (_startDate == default)
            {
                _startDate = new DateTime(DateTime.Today.Ticks);
            }
            _items = new ObservableCollection<Medication>();
            Side sideAtEvenIndex = side;
            Side sideAtOddIndex = (side == Side.Left) ? Side.Right : Side.Left;
            for (var j = 0; j < _number; j++)
            {
                
                _items.Add(new Medication()
                {
                    When = _startDate.AddDays(j),
                    WhichSide = (j%2==0)? sideAtEvenIndex : sideAtOddIndex,
                    IsDone = false
                });
                //side = (side == Side.Left) ? Side.Right : Side.Left;
            }
            
        }

        public bool ContainsToday()
        {
            var medQuery =
            from item in this.Items
            where item.When.Date == DateTime.Today.Date
            select item;
            return medQuery.Any();
        }

        public void SetDone()
        {
            _setDoneCalls = _setDoneCalls + 1;
            Debug.Print("_setDoneCalls: " + _setDoneCalls);
            if (this.ContainsToday())
            {
                SelectedItem.IsDone = !SelectedItem.IsDone;
            }
        }

        public void ReadData()
        {
            try
            {
                var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _fileName);

                if (_isFileEmpty)
                {
                    return;
                }
                using (var reader = new StreamReader(_filePath, true))
                {
                    var jsonString = reader.ReadToEnd();

                    if (jsonString != String.Empty)
                    {
                        Items = JsonConvert.DeserializeObject<ObservableCollection<Medication>>(jsonString);
                        _number = (uint)Items.Count;
                    }
                    else
                    {
                        _number = 0;
                    }
                    
                }

                
                if (_number > 0)
                {
                    _startDate = Items.First().When;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public async Task<bool> SaveData()
        {
            var JsonData = JsonConvert.SerializeObject(Items);
            try
            {
                File.Delete(_filePath);
                using (var writer = new StreamWriter(_filePath))
                {
                    await writer.WriteLineAsync(JsonData);
                }
                _isFileEmpty = false;
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }

        }
    }
}
