namespace Perilous.Npc
{
    public class Quantity : ModelBase
    {
        private double _value = default(double);

        public double Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged(() => Value);
                OnPropertyChanged(() => DerivedValue);
            }
        }

        public double DerivedValue
        {
            get { return Value*2.5d; }
        }

        private string _unit = string.Empty;

        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit.Equals(value)) return;
                _unit = value;
                OnPropertyChanged(() => Unit);
            }
        }

        public Quantity()
            : base()
        {
        }
    }
}
