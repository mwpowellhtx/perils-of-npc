using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Perilous.Npc
{
    public interface IModel : INotifyPropertyChanged
    {
    }

    public abstract class ModelBase : IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var propertyInfo = property.GetProperty();
            OnPropertyChanged(propertyInfo.Name);
        }

        protected virtual void SetProperty<TField, TProperty>(ref TField field, TField value,
            Expression<Func<TProperty>> property, Func<TField, TField, bool> changing,
            Action<TField, TField> after = null)
        {
            if (changing != null && !changing(field, value)) return;
            var old = field;
            field = value;
            OnPropertyChanged(property);
            if (after != null) after(old, field);
        }

        protected ModelBase()
        {
        }
    }
}
