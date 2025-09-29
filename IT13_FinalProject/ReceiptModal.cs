using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class Payment
{
    public int PaymentId { get; set; }
    public string CustomerName { get; set; }
    public string ServiceName { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
}

public class ReceiptModal : Popup
{
    public ReceiptModal(Payment payment)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var closeBtn = new Button
        {
            Text = "Back / Close",
            BackgroundColor = Color.FromArgb("#ef4444"),
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 140
        };
        closeBtn.Clicked += (s, e) => Close();
        Content = new Frame
        {
            Padding = 24,
            CornerRadius = 16,
            BackgroundColor = Color.FromArgb("#1f2937"),
            BorderColor = Color.FromArgb("#3b82f6"),
            HasShadow = true,
            Content = new VerticalStackLayout
            {
                Spacing = 14,
                Children =
                {
                    new Label
                    {
                        Text = $"Receipt {payment.PaymentId}",
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromArgb("#3b82f6"),
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Label { Text = $"Customer: {payment.CustomerName}", TextColor = Colors.White, FontSize = 16 },
                    new Label { Text = $"Service: {payment.ServiceName}", TextColor = Colors.White, FontSize = 16 },
                    new Label { Text = $"Amount: ${payment.Amount:F2}", TextColor = Colors.White, FontSize = 16 },
                    new Label { Text = $"Date: {payment.PaymentDate}", TextColor = Colors.White, FontSize = 16 },
                    new Label { Text = $"Method: {payment.PaymentMethod}", TextColor = Colors.White, FontSize = 16 },
                    new Label { Text = $"Status: {payment.Status}", TextColor = Colors.White, FontSize = 16 },
                    closeBtn
                }
            }
        };
    }
}
