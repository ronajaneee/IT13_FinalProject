using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class StaffManagementView : ContentView
{
    public ObservableCollection<Staff> StaffList { get; set; } = new();

    public StaffManagementView()
    {
        InitializeComponent();
        // Hardcoded example staff
        StaffList.Add(new Staff {
            Name = "Jane Doe",
            Role = "Therapist",
            WorkSchedule = "Mon-Fri 9am-5pm",
            Performance = "Excellent"
        });
        StaffList.Add(new Staff {
            Name = "John Smith",
            Role = "Stylist",
            WorkSchedule = "Tue-Sat 10am-6pm",
            Performance = "Good"
        });
        StaffList.Add(new Staff {
            Name = "Emily Clark",
            Role = "Receptionist",
            WorkSchedule = "Mon-Sat 8am-4pm",
            Performance = "Outstanding"
        });
        BindingContext = this;
    }

    private void OnAddStaffClicked(object sender, EventArgs e)
    {
        StaffList.Add(new Staff {
            Name = "New Staff",
            Role = "Unassigned",
            WorkSchedule = "Not set",
            Performance = "Not evaluated"
        });
    }

    private void OnEditStaffClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Staff staff)
        {
            staff.Name += " (Edited)";
        }
    }

    private void OnDeleteStaffClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Staff staff)
        {
            StaffList.Remove(staff);
        }
    }

    private async void OnAssignRoleClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Staff staff)
        {
            string[] roles = { "Therapist", "Stylist", "Receptionist", "Manager", "Cleaner" };
            string selectedRole = await Application.Current.MainPage.DisplayActionSheet(
                $"Assign Role to {staff.Name}", "Cancel", null, roles);
            if (!string.IsNullOrEmpty(selectedRole) && selectedRole != "Cancel")
            {
                staff.Role = selectedRole;
            }
        }
    }

    private async void OnSetScheduleClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Staff staff)
        {
            string newSchedule = await Application.Current.MainPage.DisplayPromptAsync(
                $"Set Schedule for {staff.Name}", "Enter new work schedule:");
            if (!string.IsNullOrEmpty(newSchedule))
            {
                staff.WorkSchedule = newSchedule;
            }
        }
    }

    private async void OnViewPerformanceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Staff staff)
        {
            await Application.Current.MainPage.DisplayAlert(
                $"Performance for {staff.Name}",
                staff.Performance,
                "OK");
        }
    }
}

public class Staff
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string WorkSchedule { get; set; }
    public string Performance { get; set; }
}
