using CommonTypes;
using CommonTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BikeViewModel : BaseViewModel
    {
        private int _totalPrice;
        private List<BikePart> _avaliableParts = new List<BikePart>()
        {
            new FrontWheel(),
            new BackWheel(),
            new HandleBar(),
            new Seat(),
            new Frame()
        };

        public int TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

        public IEnumerable<IBikePart> AvaliableParts
        {
            get { return _avaliableParts; }
        }

        public BikeViewModel()
        {
            RecalculatePrice();
        }

        /// <summary>
        /// Recalculates total price of parts
        /// </summary>
        public void RecalculatePrice()
        {
            TotalPrice = AvaliableParts.Sum(x => x.Price);
        }
    }
}
