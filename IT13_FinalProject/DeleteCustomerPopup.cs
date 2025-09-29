using CommunityToolkit.Maui.Views;

namespace IT13_FinalProject;

public class DeleteCustomerPopup : Popup
{
    public DeleteCustomerPopup(Customer customer, Action onDelete)
    {
        Size = new Size(300, 200);
        CanBeDismissedByTappingOutsideOfPopup = true;

        Content = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(20),
            Children =
            {
                new Label
                {
                    Text = "Delete Customer",
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Red
                },
                new Label
                {
                    Text = $"Are you sure you want to delete {customer.FirstName} {customer.LastName}?",
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.Black
                },
                new HorizontalStackLayout
                {
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Button
                        {
                            Text = "Delete",
                            BackgroundColor = Colors.Red,
                            TextColor = Colors.White,
                            Command = new Command(() =>
                            {
                                onDelete?.Invoke();
                                Close();
                            })
                        },
                        new Button
                        {
                            Text = "Cancel",
                            BackgroundColor = Colors.Gray,
                            TextColor = Colors.White,
                            Command = new Command(() => Close())
                        }
                    }
                }
            }
        };
    }
}
