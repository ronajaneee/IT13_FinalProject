using IT13_FinalProject.Models;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public class AddServiceModal : Popup
{
    public AddServiceModal(ObservableCollection<Service> services, ObservableCollection<Service> filteredServices)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;

        var nameEntry = new Entry { Placeholder = "Service Name", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var priceEntry = new Entry { Placeholder = "Price", Keyboard = Keyboard.Numeric, TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var durationEntry = new Entry { Placeholder = "Duration", TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var descriptionEntry = new Entry { Placeholder = "Description", TextColor = Colors.White, PlaceholderColor = Colors.Gray };

        var saveBtn = new Button
        {
            Text = "Save",
            BackgroundColor = Color.FromArgb("#3b82f6"),
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120,
            FontAttributes = FontAttributes.Bold
        };
        var closeBtn = new Button
        {
            Text = "Back / Close",
            BackgroundColor = Color.FromArgb("#ef4444"),
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120
        };
        saveBtn.Clicked += (s, e) =>
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && decimal.TryParse(priceEntry.Text, out var price))
            {
                var service = new Service
                {
                    Name = nameEntry.Text,
                    Description = descriptionEntry.Text,
                    Price = price,
                    Duration = durationEntry.Text
                };
                services.Add(service);
                filteredServices.Add(service);
                Close();
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please enter valid service details.", "OK");
            }
        };
        closeBtn.Clicked += (s, e) => Close();

        Content = new Frame
        {
            Padding = 24,
            CornerRadius = 16,
            BackgroundColor = Color.FromArgb("#1f2937"),
            BorderColor = Color.FromArgb("#3b82f6"),
            HasShadow = true,
            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Spacing = 14,
                    Children =
                    {
                        new Label
                        {
                            Text = "Add New Service",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        nameEntry, priceEntry, durationEntry, descriptionEntry,
                        new HorizontalStackLayout
                        {
                            Spacing = 20,
                            HorizontalOptions = LayoutOptions.Center,
                            Children = { saveBtn, closeBtn }
                        }
                    }
                }
            }
        };
    }
}
