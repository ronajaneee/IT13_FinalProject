using System.ComponentModel;

namespace IT13_FinalProject;

public partial class AdminPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool isSidebarExpanded = false;
    private bool sidebarLabelVisible = false;
    public bool SidebarLabelVisible
    {
        get => sidebarLabelVisible;
        set
        {
            if (sidebarLabelVisible != value)
            {
                sidebarLabelVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SidebarLabelVisible)));
            }
        }
    }

    public AdminPage()
    {
        InitializeComponent();
        BindingContext = this;
        Sidebar.WidthRequest = 40;
        SidebarLabelVisible = false;
    }

    private void OnSidebarToggleClicked(object sender, EventArgs e)
    {
        isSidebarExpanded = !isSidebarExpanded;
        Sidebar.WidthRequest = isSidebarExpanded ? 220 : 40;
        SidebarLabelVisible = isSidebarExpanded;
    }

    private void OnDashboardClicked(object sender, EventArgs e)
    {
        // TODO: Navigate to Dashboard or show dashboard content
        DisplayAlert("Navigation", "Dashboard clicked.", "OK");
    }

    private void OnCustomersClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Customers clicked.", "OK");
    }

    private void OnStaffClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Staff clicked.", "OK");
    }

    private void OnServicesClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Services clicked.", "OK");
    }

    private void OnAppointmentsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Appointments clicked.", "OK");
    }

    private void OnPaymentsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Payments clicked.", "OK");
    }

    private void OnReportsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Reports clicked.", "OK");
    }

    private void OnSettingsClicked(object sender, EventArgs e)
    {
        DisplayAlert("Navigation", "Settings clicked.", "OK");
    }
}