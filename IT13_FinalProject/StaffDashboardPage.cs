using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class StaffDashboardPage : ContentPage
{
    public StaffDashboardPage()
    {
        Title = "Staff Dashboard";
        Content = new StaffManagementView();
    }
}
