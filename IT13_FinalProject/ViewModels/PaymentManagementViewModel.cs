using IT13_FinalProject.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using System;
using System.Linq;
using IT13_FinalProject; // For UserSession and Payment

namespace IT13_FinalProject.ViewModels
{
    public class PaymentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Payment> _payments = new();
        private ObservableCollection<Payment> _allPayments = new();

        public ObservableCollection<Payment> Payments
        {
            get => _payments;
            set
            {
                _payments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Payments)));
            }
        }

        public ICommand AddPaymentCommand { get; }
        public ICommand EditPaymentCommand { get; }
        public ICommand DeletePaymentCommand { get; }
        public ICommand ViewReceiptCommand { get; }
        public ICommand ViewHistoryCommand { get; }

        public PaymentManagementViewModel()
        {
            // Initialize with sample data
            _allPayments = new ObservableCollection<Payment>
            {
                new Payment { PaymentId = "P001", CustomerName = "Alice Smith", ServiceName = "Haircut", AppointmentId = "A100", Amount = 50.00m, PaymentMethod = "Cash", PaymentDate = "2025-08-01", Status = "Paid" },
                new Payment { PaymentId = "P002", CustomerName = "Bob Johnson", ServiceName = "Massage", AppointmentId = "A101", Amount = 35.00m, PaymentMethod = "E-Wallet", PaymentDate = "2025-08-02", Status = "Pending" },
                new Payment { PaymentId = "P003", CustomerName = "Carol Lee", ServiceName = "Facial", AppointmentId = "A102", Amount = 40.00m, PaymentMethod = "Card", PaymentDate = "2025-08-03", Status = "Paid" }
            };

            Payments = new ObservableCollection<Payment>(_allPayments);

            AddPaymentCommand = new Command(() =>
            {
                if (UserSession.Role == "Staff" || UserSession.Role == "Admin")
                {
                    var page = new AddPaymentPage();
                    Application.Current?.MainPage?.Navigation.PushAsync(page);
                }
                else
                {
                    Application.Current?.MainPage?.DisplayAlert("Access Denied", "You do not have permission to add payments.", "OK");
                }
            });

            EditPaymentCommand = new Command<Payment>(payment =>
            {
                if (UserSession.Role != "Admin")
                {
                    Application.Current?.MainPage?.DisplayAlert("Access Denied", "Only Admin can edit payments.", "OK");
                    return;
                }
                if (payment?.Status != "Pending")
                {
                    Application.Current?.MainPage?.DisplayAlert("Edit Not Allowed", "Only pending payments can be edited.", "OK");
                    return;
                }
                var page = new EditPaymentPage(payment);
                Application.Current?.MainPage?.Navigation.PushAsync(page);
            });

            DeletePaymentCommand = new Command<Payment>(async payment =>
            {
                if (UserSession.Role != "Admin")
                {
                    await Application.Current.MainPage.DisplayAlert("Access Denied", "Only Admin can delete payments.", "OK");
                    return;
                }
                if (payment == null) return;
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Delete Payment",
                    "Are you sure you want to delete this payment?",
                    "Yes", "No");
                if (confirm)
                {
                    Payments.Remove(payment);
                    _allPayments.Remove(payment);
                }
            });

            ViewReceiptCommand = new Command<Payment>(payment =>
            {
                if (payment == null) return;
                var page = new ReceiptPage(payment);
                Application.Current?.MainPage?.Navigation.PushAsync(page);
            });

            ViewHistoryCommand = new Command<Payment>(payment =>
            {
                if (payment == null) return;
                var history = _allPayments.Where(x => x.CustomerName == payment.CustomerName).ToList();
                var page = new PaymentHistoryPage(history);
                Application.Current?.MainPage?.Navigation.PushAsync(page);
            });
        }

        public void FilterPayments(string searchText)
        {
            searchText = searchText?.ToLower() ?? "";
            Payments.Clear();
            var filtered = string.IsNullOrWhiteSpace(searchText) ? 
                _allPayments : 
                _allPayments.Where(p => 
                    p.CustomerName.ToLower().Contains(searchText) || 
                    p.ServiceName.ToLower().Contains(searchText));
            
            foreach (var payment in filtered)
                Payments.Add(payment);
        }
    }
}