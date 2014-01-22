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
                SetProperty(ref _value, value, () => Value,
                    (x, y) => y.CompareTo(x) != 0,
                    null,
                    (x, y) => OnPropertyChanged(() => DerivedValue));
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
                SetProperty(ref _unit, value ?? string.Empty, () => Unit,
                    (x, y) => !y.Equals(x));
            }
        }

        public Quantity()
            : base()
        {
        }
    }
}
