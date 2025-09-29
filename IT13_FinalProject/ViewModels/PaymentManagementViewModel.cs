using IT13_FinalProject.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using System;
using System.Linq;

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
                new Payment { PaymentId = 1, CustomerName = "Alice Smith", ServiceName = "Haircut", Amount = 50.00m, PaymentMethod = "Cash", PaymentDate = new DateTime(2025, 8, 1), Status = "Paid" },
                new Payment { PaymentId = 2, CustomerName = "Bob Johnson", ServiceName = "Massage", Amount = 35.00m, PaymentMethod = "E-Wallet", PaymentDate = new DateTime(2025, 8, 2), Status = "Pending" },
                new Payment { PaymentId = 3, CustomerName = "Carol Lee", ServiceName = "Facial", Amount = 40.00m, PaymentMethod = "Card", PaymentDate = new DateTime(2025, 8, 3), Status = "Paid" }
            };

            Payments = new ObservableCollection<Payment>(_allPayments);

            AddPaymentCommand = new Command(() =>
            {
                var modal = new AddPaymentModal(payment =>
                {
                    _allPayments.Add(payment);
                    Payments.Add(payment);
                });
                Application.Current?.MainPage?.ShowPopup(modal);
            });

            EditPaymentCommand = new Command<Payment>(payment =>
            {
                if (payment?.Status != "Pending")
                {
                    Application.Current?.MainPage?.DisplayAlert("Edit Not Allowed", "Only pending payments can be edited.", "OK");
                    return;
                }
                var modal = new EditPaymentModal(payment, editedPayment =>
                {
                    var index = Payments.IndexOf(payment);
                    if (index >= 0)
                    {
                        Payments[index] = editedPayment;
                        var allIndex = _allPayments.IndexOf(payment);
                        if (allIndex >= 0)
                            _allPayments[allIndex] = editedPayment;
                    }
                });
                Application.Current?.MainPage?.ShowPopup(modal);
            });

            DeletePaymentCommand = new Command<Payment>(async payment =>
            {
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
                var modal = new ReceiptModal(payment);
                Application.Current?.MainPage?.ShowPopup(modal);
            });

            ViewHistoryCommand = new Command<Payment>(payment =>
            {
                if (payment == null) return;
                var history = _allPayments.Where(x => x.CustomerName == payment.CustomerName).ToList();
                var modal = new PaymentHistoryModal(history);
                Application.Current?.MainPage?.ShowPopup(modal);
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