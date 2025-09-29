using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class ViewStaffModal : Popup
{
    public ViewStaffModal(Staff staff)
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

        var activitiesLayout = new VerticalStackLayout { Spacing = 6 };
        foreach (var activity in staff.RecentActivities)
        {
            activitiesLayout.Add(new Label
            {
                Text = activity,
                FontSize = 14,
                TextColor = Color.FromArgb("#d1d5db")
            });
        }

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
        AddRow("Position:", staff.Position);
        AddRow("Email:", staff.Email);
        AddRow("Phone:", staff.Phone);
        AddRow("Status:", staff.Status);

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
                    Padding = 6,
                    Spacing = 16,
                    Children =
                    {
                        new Label
                        {
                            Text = $"{staff.FirstName} {staff.LastName}",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        detailsGrid,
                        new Label
                        {
                            Text = "Recent Activities:",
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Colors.White
                        },
                        activitiesLayout,
                        closeBtn
                    }
                }
            }
        };
    }
}
