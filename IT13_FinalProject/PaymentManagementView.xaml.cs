using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class PaymentManagementView : ContentView
{
    private ObservableCollection<Payment> _allPayments = new();
    public ObservableCollection<Payment> Payments { get; set; } = new();

    public PaymentManagementView()
    {
        // Ensure the XAML file exists and is named PaymentManagementView.xaml
        // The class must be declared as 'partial' and match the x:Class in XAML
        InitializeComponent();

        // Hardcoded payments
        _allPayments.Add(new Payment { PaymentId = "P001", CustomerName = "Alice Smith", ServiceName = "Haircut", AppointmentId = "A100", Amount = 50.00m, PaymentMethod = "Cash", PaymentDate = "2025-08-01", Status = "Paid" });
        _allPayments.Add(new Payment { PaymentId = "P002", CustomerName = "Bob Johnson", ServiceName = "Massage", AppointmentId = "A101", Amount = 35.00m, PaymentMethod = "E-Wallet", PaymentDate = "2025-08-02", Status = "Pending" });
        _allPayments.Add(new Payment { PaymentId = "P003", CustomerName = "Carol Lee", ServiceName = "Facial", AppointmentId = "A102", Amount = 40.00m, PaymentMethod = "Card", PaymentDate = "2025-08-03", Status = "Paid" });

        foreach (var p in _allPayments)
            Payments.Add(p);

        BindingContext = this;

        // Listen for new payments
        MessagingCenter.Subscribe<AddPaymentPage, Payment>(this, "PaymentAdded", (sender, payment) =>
        {
            Payments.Add(payment);
            _allPayments.Add(payment);
        });
        // Listen for edited payments
        MessagingCenter.Subscribe<EditPaymentPage, Payment>(this, "PaymentEdited", (sender, payment) =>
        {
            // Refresh list
            Payments.Clear();
            foreach (var p in _allPayments)
                Payments.Add(p);
        });
    }

    private async void OnAddPaymentClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AddPaymentPage());
    }

    private async void OnEditPaymentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            if (p.Status != "Pending")
            {
                await Application.Current.MainPage.DisplayAlert("Edit Not Allowed", "Only pending payments can be edited.", "OK");
                return;
            }
            // Navigate to EditPaymentPage, passing the payment to edit
            await Application.Current.MainPage.Navigation.PushAsync(new EditPaymentPage(p));
        }
    }

    private async void OnDeletePaymentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Delete Payment", "Are you sure you want to delete this payment?", "Yes", "No");
            if (confirm)
            {
                Payments.Remove(p);
                _allPayments.Remove(p);
            }
        }
    }

    private async void OnViewReceiptClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(p));
        }
    }

    private async void OnViewBillingHistoryClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            var history = _allPayments.Where(x => x.AppointmentId == p.AppointmentId).ToList();
            await Application.Current.MainPage.Navigation.PushAsync(new PaymentHistoryPage(history));
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLower() ?? "";
        Payments.Clear();
        foreach (var p in _allPayments)
        {
            if (p.CustomerName.ToLower().Contains(query) || p.ServiceName.ToLower().Contains(query))
                Payments.Add(p);
        }
    }
}

public class Payment
{
    public string PaymentId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string AppointmentId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty; // For legacy compatibility
    public string PaymentDate { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty; // For legacy compatibility
    public string Status { get; set; } = "Paid";
}
