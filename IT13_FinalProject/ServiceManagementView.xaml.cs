using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class ServiceManagementView : ContentView
{
    public ObservableCollection<Service> Services { get; set; } = new();

    public ServiceManagementView()
    {
        InitializeComponent();

        // Hardcoded example services
        Services.Add(new Service { Name = "Massage", Price = 50.00m, Duration = "60 mins", Description = "Relaxing full-body massage" });
        Services.Add(new Service { Name = "Facial", Price = 35.00m, Duration = "45 mins", Description = "Rejuvenating facial treatment" });
        Services.Add(new Service { Name = "Hair Treatment", Price = 40.00m, Duration = "50 mins", Description = "Deep conditioning hair treatment" });

        BindingContext = this;
    }

    private void OnAddServiceClicked(object sender, EventArgs e)
    {
        Services.Add(new Service { Name = "New Service", Price = 0.00m, Duration = "0 mins", Description = "Description" });
    }

    private void OnEditServiceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            service.Name += " (Edited)";
        }
    }

    private void OnDeleteServiceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            Services.Remove(service);
        }
    }

    private async void OnUpdatePriceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync($"Update Price for {service.Name}", "Enter new price:", keyboard: Keyboard.Numeric);
            if (decimal.TryParse(result, out var newPrice))
            {
                service.Price = newPrice;
            }
        }
    }

    private async void OnUpdateDurationClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            string newDuration = await Application.Current.MainPage.DisplayPromptAsync($"Update Duration for {service.Name}", "Enter new duration (e.g., 60 mins):");
            if (!string.IsNullOrEmpty(newDuration))
            {
                service.Duration = newDuration;
            }
        }
    }
}

public class Service
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Duration { get; set; }
    public string Description { get; set; }
}
