using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        protected ModelBase()
        {
        }
    }
}
