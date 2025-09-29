using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class ViewCustomerDetailsPopupV2 : Popup
{
    public ViewCustomerDetailsPopupV2(Customer customer)
    {
        var closeBtn = new Button
        {
            Text = "Back / Close",
            BackgroundColor = Color.FromArgb("#ef4444"), // modern red
            TextColor = Colors.White,
            CornerRadius = 8,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 140
        };
        closeBtn.Clicked += (s, e) => Close();

        // Booking history list
        var bookingHistoryLayout = new VerticalStackLayout { Spacing = 6 };
        foreach (var b in customer.BookingHistory)
        {
            bookingHistoryLayout.Add(new Label
            {
                Text = b,
                FontSize = 14,
                TextColor = Color.FromArgb("#d1d5db")
            });
        }

        // Grid for details (like a table)
        var detailsGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star }
            },
            RowSpacing = 8,
            ColumnSpacing = 16
        };

        void AddRow(string label, string value, Color? valueColor = null, bool bold = false)
        {
            int row = detailsGrid.RowDefinitions.Count;
            detailsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            detailsGrid.Add(new Label
            {
                Text = label,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White
            }, 0, row);

            detailsGrid.Add(new Label
            {
                Text = value,
                FontSize = 16,
                TextColor = valueColor ?? Color.FromArgb("#e5e7eb"),
                FontAttributes = bold ? FontAttributes.Bold : FontAttributes.None
            }, 1, row);
        }

        // Add rows
        AddRow("Email:", customer.Email);
        AddRow("Phone:", customer.Phone);
        AddRow("Address:", customer.Address);
        AddRow("Loyalty Points:", customer.LoyaltyPoints.ToString(), Color.FromArgb("#10b981"), true);

        // Main popup card
        Content = new Frame
        {
            Padding = 24,
            CornerRadius = 16,
            BackgroundColor = Color.FromArgb("#1f2937"), // deep slate
            BorderColor = Color.FromArgb("#3b82f6"),     // blue border
            HasShadow = true,
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Padding = 6,
                    Spacing = 16,
                    Children =
                    {
                        new Label
                        {
                            Text = $"{customer.FirstName} {customer.LastName}",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        detailsGrid,
                        new Label
                        {
                            Text = "Booking History:",
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Colors.White
                        },
                        bookingHistoryLayout,
                        closeBtn
                    }
                }
            }
        };
    }
}