namespace Task_1
{
    public class Integer : NotifyPropertyChanged
    {
        private int _value;

        public Integer(int value)
        {
            Value = value;
        }
        
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}