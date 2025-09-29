using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class ServiceDetailsPage : ContentPage
{
    public ServiceDetailsPage(Service service)
    {
        InitializeComponent();
        if (service != null)
        {
            NameLabel.Text = service.Name;
            PriceLabel.Text = $"Price: ${service.Price:F2}";
            DurationLabel.Text = $"Duration: {service.Duration}";
            DescriptionLabel.Text = $"Description: {service.Description}";
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
