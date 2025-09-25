using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class PaymentManagementView : ContentView
{
    public ObservableCollection<Payment> Payments { get; set; } = new();

    public PaymentManagementView()
    {
        InitializeComponent();

        // Hardcoded payments
        Payments.Add(new Payment { PaymentId = "P001", CustomerName = "Alice Smith", AppointmentId = "A100", Amount = 50.00m, Method = "Cash", Date = "2025-08-01" });
        Payments.Add(new Payment { PaymentId = "P002", CustomerName = "Bob Johnson", AppointmentId = "A101", Amount = 35.00m, Method = "E-Wallet", Date = "2025-08-02" });
        Payments.Add(new Payment { PaymentId = "P003", CustomerName = "Carol Lee", AppointmentId = "A102", Amount = 40.00m, Method = "Card", Date = "2025-08-03" });

        BindingContext = this;
    }

    private async void OnAddPaymentClicked(object sender, EventArgs e)
    {
        // Simple prompt-based payment entry (for demo)
        string customer = await Application.Current.MainPage.DisplayPromptAsync("Record Payment", "Customer name:");
        string appointment = await Application.Current.MainPage.DisplayPromptAsync("Record Payment", "Appointment ID:");
        string amountStr = await Application.Current.MainPage.DisplayPromptAsync("Record Payment", "Amount:", keyboard: Keyboard.Numeric);
        string method = await Application.Current.MainPage.DisplayActionSheet("Payment Method", "Cancel", null, "Cash", "Card", "E-Wallet");

        if (!string.IsNullOrEmpty(customer) && !string.IsNullOrEmpty(amountStr) && decimal.TryParse(amountStr, out var amount))
        {
            var id = "P" + (Payments.Count + 1).ToString("D3");
            Payments.Add(new Payment { PaymentId = id, CustomerName = customer, AppointmentId = appointment, Amount = amount, Method = method, Date = DateTime.Now.ToString("yyyy-MM-dd") });
        }
    }

    private void OnEditPaymentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            p.CustomerName += " (Edited)";
        }
    }

    private void OnDeletePaymentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            Payments.Remove(p);
        }
    }

    private async void OnViewReceiptClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            await Application.Current.MainPage.DisplayAlert($"Receipt {p.PaymentId}", $"Customer: {p.CustomerName}\nAmount: ${p.Amount:F2}\nMethod: {p.Method}\nDate: {p.Date}", "OK");
        }
    }

    private async void OnViewBillingHistoryClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Payment p)
        {
            // For demo: show the billing history by appointment id
            var history = Payments.Where(x => x.AppointmentId == p.AppointmentId).ToList();
            var msg = string.Join("\n", history.Select(h => $"{h.Date}: {h.CustomerName} - ${h.Amount:F2} ({h.Method})"));
            await Application.Current.MainPage.DisplayAlert($"Billing History for {p.AppointmentId}", msg, "OK");
        }
    }
}

public class Payment
{
    public string PaymentId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string AppointmentId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}
