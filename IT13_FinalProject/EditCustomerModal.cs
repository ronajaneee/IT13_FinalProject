using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace IT13_FinalProject;

public class EditCustomerModal : Popup
{
    public EditCustomerModal(Customer customer, Action<Customer> onSave)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var firstName = new Entry { Placeholder = "First Name", Text = customer.FirstName, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var lastName = new Entry { Placeholder = "Last Name", Text = customer.LastName, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var email = new Entry { Placeholder = "Email", Text = customer.Email, Keyboard = Keyboard.Email, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var phone = new Entry { Placeholder = "Phone", Text = customer.Phone, Keyboard = Keyboard.Telephone, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var address = new Entry { Placeholder = "Address", Text = customer.Address, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
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
            customer.FirstName = firstName.Text;
            customer.LastName = lastName.Text;
            customer.Email = email.Text;
            customer.Phone = phone.Text;
            customer.Address = address.Text;
            onSave?.Invoke(customer);
            Close();
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
                            Text = "Edit Customer",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        firstName, lastName, email, phone, address,
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
