using IT13_FinalProject.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace IT13_FinalProject;

public class PaymentHistoryModal : Popup
{
    public PaymentHistoryModal(IEnumerable<Payment> payments)
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
        var historyList = new CollectionView
        {
            ItemsSource = payments,
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid { Padding = 8, ColumnSpacing = 10 };
                var idLabel = new Label { FontSize = 14, TextColor = Colors.White };
                var amountLabel = new Label { FontSize = 14, TextColor = Colors.White };
                var dateLabel = new Label { FontSize = 14, TextColor = Colors.White };
                idLabel.SetBinding(Label.TextProperty, "PaymentId");
                amountLabel.SetBinding(Label.TextProperty, "Amount");
                dateLabel.SetBinding(Label.TextProperty, "PaymentDate");
                grid.Add(idLabel, 0, 0);
                grid.Add(amountLabel, 1, 0);
                grid.Add(dateLabel, 2, 0);
                return grid;
            })
        };
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
                        Text = "Payment History",
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromArgb("#3b82f6"),
                        HorizontalOptions = LayoutOptions.Center
                    },
                    historyList,
                    closeBtn
                }
            }
        };
    }
}
