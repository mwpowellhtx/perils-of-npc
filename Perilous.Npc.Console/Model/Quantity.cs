﻿using System.ComponentModel;

namespace Perilous.Npc.Model
{
    public class Quantity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private double _value = default(double);

        public double Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private string _unit = string.Empty;

        public string Unit
        {
            get { return _unit; }
            set
            {
                if (_unit.Equals(value)) return;
                _unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public Quantity()
        {
        }
    }
}
