using System.ComponentModel;

namespace IT13_FinalProject;

public partial class AdminPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool isSidebarExpanded = false;
    private bool sidebarLabelVisible = false;
    private double sidebarWidth = 40;

    // Track the currently active button
    private Button _activeButton;

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

    public double SidebarWidth
    {
        get => sidebarWidth;
        set
        {
            if (sidebarWidth != value)
            {
                sidebarWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SidebarWidth)));
                if (Sidebar != null)
                    Sidebar.WidthRequest = sidebarWidth;
            }
        }
    }

    public AdminPage()
    {
        InitializeComponent();
        BindingContext = this;
        SidebarWidth = 40;
        SidebarLabelVisible = false;
        if (Sidebar != null)
            Sidebar.WidthRequest = SidebarWidth;

        // Show dashboard by default
        MainContent.Content = new DashboardView();

        // Set Dashboard as active on load
        SetActiveButton(BtnDashboard);
    }

    private void OnSidebarToggleClicked(object sender, EventArgs e)
    {
        isSidebarExpanded = !isSidebarExpanded;
        SidebarWidth = isSidebarExpanded ? 220 : 40;
        SidebarLabelVisible = isSidebarExpanded;
        if (Sidebar != null)
            Sidebar.WidthRequest = SidebarWidth;
    }

    // Unified click handler for all nav buttons
    private void OnNavButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button clickedButton)
        {
            // Change main content based on button text
            switch (clickedButton.Text)
            {
                case "Dashboard":
                    MainContent.Content = new DashboardView();
                    break;
                case "Customers":
                    MainContent.Content = new CustomerManagementView();
                    break;
                case "Staff":
                    MainContent.Content = new StaffManagementView();
                    break;
                case "Services":
                    MainContent.Content = new ServiceManagementView();
                    break;
                case "Appointments":
                    MainContent.Content = new AppointmentManagementView();
                    break;
                case "Payments":
                    MainContent.Content = new PaymentManagementView();
                    break;
                case "Reports":
                    MainContent.Content = new ReportAnalyticsView();
                    break;
                case "Settings":
                    MainContent.Content = new SettingsView();
                    break;
            }

            // Highlight the active button
            SetActiveButton(clickedButton);
        }
    }

    private void SetActiveButton(Button newActiveButton)
    {
        // Reset previous active button
        if (_activeButton != null)
        {
            _activeButton.BackgroundColor = Colors.Transparent;
            _activeButton.TextColor = Color.FromArgb("#ccc");
        }

        // Set new active button
        newActiveButton.BackgroundColor = Color.FromArgb("#3b82f6"); // Blue active
        newActiveButton.TextColor = Colors.White;

        _activeButton = newActiveButton;
    }
}