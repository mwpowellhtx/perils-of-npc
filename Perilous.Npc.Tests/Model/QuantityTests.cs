using System;
using System.ComponentModel;
using NUnit.Framework;
using Perilous.Npc.Model;

namespace Perilous.Npc
{
    [TestFixture]
    public class QuantityTests
    {
        private Quantity _quantity;

        [SetUp]
        public void SetUp()
        {
            _quantity = new Quantity()
            {
                Value = 0d,
                Unit = string.Empty,
            };
        }

        [Test]
        public void Test_That_Value_PropertyChanged()
        {
            var value = 1d;
            var changed = 0;

            PropertyChangedEventHandler handler = (s, e) =>
            {
                Assert.That(s, Is.SameAs(_quantity));
                Assert.That(e.PropertyName, Is.EqualTo("Value"));
                ++changed;
            };

            VerifyPropertyChanged(
                _quantity, handler,
                q =>
                {
                    var old = changed;
                    q.Value = value;
                    Assert.That(changed, Is.GreaterThan(old));
                });

            VerifyPropertyChanged(
                _quantity, handler,
                q =>
                {
                    var old = changed;
                    q.Value = value;
                    Assert.That(changed, Is.EqualTo(old));
                });
        }

        [Test]
        public void Test_That_Unit_PropertyChanged()
        {
            var changed = 0;

            PropertyChangedEventHandler handler = (s, e) =>
            {
                Assert.That(s, Is.SameAs(_quantity));
                Assert.That(e.PropertyName, Is.EqualTo("Unit"));
                ++changed;
            };

            VerifyPropertyChanged(
                _quantity, handler,
                q =>
                {
                    var old = changed;
                    q.Unit = "s";
                    Assert.That(changed, Is.GreaterThan(old));
                });

            VerifyPropertyChanged(
                _quantity, handler,
                q =>
                {
                    var old = changed;
                    q.Unit = "s";
                    Assert.That(changed, Is.EqualTo(old));
                });
        }

        private static void VerifyPropertyChanged<T>(
            T obj, PropertyChangedEventHandler handler,
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
