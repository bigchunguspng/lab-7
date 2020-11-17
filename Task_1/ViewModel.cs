using System.Collections.ObjectModel;

namespace Task_1
{
    public class ViewModel : NotifyPropertyChanged
    {
        private int _newNumber;
        private RelayCommand _addNumber;
        private RelayCommand _removeNumber;

        public ViewModel()
        {
            Numbers = new ObservableCollection<Integer>();
        }

        public ObservableCollection<Integer> Numbers { get; }

        public Integer SelectedNumber { get; set; }

        public int NewNumber
        {
            get => _newNumber;
            set
            {
                _newNumber = value;
                OnPropertyChanged(nameof(NewNumber));
            }
        }

        public RelayCommand AddNumber => _addNumber ?? (_addNumber = new RelayCommand(o =>
        {
            Numbers.Add(new Integer(NewNumber));
            NewNumber = 0;
            UpdateCalculations();
        }));

        public RelayCommand RemoveNumber => _removeNumber ?? (_removeNumber = new RelayCommand(o =>
        {
            Numbers.Remove(SelectedNumber);
            UpdateCalculations();
        }, o => SelectedNumber != null));

        #region calculations

        public string Max
        {
            get
            {
                var result = int.MinValue;
                foreach (var number in Numbers)
                    if (number.Value > result)
                        result = number.Value;

                return "Max number: " + (result == int.MinValue ? "none" : result.ToString());
            }
        }

        public string Min
        {
            get
            {
                var result = int.MaxValue;
                foreach (var number in Numbers)
                    if (number.Value < result)
                        result = number.Value;

                return "Min number: " + (result == int.MaxValue ? "none" : result.ToString());
            }
        }

        private int CalculateSum()
        {
            var result = 0;
            foreach (var number in Numbers)
                result += number.Value;

            return result;
        }
        
        public string Sum => "Sum of elements: " + CalculateSum();

        public string Average => "Average value: " + CalculateSum() / Numbers.Count;

        public ObservableCollection<Integer> OddNumbers
        {
            get
            {
                var result = new ObservableCollection<Integer>();
                foreach (var number in Numbers)
                    if (number.Value % 2 != 0)
                        result.Add(number);

                return result;
            }
        }

        private void UpdateCalculations()
        {
            OnPropertyChanged(nameof(Max));
            OnPropertyChanged(nameof(Min));
            OnPropertyChanged(nameof(Sum));
            OnPropertyChanged(nameof(Average));
            OnPropertyChanged(nameof(OddNumbers));
        }

        #endregion
    }
}