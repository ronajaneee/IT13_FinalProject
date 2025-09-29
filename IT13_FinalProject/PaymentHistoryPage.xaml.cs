using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System;

namespace IT13_FinalProject;

public partial class PaymentHistoryPage : ContentPage
{
    public PaymentHistoryPage(IEnumerable<Payment> payments)
    {
        InitializeComponent();
        HistoryCollectionView.ItemsSource = payments;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
