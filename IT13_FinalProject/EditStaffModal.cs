using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace IT13_FinalProject;

public class EditStaffModal : Popup
{
    public EditStaffModal(Staff staff, Action<Staff> onSave)
    {
        CanBeDismissedByTappingOutsideOfPopup = true;
        var firstName = new Entry { Placeholder = "First Name", Text = staff.FirstName, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var lastName = new Entry { Placeholder = "Last Name", Text = staff.LastName, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var position = new Entry { Placeholder = "Position", Text = staff.Position, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var email = new Entry { Placeholder = "Email", Text = staff.Email, Keyboard = Keyboard.Email, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var phone = new Entry { Placeholder = "Phone", Text = staff.Phone, Keyboard = Keyboard.Telephone, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
        var status = new Entry { Placeholder = "Status", Text = staff.Status, Margin = new Thickness(0, 5), TextColor = Colors.White, PlaceholderColor = Colors.Gray };
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
            staff.FirstName = firstName.Text;
            staff.LastName = lastName.Text;
            staff.Position = position.Text;
            staff.Email = email.Text;
            staff.Phone = phone.Text;
            staff.Status = status.Text;
            onSave?.Invoke(staff);
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
                            Text = "Edit Staff",
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#3b82f6"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        firstName, lastName, position, email, phone, status,
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
