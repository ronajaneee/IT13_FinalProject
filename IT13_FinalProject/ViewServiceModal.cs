using IT13_FinalProject.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class ViewServiceModal : Popup
{
    public ViewServiceModal(Service service)
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
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Spacing = 14,
                    Children =
                    {
                        new Label
                        {
                            Text = service.Name,
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        new Label { Text = $"Price: ${service.Price:F2}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Duration: {service.Duration}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Description: {service.Description}", TextColor = Colors.White, FontSize = 16 },
                        closeBtn
                    }
                }
            }
        };
    }
}
