using IT13_FinalProject.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace IT13_FinalProject;

public class EditPaymentModal : Popup
{
    public EditPaymentModal(Payment payment, Action<Payment> onSave)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var customerNameEntry = new Entry { Text = payment.CustomerName, Placeholder = "Customer Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var serviceNameEntry = new Entry { Text = payment.ServiceName, Placeholder = "Service Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var amountEntry = new Entry { Text = payment.Amount.ToString(), Placeholder = "Amount", Keyboard = Keyboard.Numeric, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var paymentMethodEntry = new Entry { Text = payment.PaymentMethod, Placeholder = "Payment Method", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var paymentDatePicker = new DatePicker { Date = payment.PaymentDate, TextColor = Colors.White };
        var statusEntry = new Entry { Text = payment.Status, Placeholder = "Status", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
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
            payment.CustomerName = customerNameEntry.Text;
            payment.ServiceName = serviceNameEntry.Text;
            payment.Amount = decimal.TryParse(amountEntry.Text, out var amt) ? amt : 0;
            payment.PaymentMethod = paymentMethodEntry.Text;
            payment.PaymentDate = paymentDatePicker.Date;
            payment.Status = statusEntry.Text;
            onSave?.Invoke(payment);
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
                            Text = "Edit Payment",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        customerNameEntry, serviceNameEntry, amountEntry, paymentMethodEntry, paymentDatePicker, statusEntry,
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
