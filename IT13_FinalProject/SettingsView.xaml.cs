using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class SettingsView : ContentView
{
    public string AdminName { get; set; } = "Sarah Johnson";
    public string AdminEmail { get; set; } = "admin@luxespa.com";
    public string AdminContact { get; set; } = "555-1234";
    public string AdminPassword { get; set; } = "password123";

    public ObservableCollection<Account> Accounts { get; set; } = new();

    public SettingsView()
    {
        InitializeComponent();
        // Hardcoded accounts
        Accounts.Add(new Account { Name = "Sarah Johnson", Email = "admin@luxespa.com", Role = "Admin" });
        Accounts.Add(new Account { Name = "Jane Doe", Email = "jane@luxespa.com", Role = "Staff" });
        Accounts.Add(new Account { Name = "John Smith", Email = "john@luxespa.com", Role = "Staff" });
        BindingContext = this;
    }

    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Profile Updated", $"Name: {AdminName}\nEmail: {AdminEmail}\nContact: {AdminContact}", "OK");
    }

    private async void OnAddAccountClicked(object sender, EventArgs e)
    {
        string name = await Application.Current.MainPage.DisplayPromptAsync("Add Account", "Enter name:");
        string email = await Application.Current.MainPage.DisplayPromptAsync("Add Account", "Enter email:");
        string role = await Application.Current.MainPage.DisplayActionSheet("Select Role", "Cancel", null, "Admin", "Staff");
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && role != "Cancel")
        {
            Accounts.Add(new Account { Name = name, Email = email, Role = role });
        }
    }

    private async void OnSetRoleClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Account acc)
        {
            string role = await Application.Current.MainPage.DisplayActionSheet($"Set Role for {acc.Name}", "Cancel", null, "Admin", "Staff");
            if (role != "Cancel")
            {
                acc.Role = role;
            }
        }
    }

    private void OnRemoveAccountClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Account acc)
        {
            Accounts.Remove(acc);
        }
    }
}

public class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
