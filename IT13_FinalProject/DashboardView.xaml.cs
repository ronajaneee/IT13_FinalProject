using Microcharts;
using SkiaSharp;
using Microcharts.Maui;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class DashboardView : ContentView
{
    public Chart BookingsBarChart { get; set; }
    public Chart RevenueLineChart { get; set; }
    public Chart CustomerPieChart { get; set; }

    public double SalesProgress { get; set; } = 2500.0 / 3500.0; // $2,500 of $3,500 target

    public ObservableCollection<StaffOnDuty> StaffList { get; set; }
    public ObservableCollection<AppointmentItem> Appointments { get; set; }

    public DashboardView()
    {
        InitializeComponent();

        BookingsBarChart = new BarChart
        {
            Entries = new[]
            {
                new ChartEntry(12) { Label = "Mon", ValueLabel = "12", Color = SKColor.Parse("#3b82f6") },
                new ChartEntry(18) { Label = "Tue", ValueLabel = "18", Color = SKColor.Parse("#10b981") },
                new ChartEntry(15) { Label = "Wed", ValueLabel = "15", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(20) { Label = "Thu", ValueLabel = "20", Color = SKColor.Parse("#ef4444") },
                new ChartEntry(25) { Label = "Fri", ValueLabel = "25", Color = SKColor.Parse("#4B91FF") },
                new ChartEntry(30) { Label = "Sat", ValueLabel = "30", Color = SKColor.Parse("#243056") },
                new ChartEntry(10) { Label = "Sun", ValueLabel = "10", Color = SKColor.Parse("#1e2445") },
            }
        };

        RevenueLineChart = new LineChart
        {
            Entries = new[]
            {
                new ChartEntry(800) { Label = "Week 1", ValueLabel = "$800", Color = SKColor.Parse("#3b82f6") },
                new ChartEntry(1200) { Label = "Week 2", ValueLabel = "$1200", Color = SKColor.Parse("#10b981") },
                new ChartEntry(900) { Label = "Week 3", ValueLabel = "$900", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(1300) { Label = "Week 4", ValueLabel = "$1300", Color = SKColor.Parse("#ef4444") },
            }
        };

        CustomerPieChart = new PieChart
        {
            Entries = new[]
            {
                new ChartEntry(40) { Label = "New", ValueLabel = "40", Color = SKColor.Parse("#10b981") },
                new ChartEntry(310) { Label = "Returning", ValueLabel = "310", Color = SKColor.Parse("#4B91FF") },
            }
        };

        StaffList = new ObservableCollection<StaffOnDuty>
        {
            new StaffOnDuty { Name = "Anna", Role = "Massage (2 sessions)", Image = "anna_profile.png" },
            new StaffOnDuty { Name = "John", Role = "Facial (1 session)", Image = "john_profile.png" },
            new StaffOnDuty { Name = "Mia", Role = "Reception Desk", Image = "mia_profile.png" }
        };

        Appointments = new ObservableCollection<AppointmentItem>
        {
            new AppointmentItem { Customer = "Alice Smith", Service = "Massage", Time = "10:00 AM", Status = "Confirmed" },
            new AppointmentItem { Customer = "Bob Lee", Service = "Facial", Time = "11:30 AM", Status = "Pending" },
            new AppointmentItem { Customer = "Cathy Brown", Service = "Manicure", Time = "1:00 PM", Status = "Confirmed" },
            new AppointmentItem { Customer = "David Kim", Service = "Pedicure", Time = "2:30 PM", Status = "Pending" }
        };

        BindingContext = this;
    }
}

public class StaffOnDuty
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Image { get; set; }
}

public class AppointmentItem
{
    public string Customer { get; set; }
    public string Service { get; set; }
    public string Time { get; set; }
    public string Status { get; set; }
}
