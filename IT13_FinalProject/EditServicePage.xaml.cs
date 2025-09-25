using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class EditServicePage : ContentPage
{
    private Service _service;
    public EditServicePage(Service service)
    {
        InitializeComponent();
        _service = service;
        NameEntry.Text = service.Name;
        DescriptionEntry.Text = service.Description;
        PriceEntry.Text = service.Price.ToString("F2");
        DurationEntry.Text = service.Duration;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEntry.Text) && decimal.TryParse(PriceEntry.Text, out var price))
        {
            _service.Name = NameEntry.Text;
            _service.Description = DescriptionEntry.Text;
            _service.Price = price;
            _service.Duration = DurationEntry.Text;
            await DisplayAlert("Success", "Service updated.", "OK");
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
