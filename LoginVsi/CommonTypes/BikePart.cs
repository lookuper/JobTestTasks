using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public abstract class BikePart : IBikePart, INotifyPropertyChanged
    {
        protected string _name;
        protected string _description;
        protected int _partPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string Name
        {
            get
            {
                return _name;
            }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public virtual string Description
        {
            get
            {
                return _description;
            }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public virtual int Price
        {
            get { return _partPrice; }
            set { _partPrice = value; OnPropertyChanged("Price"); }
        }

        protected BikePart(string name, string description, int price = 0)
        {
            _name = name;
            _description = description;
            Price = price;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler == null)
                return;

            var eventArgs = new PropertyChangedEventArgs(propertyName);
            handler(this, eventArgs);
        }
    }
}
