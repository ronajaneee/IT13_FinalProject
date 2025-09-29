using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class ViewAppointmentModal : Popup
{
    public ViewAppointmentModal(Appointment appointment)
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
                            Text = $"Appointment {appointment.AppointmentId}",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        new Label { Text = $"Customer: {appointment.CustomerName}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Service: {appointment.ServiceName}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Staff: {appointment.AssignedStaff}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Date: {appointment.AppointmentDateTimeString}", TextColor = Colors.White, FontSize = 16 },
                        new Label { Text = $"Status: {appointment.Status}", TextColor = Colors.White, FontSize = 16 },
                        closeBtn
                    }
                }
            }
        };
    }
}
