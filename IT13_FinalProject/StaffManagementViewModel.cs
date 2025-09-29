using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using System;

namespace IT13_FinalProject
{
    public class StaffManagementViewModel
    {
        public ObservableCollection<Staff> StaffList { get; set; }
        public ICommand AddStaffCommand { get; }
        public ICommand EditStaffCommand { get; }
        public ICommand DeleteStaffCommand { get; }
        public ICommand ViewStaffCommand { get; }

        public StaffManagementViewModel()
        {
            StaffList = new ObservableCollection<Staff>
            {
                new Staff { 
                    FirstName = "Anna", 
                    LastName = "Smith", 
                    Position = "Senior Manager", 
                    Email = "anna.smith@spamanagement.com", 
                    Phone = "123-456-7890", 
                    Status = "Active", 
                    RecentActivities = new System.Collections.Generic.List<string>{
                        "Completed quarterly staff review",
                        "Updated staff schedules",
                        "Conducted training session"
                    } 
                },
                new Staff { 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Position = "Lead Receptionist", 
                    Email = "john.doe@spamanagement.com", 
                    Phone = "987-654-3210", 
                    Status = "Active", 
                    RecentActivities = new System.Collections.Generic.List<string>{
                        "Processed 15 bookings today",
                        "Updated customer database",
                        "Handled customer feedback"
                    } 
                },
                new Staff { 
                    FirstName = "Maria", 
                    LastName = "Garcia", 
                    Position = "Senior Therapist", 
                    Email = "maria.garcia@spamanagement.com", 
                    Phone = "555-123-4567", 
                    Status = "On Leave", 
                    RecentActivities = new System.Collections.Generic.List<string>{
                        "Completed 8 therapy sessions",
                        "Updated client progress notes",
                        "Scheduled for training next week"
                    } 
                },
                new Staff { 
                    FirstName = "David", 
                    LastName = "Chen", 
                    Position = "Massage Therapist", 
                    Email = "david.chen@spamanagement.com", 
                    Phone = "555-987-6543", 
                    Status = "Active", 
                    RecentActivities = new System.Collections.Generic.List<string>{
                        "Completed 6 massage sessions",
                        "Restocked supplies",
                        "Updated treatment records"
                    } 
                },
                new Staff { 
                    FirstName = "Sarah", 
                    LastName = "Johnson", 
                    Position = "Wellness Coordinator", 
                    Email = "sarah.johnson@spamanagement.com", 
                    Phone = "555-789-0123", 
                    Status = "Active", 
                    RecentActivities = new System.Collections.Generic.List<string>{
                        "Organized wellness workshop",
                        "Updated wellness programs",
                        "Conducted client consultations"
                    } 
                }
            };

            AddStaffCommand = new Command(() => {
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new AddStaffModal(staff =>
                {
                    StaffList.Add(staff);
                });
                page.ShowPopup(modal);
            });

            EditStaffCommand = new Command<Staff>((staff) => {
                if (staff == null) return;
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new EditStaffModal(staff, editedStaff =>
                {
                    var index = StaffList.IndexOf(staff);
                    if (index >= 0)
                    {
                        StaffList[index] = editedStaff;
                    }
                });
                page.ShowPopup(modal);
            });

            DeleteStaffCommand = new Command<Staff>(async (staff) => {
                try
                {
                    if (staff != null && StaffList.Contains(staff))
                    {
                        bool confirm = await Application.Current.MainPage.DisplayAlert(
                            "Confirm Delete",
                            $"Are you sure you want to remove {staff.FirstName} {staff.LastName}?",
                            "Yes", "No");

                        if (confirm)
                        {
                            StaffList.Remove(staff);
                            await Application.Current.MainPage.DisplayAlert("Success", "Staff member removed successfully.", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete staff member: {ex.Message}", "OK");
                }
            });

            ViewStaffCommand = new Command<Staff>((staff) => {
                if (staff == null) return;
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new ViewStaffModal(staff);
                page.ShowPopup(modal);
            });
        }
    }
}