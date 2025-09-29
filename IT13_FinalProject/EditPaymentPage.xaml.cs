using System;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject
{
    public partial class EditPaymentPage : ContentPage
    {
        private Payment _payment;
        public EditPaymentPage(Payment payment)
        {
            InitializeComponent();
            _payment = payment;
            // Pre-fill fields
            CustomerNameEntry.Text = payment.CustomerName;
            ServiceNameEntry.Text = payment.ServiceName;
            AmountEntry.Text = payment.Amount.ToString();
            PaymentMethodPicker.SelectedItem = payment.PaymentMethod;
            PaymentDatePicker.Date = DateTime.TryParse(payment.PaymentDate, out var dt) ? dt : DateTime.Today;
            StatusPicker.SelectedItem = payment.Status;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(CustomerNameEntry.Text) ||
                string.IsNullOrWhiteSpace(ServiceNameEntry.Text) ||
                string.IsNullOrWhiteSpace(AmountEntry.Text) ||
                PaymentMethodPicker.SelectedItem == null ||
                StatusPicker.SelectedItem == null)
            {
                await DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }
            if (!decimal.TryParse(AmountEntry.Text, out var amount))
            {
                await DisplayAlert("Validation Error", "Amount must be a valid number.", "OK");
                return;
            }
            // Update payment
            _payment.CustomerName = CustomerNameEntry.Text;
            _payment.ServiceName = ServiceNameEntry.Text;
            _payment.Amount = amount;
            _payment.PaymentMethod = PaymentMethodPicker.SelectedItem.ToString();
            _payment.PaymentDate = PaymentDatePicker.Date.ToString("yyyy-MM-dd");
            _payment.Status = StatusPicker.SelectedItem.ToString();
            MessagingCenter.Send(this, "PaymentEdited", _payment);
            await Navigation.PopAsync();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // You can add navigation or logic here, e.g.:
            // await Navigation.PopAsync();
        }
    }
}
