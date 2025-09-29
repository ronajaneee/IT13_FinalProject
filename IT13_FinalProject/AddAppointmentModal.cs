using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public class AddAppointmentModal : Popup
{
    public AddAppointmentModal(ObservableCollection<Appointment> appointments, ObservableCollection<Appointment> filteredAppointments)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var customerEntry = new Entry { Placeholder = "Customer Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var serviceEntry = new Entry { Placeholder = "Service Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var staffEntry = new Entry { Placeholder = "Staff Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var dateEntry = new Entry { Placeholder = "Date/Time", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var statusPicker = new Picker { Title = "Status", ItemsSource = new[] { "Confirmed", "Pending", "Completed", "Cancelled" }, SelectedIndex = 0, TextColor = Colors.White };
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
                var newAppointment = new Appointment
                {
                    AppointmentId = "A" + (appointments.Count + 1001),
                    CustomerName = customerEntry.Text,
                    ServiceName = serviceEntry.Text,
                    AssignedStaff = staffEntry.Text,
                    AppointmentDateTimeString = dateEntry.Text,
                    Status = status
                };
                appointments.Add(newAppointment);
                filteredAppointments.Add(newAppointment);
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
                            Text = "Add New Appointment",
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
