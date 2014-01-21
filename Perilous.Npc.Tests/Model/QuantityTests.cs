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
            _quantity = new Quantity();
        }

        [Test]
        public void Test_That_Value_PropertyChanged()
        {
            var changed = false;

            VerifyPropertyChanged(_quantity,
                (s, e) =>
                {
                    Assert.That(s, Is.SameAs(_quantity));
                    Assert.That(e.PropertyName, Is.EqualTo("Value"));
                    changed = true;
                },
                q => q.Value = 1d,
                ref changed);
        }

        [Test]
        public void Test_That_Unit_PropertyChanged()
        {
            var changed = false;

            VerifyPropertyChanged(_quantity,
                (s, e) =>
                {
                    Assert.That(s, Is.SameAs(_quantity));
                    Assert.That(e.PropertyName, Is.EqualTo("Unit"));
                    changed = true;
                },
                q => q.Unit = "s",
                ref changed);
        }

        private static void VerifyPropertyChanged<T>(
            T obj, PropertyChangedEventHandler handler,
            Action<T> action, ref bool changed)
            where T : INotifyPropertyChanged
        {
            try
            {
                obj.PropertyChanged += handler;

                action(obj);

                Assert.That(changed, Is.True);
            }
            finally
            {
                obj.PropertyChanged -= handler;
            }
        }
    }
}
