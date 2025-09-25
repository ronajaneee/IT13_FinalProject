using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class AddAppointmentPage : ContentPage
{
    private ObservableCollection<Appointment> _appointments;
    private ObservableCollection<Appointment> _filteredAppointments;
    public AddAppointmentPage(ObservableCollection<Appointment> appointments, ObservableCollection<Appointment> filteredAppointments)
    {
        InitializeComponent();
        _appointments = appointments;
        _filteredAppointments = filteredAppointments;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(CustomerEntry.Text) &&
            !string.IsNullOrWhiteSpace(ServiceEntry.Text) &&
            !string.IsNullOrWhiteSpace(StaffEntry.Text) &&
            !string.IsNullOrWhiteSpace(DateEntry.Text) &&
            StatusPicker.SelectedItem is string status)
        {
            var newAppointment = new Appointment
            {
                AppointmentId = "A" + (_appointments.Count + 1001),
                CustomerName = CustomerEntry.Text,
                ServiceName = ServiceEntry.Text,
                AssignedStaff = StaffEntry.Text,
                AppointmentDateTimeString = DateEntry.Text,
                Status = status
            };
            _appointments.Add(newAppointment);
            _filteredAppointments.Add(newAppointment);
            await DisplayAlert("Success", "Appointment added.", "OK");
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
