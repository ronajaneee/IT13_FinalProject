using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Maui.Controls;
using IT13_FinalProject; // Use the model from Models/Appointment.cs

namespace IT13_FinalProject;

public partial class AppointmentManagementView : ContentView, INotifyPropertyChanged
{
    public ObservableCollection<Appointment> Appointments { get; set; } = new();
    public ObservableCollection<Appointment> FilteredAppointments { get; set; } = new();
    public event PropertyChangedEventHandler PropertyChanged;

    public AppointmentManagementView()
    {
        InitializeComponent();

        // Hardcoded example appointments
        Appointments.Add(new Appointment {
            AppointmentId = "A1001",
            CustomerName = "Anna Smith",
            ServiceName = "Massage",
            AssignedStaff = "David Chen",
            AppointmentDateTimeString = "2024-06-10 10:00",
            Status = "Confirmed"
        });
        Appointments.Add(new Appointment {
            AppointmentId = "A1002",
            CustomerName = "John Doe",
            ServiceName = "Facial",
            AssignedStaff = "Maria Garcia",
            AppointmentDateTimeString = "2024-06-10 11:30",
            Status = "Pending"
        });
        Appointments.Add(new Appointment {
            AppointmentId = "A1003",
            CustomerName = "Sarah Johnson",
            ServiceName = "Hair Treatment",
            AssignedStaff = "Anna Smith",
            AppointmentDateTimeString = "2024-06-11 09:00",
            Status = "Completed"
        });
        Appointments.Add(new Appointment {
            AppointmentId = "A1004",
            CustomerName = "Maria Garcia",
            ServiceName = "Massage",
            AssignedStaff = "David Chen",
            AppointmentDateTimeString = "2024-06-11 14:00",
            Status = "Cancelled"
        });
        FilteredAppointments = new ObservableCollection<Appointment>(Appointments);
        BindingContext = this;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";
        var filtered = Appointments.Where(a =>
            (a.CustomerName?.ToLower().Contains(searchText) ?? false) ||
            (a.ServiceName?.ToLower().Contains(searchText) ?? false) ||
            (a.AssignedStaff?.ToLower().Contains(searchText) ?? false) ||
            (a.AppointmentDateTimeString?.ToLower().Contains(searchText) ?? false)
        ).ToList();
        FilteredAppointments.Clear();
        foreach (var appt in filtered)
            FilteredAppointments.Add(appt);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredAppointments)));
    }

    private async void OnAddAppointmentClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AddAppointmentPage(Appointments, FilteredAppointments));
    }

    private async void OnEditAppointmentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditAppointmentPage(appt));
        }
    }

    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AppointmentDetailsPage(appt));
        }
    }

    private async void OnDeleteAppointmentClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete appointment {appt.AppointmentId}?",
                "Yes", "No");
            if (confirm)
            {
                Appointments.Remove(appt);
                FilteredAppointments.Remove(appt);
            }
        }
    }

    private async void OnStatusUpdateClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            string newStatus = await Application.Current.MainPage.DisplayActionSheet(
                "Update Status",
                "Cancel",
                null,
                "Confirmed", "Pending", "Completed", "Cancelled");
            if (!string.IsNullOrEmpty(newStatus) && newStatus != "Cancel")
            {
                appt.Status = newStatus;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredAppointments)));
            }
        }
    }
}
