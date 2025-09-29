using Microsoft.Maui.Controls;
using System;

namespace IT13_FinalProject;

public partial class ReceiptPage : ContentPage
{
    public ReceiptPage(Payment payment)
    {
        InitializeComponent();
        ReceiptLabel.Text = $"Receipt {payment.PaymentId}";
        DetailsLabel.Text = $"Customer: {payment.CustomerName}\nService: {payment.ServiceName}\nAmount: ${payment.Amount:F2}\nDate: {payment.PaymentDate}\nMethod: {payment.PaymentMethod}\nStatus: {payment.Status}";
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
