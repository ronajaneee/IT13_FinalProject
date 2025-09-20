  using Microcharts;
using SkiaSharp;
using Microsoft.Maui.Graphics;

namespace IT13_FinalProject;

public partial class DashboardView : ContentView
{
    public DashboardView()
    {
        InitializeComponent();
        InitializeCharts();
    }

    private void InitializeCharts()
    {
        try
        {
            // Bookings Chart Data (Bar Chart)
            var bookingsEntries = new[]
            {
                new ChartEntry(25) { Label = "Mon", ValueLabel = "25", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(30) { Label = "Tue", ValueLabel = "30", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(28) { Label = "Wed", ValueLabel = "28", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(35) { Label = "Thu", ValueLabel = "35", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(40) { Label = "Fri", ValueLabel = "40", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(32) { Label = "Sat", ValueLabel = "32", Color = SKColor.Parse("#FF4B91") },
                new ChartEntry(20) { Label = "Sun", ValueLabel = "20", Color = SKColor.Parse("#FF4B91") }
            };

            // Revenue Chart Data (Line Chart)
            var revenueEntries = new[]
            {
                new ChartEntry(2100) { Label = "W1", ValueLabel = "2.1k", Color = SKColor.Parse("#4B9BFF") },
                new ChartEntry(2400) { Label = "W2", ValueLabel = "2.4k", Color = SKColor.Parse("#4B9BFF") },
                new ChartEntry(2200) { Label = "W3", ValueLabel = "2.2k", Color = SKColor.Parse("#4B9BFF") },
                new ChartEntry(2500) { Label = "W4", ValueLabel = "2.5k", Color = SKColor.Parse("#4B9BFF") }
            };

            // Customer Distribution Pie Chart
            var customerEntries = new[]
            {
                new ChartEntry(40) { Label = "New", Color = SKColor.Parse("#90EE90") },
                new ChartEntry(310) { Label = "Returning", Color = SKColor.Parse("#4B9BFF") }
            };

            // Initialize Charts
            if (BookingsChart != null)
            {
                BookingsChart.Chart = new BarChart { 
                    Entries = bookingsEntries,
                    LabelTextSize = 32,
                    BackgroundColor = SKColor.Parse("#2D3748"),
                    LabelColor = SKColor.Parse("#FFFFFF")
                };
            }

            if (RevenueChart != null)
            {
                RevenueChart.Chart = new LineChart { 
                    Entries = revenueEntries,
                    LineMode = LineMode.Spline,
                    LineSize = 8,
                    PointMode = PointMode.Circle,
                    PointSize = 18,
                    BackgroundColor = SKColor.Parse("#2D3748"),
                    LabelColor = SKColor.Parse("#FFFFFF")
                };
            }

            if (CustomerPieChart != null)
            {
                CustomerPieChart.Chart = new DonutChart { 
                    Entries = customerEntries,
                    BackgroundColor = SKColor.Parse("#1e2445"),
                    LabelTextSize = 0
                };
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Charts init error: {ex}");
            if (BookingsChart != null) BookingsChart.IsVisible = false;
            if (RevenueChart != null) RevenueChart.IsVisible = false;
            if (CustomerPieChart != null) CustomerPieChart.IsVisible = false;
        }
    }
}
