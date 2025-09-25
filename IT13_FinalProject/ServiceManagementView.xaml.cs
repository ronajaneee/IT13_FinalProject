using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace IT13_FinalProject;

public partial class ServiceManagementView : ContentView, INotifyPropertyChanged
{
    public ObservableCollection<Service> Services { get; set; } = new();
    public ObservableCollection<Service> FilteredServices { get; set; } = new();
    public event PropertyChangedEventHandler PropertyChanged;

    public ServiceManagementView()
    {
        InitializeComponent();

        // Hardcoded example services
        Services.Add(new Service { Name = "Massage", Price = 50.00m, Duration = "60 mins", Description = "Relaxing full-body massage" });
        Services.Add(new Service { Name = "Facial", Price = 35.00m, Duration = "45 mins", Description = "Rejuvenating facial treatment" });
        Services.Add(new Service { Name = "Hair Treatment", Price = 40.00m, Duration = "50 mins", Description = "Deep conditioning hair treatment" });

        FilteredServices = new ObservableCollection<Service>(Services);
        BindingContext = this;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";
        var filtered = Services.Where(s => s.Name.ToLower().Contains(searchText)).ToList();
        FilteredServices.Clear();
        foreach (var service in filtered)
            FilteredServices.Add(service);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredServices)));
    }

    private async void OnAddServiceClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AddServicePage(Services, FilteredServices));
    }

    private async void OnEditServiceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditServicePage(service));
        }
    }

    private async void OnViewServiceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ServiceDetailsPage(service));
        }
    }

    private async void OnDeleteServiceClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Service service)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete {service.Name}?",
                "Yes", "No");
            if (confirm)
            {
                Services.Remove(service);
                FilteredServices.Remove(service);
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
