using IT13_FinalProject.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Text.RegularExpressions;

namespace IT13_FinalProject;

public class AddPaymentModal : Popup
{
    public AddPaymentModal(Action<Payment> onSave)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var customerNameEntry = new Entry { Placeholder = "Customer Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var serviceNameEntry = new Entry { Placeholder = "Service Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var amountEntry = new Entry { Placeholder = "Amount", Keyboard = Keyboard.Numeric, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var paymentMethodPicker = new Picker { Title = "Payment Method", ItemsSource = new[] { "Cash", "Card", "E-Wallet" }, SelectedIndex = 0, TextColor = Colors.White };
        var paymentDatePicker = new DatePicker { Date = DateTime.Today, TextColor = Colors.White };
        var statusPicker = new Picker { Title = "Status", ItemsSource = new[] { "Paid", "Pending" }, SelectedIndex = 0, TextColor = Colors.White };
        var cardForm = new VerticalStackLayout { IsVisible = false };
        var cardholderNameEntry = new Entry { Placeholder = "Cardholder Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var cardNumberEntry = new Entry { Placeholder = "Card Number", Keyboard = Keyboard.Numeric, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var expirationEntry = new Entry { Placeholder = "MM/YY", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var cvvEntry = new Entry { Placeholder = "CVV", Keyboard = Keyboard.Numeric, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        cardForm.Children.Add(cardholderNameEntry);
        cardForm.Children.Add(cardNumberEntry);
        cardForm.Children.Add(expirationEntry);
        cardForm.Children.Add(cvvEntry);
        paymentMethodPicker.SelectedIndexChanged += (s, e) =>
        {
            cardForm.IsVisible = paymentMethodPicker.SelectedItem?.ToString() == "Card";
        };
        var saveBtn = new Button
        {
            Text = "Save",
            BackgroundColor = Color.FromArgb("#3b82f6"),
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120,
            FontAttributes = FontAttributes.Bold
        };
        var closeBtn = new Button
        {
            Text = "Back / Close",
            BackgroundColor = Color.FromArgb("#ef4444"),
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120
        };
        saveBtn.Clicked += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(customerNameEntry.Text) ||
                string.IsNullOrWhiteSpace(serviceNameEntry.Text) ||
                string.IsNullOrWhiteSpace(amountEntry.Text) ||
                paymentMethodPicker.SelectedIndex < 0 ||
                statusPicker.SelectedIndex < 0)
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }
            if (!decimal.TryParse(amountEntry.Text, out var amount))
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Amount must be a valid number.", "OK");
                return;
            }
            if (paymentMethodPicker.SelectedItem?.ToString() == "Card")
            {
                if (string.IsNullOrWhiteSpace(cardholderNameEntry.Text) ||
                    string.IsNullOrWhiteSpace(cardNumberEntry.Text) ||
                    string.IsNullOrWhiteSpace(expirationEntry.Text) ||
                    string.IsNullOrWhiteSpace(cvvEntry.Text))
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "Please fill in all card details.", "OK");
                    return;
                }
                if (cardNumberEntry.Text.Length != 16 || !Regex.IsMatch(cardNumberEntry.Text, "^\\d{16}$"))
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "Card number must be 16 digits.", "OK");
                    return;
                }
                if (!Regex.IsMatch(expirationEntry.Text, "^(0[1-9]|1[0-2])\\/\\d{2}$"))
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "Expiration date must be in MM/YY format.", "OK");
                    return;
                }
                if (cvvEntry.Text.Length != 3 || !Regex.IsMatch(cvvEntry.Text, "^\\d{3}$"))
                {
                    Application.Current?.MainPage?.DisplayAlert("Error", "CVV must be 3 digits.", "OK");
                    return;
                }
            }
            var newPayment = new Payment
            {
                PaymentId = (int)(DateTime.Now.Ticks % 1000000),
                CustomerName = customerNameEntry.Text,
                ServiceName = serviceNameEntry.Text,
                Amount = amount,
                PaymentMethod = paymentMethodPicker.SelectedItem.ToString(),
                PaymentDate = paymentDatePicker.Date,
                Status = statusPicker.SelectedItem.ToString()
            };
            onSave?.Invoke(newPayment);
            Close();
        };
        closeBtn.Clicked += (s, e) => Close();
        Content = new Frame
        {
            Padding = 24,
            CornerRadius = 16,
            BackgroundColor = Color.FromArgb("#1f2937"),
            BorderColor = Color.FromArgb("#3b82f6"),
            HasShadow = true,
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Spacing = 14,
                    Children =
                    {
                        new Label
                        {
                            Text = "Add Payment",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        customerNameEntry, serviceNameEntry, amountEntry, paymentMethodPicker, statusPicker, paymentDatePicker, cardForm,
                        new HorizontalStackLayout
                        {
                            Spacing = 20,
                            HorizontalOptions = LayoutOptions.Center,
                            Children = { saveBtn, closeBtn }
                        }
                    }
                }
            }
        };
    }
}
