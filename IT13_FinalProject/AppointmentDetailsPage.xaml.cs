using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class AppointmentDetailsPage : ContentPage
{
    public AppointmentDetailsPage(Appointment appointment)
    {
        InitializeComponent();
        IdLabel.Text = $"ID: {appointment.AppointmentId}";
        CustomerLabel.Text = $"Customer: {appointment.CustomerName}";
        ServiceLabel.Text = $"Service: {appointment.ServiceName}";
        StaffLabel.Text = $"Staff: {appointment.AssignedStaff}";
        DateLabel.Text = $"Date: {appointment.AppointmentDateTimeString}";
        StatusLabel.Text = $"Status: {appointment.Status}";
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
