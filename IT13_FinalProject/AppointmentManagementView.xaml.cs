using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class AppointmentManagementView : ContentView
{
    public ObservableCollection<Appointment> Appointments { get; set; } = new();
    public ObservableCollection<string> StaffNames { get; set; } = new();

    public AppointmentManagementView()
    {
        InitializeComponent();

        // Hardcoded staff names (should match StaffManagementView example)
        StaffNames.Add("Jane Doe");
        StaffNames.Add("John Smith");
        StaffNames.Add("Emily Clark");

        // Hardcoded appointments
        Appointments.Add(new Appointment { AppointmentId = "A100", ServiceName = "Massage", CustomerName = "Alice Smith", DateTime = "2025-08-10 10:00", Status = "Pending", AssignedStaff = "" });
        Appointments.Add(new Appointment { AppointmentId = "A101", ServiceName = "Facial", CustomerName = "Bob Johnson", DateTime = "2025-08-11 14:00", Status = "Approved", AssignedStaff = "Jane Doe" });
        Appointments.Add(new Appointment { AppointmentId = "A102", ServiceName = "Hair Treatment", CustomerName = "Carol Lee", DateTime = "2025-08-12 09:00", Status = "Pending", AssignedStaff = "" });

        BindingContext = this;
    }

    private void OnAddAppointmentClicked(object sender, EventArgs e)
    {
        Appointments.Add(new Appointment { AppointmentId = "A" + (Appointments.Count + 100), ServiceName = "New Service", CustomerName = "New Customer", DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), Status = "Pending", AssignedStaff = "" });
    }

    private void OnApproveClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            appt.Status = "Approved";
        }
    }

    private async void OnRescheduleClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            string newDate = await Application.Current.MainPage.DisplayPromptAsync("Reschedule", "Enter new date/time (yyyy-MM-dd HH:mm):");
            if (!string.IsNullOrEmpty(newDate))
            {
                appt.DateTime = newDate;
                appt.Status = "Rescheduled";
            }
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            appt.Status = "Cancelled";
        }
    }

    private async void OnAssignStaffClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Appointment appt)
        {
            string selected = await Application.Current.MainPage.DisplayActionSheet($"Assign Staff for {appt.AppointmentId}", "Cancel", null, StaffNames.ToArray());
            if (!string.IsNullOrEmpty(selected) && selected != "Cancel")
            {
                appt.AssignedStaff = selected;
                appt.Status = "Assigned";
            }
        }
    }
}

public class Appointment
{
    public string AppointmentId { get; set; }
    public string ServiceName { get; set; }
    public string CustomerName { get; set; }
    public string DateTime { get; set; }
    public string Status { get; set; }
    public string AssignedStaff { get; set; }
}
