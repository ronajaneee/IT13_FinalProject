using Microcharts;
using SkiaSharp;
using Microsoft.Maui.ApplicationModel;

namespace IT13_FinalProject;

public partial class ReportAnalyticsView : ContentView
{
    public ReportAnalyticsView()
    {
        InitializeComponent();

        // If the native handler is already attached, initialize now.
        if (SalesChartView != null && SalesChartView.Handler != null)
        {
            InitializeChartSafe();
        }

        // Ensure we initialize the chart once the native handler is available.
        if (SalesChartView != null)
        {
            SalesChartView.HandlerChanged += (s, e) =>
            {
                if (SalesChartView.Handler != null)
                {
                    InitializeChartSafe();
                }
            };
        }
    }

    void InitializeChartSafe()
    {
        // Always run UI updates on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                if (SalesChartView == null)
                {
                    System.Diagnostics.Debug.WriteLine("SalesChartView is null, cannot initialize chart.");
                    return;
                }

                // Create and assign chart directly to avoid binding issues
                SalesChartView.Chart = new BarChart
                {
                    Entries = new[]
                    {
                        new ChartEntry(200f) { Label = "Mon", ValueLabel = "$200", Color = SKColor.Parse("#3b82f6") },
                        new ChartEntry(400f) { Label = "Tue", ValueLabel = "$400", Color = SKColor.Parse("#10b981") },
                        new ChartEntry(300f) { Label = "Wed", ValueLabel = "$300", Color = SKColor.Parse("#FFB84B") },
                        new ChartEntry(500f) { Label = "Thu", ValueLabel = "$500", Color = SKColor.Parse("#ef4444") },
                        new ChartEntry(600f) { Label = "Fri", ValueLabel = "$600", Color = SKColor.Parse("#4B91FF") },
                        new ChartEntry(700f) { Label = "Sat", ValueLabel = "$700", Color = SKColor.Parse("#243056") },
                        new ChartEntry(100f) { Label = "Sun", ValueLabel = "$100", Color = SKColor.Parse("#1e2445") },
                    },
                    LabelTextSize = 32f,
                    ValueLabelTextSize = 28f,
                    Margin = 20,
                };

                // Show chart and hide placeholder
                SalesChartView.IsVisible = true;
                if (SalesChartPlaceholder != null)
                    SalesChartPlaceholder.IsVisible = false;
            }
            catch (Exception ex)
            {
                // Prevent crash on platforms where SKIA or Microcharts is not available
                System.Diagnostics.Debug.WriteLine($"Chart init error: {ex}");
                if (SalesChartView != null)
                    SalesChartView.IsVisible = false;
                if (SalesChartPlaceholder != null)
                    SalesChartPlaceholder.IsVisible = true;
            }
        });
    }
}