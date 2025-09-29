using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class EditAppointmentModal : Popup
{
    public EditAppointmentModal(Appointment appointment)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var customerEntry = new Entry { Placeholder = "Customer Name", Text = appointment.CustomerName, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var serviceEntry = new Entry { Placeholder = "Service Name", Text = appointment.ServiceName, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var staffEntry = new Entry { Placeholder = "Staff Name", Text = appointment.AssignedStaff, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var dateEntry = new Entry { Placeholder = "Date/Time", Text = appointment.AppointmentDateTimeString, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var statusPicker = new Picker { Title = "Status", ItemsSource = new[] { "Confirmed", "Pending", "Completed", "Cancelled" }, TextColor = Colors.White };
        statusPicker.SelectedItem = appointment.Status;
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
            if (!string.IsNullOrWhiteSpace(customerEntry.Text) &&
                !string.IsNullOrWhiteSpace(serviceEntry.Text) &&
                !string.IsNullOrWhiteSpace(staffEntry.Text) &&
                !string.IsNullOrWhiteSpace(dateEntry.Text) &&
                statusPicker.SelectedItem is string status)
            {
                appointment.CustomerName = customerEntry.Text;
                appointment.ServiceName = serviceEntry.Text;
                appointment.AssignedStaff = staffEntry.Text;
                appointment.AppointmentDateTimeString = dateEntry.Text;
                appointment.Status = status;
                Close();
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please fill in all fields.", "OK");
            }
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
                            Text = "Edit Appointment",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        customerEntry, serviceEntry, staffEntry, dateEntry, statusPicker,
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
