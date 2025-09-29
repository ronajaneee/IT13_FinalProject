using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class StaffManagementView : ContentView
{
    private StaffManagementViewModel _viewModel;

    public StaffManagementView()
    {
        InitializeComponent();
        _viewModel = new StaffManagementViewModel();
        BindingContext = _viewModel;

        // Subscribe to staff add event
        MessagingCenter.Subscribe<AddStaffPage, Staff>(this, "AddStaff", (sender, staff) =>
        {
            _viewModel.StaffList.Add(staff);
        });
    }
}
