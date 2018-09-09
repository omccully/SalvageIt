using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace SalvageIt.UnitTest.TestHelpers
{
    public class PropertyChangedQueue : Queue<string>
    {
        public PropertyChangedQueue(INotifyPropertyChanged obj)
        {
            obj.PropertyChanged += Obj_PropertyChanged;
        }

        private void Obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Enqueue(e.PropertyName);
        }
    }
}
