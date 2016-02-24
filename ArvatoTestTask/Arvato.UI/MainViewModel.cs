using Arvato.BusinessLogic;
using Arvato.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arvato.UI
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ParkingController _parkingController = new ParkingController();

        private List<IParkingHouse> _allParkings = new List<IParkingHouse>();
        public List<IParkingHouse> AllParkings
        {
            get { return _allParkings; }
            set { _allParkings = value; OnPropertyChanged("AllParkings"); }
        }

        private IParkingHouse _selectedParking = null;
        public IParkingHouse SelectedParking
        {
            get { return _selectedParking; }
            set {
                _selectedParking = value;
                OnPropertyChanged("SelectedParking");
                ParkingCustomers = _selectedParking?.GetAllCustomers();
            }
        }

        private List<Customer> _parkingCustomers = new List<Customer>();
        public List<Customer> ParkingCustomers
        {
            get { return _parkingCustomers; }
            set { _parkingCustomers = value;  OnPropertyChanged("ParkingCustomers"); }
        }

        private List<Visit> _customerVisits = new List<Visit>();
        public List<Visit> CustomerVisits
        {
            get { return _customerVisits; }
            set { _customerVisits = value; OnPropertyChanged("CustomerVisits"); }
        }


        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        private Dictionary<Moths, String> _months;
        public Dictionary<Moths, String> Months
        {
            get { return _months; }
            set { _months = value; OnPropertyChanged("Months"); }
        }

        private Moths _selectedMonth;
        public Moths SelectedMonth
        {
            get { return _selectedMonth; }
            set { _selectedMonth = value; OnPropertyChanged("SelectedMonth"); }
        }

        private List<Invoice> _avaliableInvoices;
        public List<Invoice> AvaliableInvoinces
        {
            get { return _avaliableInvoices; }
            set { _avaliableInvoices = value; OnPropertyChanged("AvaliableInvoinces"); }
        }

        private Visit _selectedVisit;
        public Visit SelectedVisit
        {
            get { return _selectedVisit; }
            set { _selectedVisit = value; OnPropertyChanged("SelectedVisit"); }
        }

        public ICommand GetCustomerVisitsCommand { get; set; }

        public MainViewModel()
        {
            AllParkings = _parkingController.AvaliableParking;
            SelectedParking = AllParkings.FirstOrDefault();
            GetCustomerVisitsCommand = new RelayCommand<Object>(GetCustomerVisitsHanler);

            Months = Enum.GetValues(typeof(Moths))
               .Cast<Moths>()
               .ToDictionary(t => t, t => t.ToString());
        }

        private void GetCustomerVisitsHanler(object obj)
        {
            AvaliableInvoinces = _parkingController.GetAllInvoicesForMonth(SelectedMonth);
        }
    }
}
