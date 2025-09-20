using Microcharts;
using SkiaSharp;

namespace IT13_FinalProject;

public partial class ReportAnalyticsView : ContentView
{
    public ReportAnalyticsView()
    {
        InitializeComponent();
        TryInitChart();
    }

    private void TryInitChart()
    {
        try
        {
            // Sample sales data
            var entries = new[]
            {
                new ChartEntry(1200) { Label = "Mon", ValueLabel = "1.2k", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(1500) { Label = "Tue", ValueLabel = "1.5k", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(900)  { Label = "Wed", ValueLabel = "0.9k", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(1800) { Label = "Thu", ValueLabel = "1.8k", Color = SKColor.Parse("#FFB84B") },
                new ChartEntry(2100) { Label = "Fri", ValueLabel = "2.1k", Color = SKColor.Parse("#FFB84B") },
            };

            if (SalesChartView != null)
            {
                SalesChartView.Chart = new BarChart
                {
                    Entries = entries,
                    LabelTextSize = 28,
                    BackgroundColor = SKColor.Parse("#2D3748"),
                    LabelColor = SKColor.Parse("#FFFFFF")
                };
            }

            if (SalesChartPlaceholder != null)
                SalesChartPlaceholder.IsVisible = false;
        }
        catch (Exception)
        {
            // Fallback placeholder if chart fails for any reason
            if (SalesChartView != null)
                SalesChartView.IsVisible = false;
            if (SalesChartPlaceholder != null)
                SalesChartPlaceholder.IsVisible = true;
        }
    }
}
