using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Perilous.Npc.Model;

namespace Perilous.Npc
{
    [TestFixture]
    public class QuantityTests
    {
        private Quantity _quantity;

        private IDictionary<string, int> _changedProperties;

        [SetUp]
        public void SetUp()
        {
            _quantity = new Quantity()
            {
                Value = 0d,
                Unit = string.Empty,
            };

            _changedProperties = new Dictionary<string, int>();
        }

        private void SetPropertyChanged(string propertyName)
        {
            if (!_changedProperties.ContainsKey(propertyName))
                _changedProperties[propertyName] = 0;

            _changedProperties[propertyName]++;
        }

        [Test]
        public void Test_That_Value_PropertyChanged()
        {
            const double value = 1d;

            PropertyChangedEventHandler handler = (s, e) =>
            {
                Assert.That(s, Is.SameAs(_quantity));
                SetPropertyChanged(e.PropertyName);
            };

            const string valueName = "Value";
            const string derivedValueName = "DerivedValue";

            VerifyPropertyChanged(_quantity, handler, q => q.Value = value);
            VerifyPropertyChanged(_quantity, handler, q => q.Value = value);

            Assert.That(_changedProperties.Count, Is.EqualTo(2));

            Assert.That(_changedProperties.ContainsKey(valueName), Is.True);
            Assert.That(_changedProperties.ContainsKey(derivedValueName), Is.True);

            Assert.That(_changedProperties[valueName], Is.EqualTo(1));
            Assert.That(_changedProperties[derivedValueName], Is.EqualTo(1));
        }

        [Test]
        public void Test_That_Unit_PropertyChanged()
        {
            PropertyChangedEventHandler handler = (s, e) =>
            {
                Assert.That(s, Is.SameAs(_quantity));
                SetPropertyChanged(e.PropertyName);
            };

            const string unitName = "Unit";

            VerifyPropertyChanged(_quantity, handler, q => q.Unit = "s");
            VerifyPropertyChanged(_quantity, handler, q => q.Unit = "s");

            Assert.That(_changedProperties.Count, Is.EqualTo(1));

            Assert.That(_changedProperties.ContainsKey(unitName), Is.True);

            Assert.That(_changedProperties[unitName], Is.EqualTo(1));
        }

        private static void VerifyPropertyChanged<T>(T obj,
            PropertyChangedEventHandler handler,
            Action<T> action)
            where T : INotifyPropertyChanged
        {
            try
            {
                obj.PropertyChanged += handler;

                action(obj);
            }
            finally
            {
                obj.PropertyChanged -= handler;
            }
        }
    }
}
