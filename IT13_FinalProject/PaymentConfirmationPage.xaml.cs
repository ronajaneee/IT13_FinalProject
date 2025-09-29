using Microsoft.Maui.Controls;
using System;

namespace IT13_FinalProject;

public partial class PaymentConfirmationPage : ContentPage
{
    public PaymentConfirmationPage(string method)
    {
        InitializeComponent();
        MethodLabel.Text = $"Confirm {method} Payment";
    }

    private async void OnConfirmPaymentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(AccountEntry.Text) || string.IsNullOrWhiteSpace(PinEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }
        await DisplayAlert("Success", "Payment confirmed (demo only)", "OK");
        await Navigation.PopAsync();
    }
}
