using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Perilous.Npc.Model
{
    public class Quantity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var propertyInfo = property.GetProperty();
            OnPropertyChanged(propertyInfo.Name);
        }

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
        {
        }
    }
}
