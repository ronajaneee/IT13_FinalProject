using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public class AddEditCustomerPopupV2 : Popup
{
    public AddEditCustomerPopupV2(Customer? customer, bool isEdit, Action<Customer> onSave)
    {
        // Enable closing when tapping outside
        CanBeDismissedByTappingOutsideOfPopup = true;

        // Input fields (dark theme styling)
        var firstName = new Entry { Placeholder = "First Name", Text = customer?.FirstName ?? "", Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var lastName = new Entry { Placeholder = "Last Name", Text = customer?.LastName ?? "", Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var email = new Entry { Placeholder = "Email", Text = customer?.Email ?? "", Keyboard = Keyboard.Email, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var phone = new Entry { Placeholder = "Phone", Text = customer?.Phone ?? "", Keyboard = Keyboard.Telephone, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var address = new Entry { Placeholder = "Address", Text = customer?.Address ?? "", Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };

        var loyalty = new Label
        {
            Text = $"Loyalty Points: {customer?.LoyaltyPoints ?? 0}",
            TextColor = Color.FromArgb("#10b981"), // teal accent
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(0, 10, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        };

        // Buttons
        var saveBtn = new Button
        {
            Text = "Save",
            BackgroundColor = Color.FromArgb("#3b82f6"), // blue accent
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120,
            FontAttributes = FontAttributes.Bold
        };

        var cancelBtn = new Button
        {
            Text = "Cancel",
            BackgroundColor = Color.FromArgb("#ef4444"), // modern red
            TextColor = Colors.White,
            CornerRadius = 8,
            WidthRequest = 120
        };

        // Button logic
        saveBtn.Clicked += (s, e) =>
        {
            var cust = customer ?? new Customer();
            cust.FirstName = firstName.Text;
            cust.LastName = lastName.Text;
            cust.Email = email.Text;
            cust.Phone = phone.Text;
            cust.Address = address.Text;
            if (!isEdit) cust.LoyaltyPoints = 0;

            onSave?.Invoke(cust);
            Close();
        };

        cancelBtn.Clicked += (s, e) => Close();

        // Wrap content in ScrollView for scrolling
        var scrollableContent = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 14,
                Children =
                {
                    new Label
                    {
                        Text = isEdit ? "Edit Customer" : "Add New Customer",
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromArgb("#3b82f6"), // blue accent
                        HorizontalOptions = LayoutOptions.Center
                    },
                    firstName, lastName, email, phone, address, loyalty,
                    new HorizontalStackLayout
                    {
                        Spacing = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = { saveBtn, cancelBtn }
                    }
                }
            }
        };

        // Main popup card
        Content = new Frame
        {
            Padding = 24,
            CornerRadius = 16,
            BackgroundColor = Color.FromArgb("#1f2937"), // deep slate background
            BorderColor = Color.FromArgb("#3b82f6"),     // blue border
            HasShadow = true,
            Content = scrollableContent
        };
    }
}