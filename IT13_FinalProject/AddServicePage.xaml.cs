using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class AddServicePage : ContentPage
{
    private ObservableCollection<Service> _services;
    private ObservableCollection<Service> _filteredServices;
    public AddServicePage(ObservableCollection<Service> services, ObservableCollection<Service> filteredServices)
    {
        InitializeComponent();
        _services = services;
        _filteredServices = filteredServices;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEntry.Text) && decimal.TryParse(PriceEntry.Text, out var price))
        {
            var service = new Service
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                Price = price,
                Duration = DurationEntry.Text
            };
            _services.Add(service);
            _filteredServices.Add(service);
            await DisplayAlert("Success", "Service added.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please enter valid service details.", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
