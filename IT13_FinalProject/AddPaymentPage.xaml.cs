using Microsoft.Maui.Controls;
using System;
using System.Text.RegularExpressions;

namespace IT13_FinalProject;

public partial class AddPaymentPage : ContentPage
{
    public AddPaymentPage()
    {
        InitializeComponent();
        PaymentMethodPicker.SelectedIndex = 0;
        StatusPicker.SelectedIndex = 0;
        PaymentDatePicker.Date = DateTime.Today;
        CardForm.IsVisible = false;
    }

    private void OnPaymentMethodChanged(object sender, EventArgs e)
    {
        CardForm.IsVisible = PaymentMethodPicker.SelectedItem?.ToString() == "Card";
    }

    private async void OnConfirmClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CustomerNameEntry.Text) ||
            string.IsNullOrWhiteSpace(ServiceNameEntry.Text) ||
            string.IsNullOrWhiteSpace(AppointmentIdEntry.Text) ||
            string.IsNullOrWhiteSpace(AmountEntry.Text) ||
            PaymentMethodPicker.SelectedIndex < 0 ||
            StatusPicker.SelectedIndex < 0)
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }
        if (!decimal.TryParse(AmountEntry.Text, out var amount))
        {
            await DisplayAlert("Error", "Amount must be a valid number.", "OK");
            return;
        }
        // Card payment validation
        if (PaymentMethodPicker.SelectedItem?.ToString() == "Card")
        {
            if (string.IsNullOrWhiteSpace(CardholderNameEntry.Text) ||
                string.IsNullOrWhiteSpace(CardNumberEntry.Text) ||
                string.IsNullOrWhiteSpace(ExpirationEntry.Text) ||
                string.IsNullOrWhiteSpace(CVVEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all card details.", "OK");
                return;
            }
            if (CardNumberEntry.Text.Length != 16 || !Regex.IsMatch(CardNumberEntry.Text, "^\\d{16}$"))
            {
                await DisplayAlert("Error", "Card number must be 16 digits.", "OK");
                return;
            }
            if (!Regex.IsMatch(ExpirationEntry.Text, "^(0[1-9]|1[0-2])\\/\\d{2}$"))
            {
                await DisplayAlert("Error", "Expiration date must be in MM/YY format.", "OK");
                return;
            }
            if (CVVEntry.Text.Length != 3 || !Regex.IsMatch(CVVEntry.Text, "^\\d{3}$"))
            {
                await DisplayAlert("Error", "CVV must be 3 digits.", "OK");
                return;
            }
        }
        // Create new payment
        var newPayment = new Payment
        {
            PaymentId = $"P{DateTime.Now.Ticks % 1000000}",
            CustomerName = CustomerNameEntry.Text,
            ServiceName = ServiceNameEntry.Text,
            AppointmentId = AppointmentIdEntry.Text,
            Amount = amount,
            PaymentMethod = PaymentMethodPicker.SelectedItem.ToString(),
            PaymentDate = PaymentDatePicker.Date.ToString("yyyy-MM-dd"),
            Status = StatusPicker.SelectedItem.ToString()
        };
        // Optionally, you can store card details in newPayment if needed (not required for hardcoded example)
        MessagingCenter.Send(this, "PaymentAdded", newPayment);
        await DisplayAlert("Success", "Payment added.", "OK");
        await Navigation.PopAsync();
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        // You can add your cancel logic here, for example:
        // Navigation.PopAsync();
    }
}
