using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class EditAppointmentPage : ContentPage
{
    private Appointment _appointment;
    public EditAppointmentPage(Appointment appointment)
    {
        InitializeComponent();
        _appointment = appointment;
        CustomerEntry.Text = appointment.CustomerName;
        ServiceEntry.Text = appointment.ServiceName;
        StaffEntry.Text = appointment.AssignedStaff;
        DateEntry.Text = appointment.AppointmentDateTimeString;
        StatusPicker.SelectedItem = appointment.Status;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CustomerEntry.Text) &&
            !string.IsNullOrWhiteSpace(ServiceEntry.Text) &&
            !string.IsNullOrWhiteSpace(StaffEntry.Text) &&
            !string.IsNullOrWhiteSpace(DateEntry.Text) &&
            StatusPicker.SelectedItem is string status)
        {
            _appointment.CustomerName = CustomerEntry.Text;
            _appointment.ServiceName = ServiceEntry.Text;
            _appointment.AssignedStaff = StaffEntry.Text;
            _appointment.AppointmentDateTimeString = DateEntry.Text;
            _appointment.Status = status;
            await DisplayAlert("Success", "Appointment updated.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
