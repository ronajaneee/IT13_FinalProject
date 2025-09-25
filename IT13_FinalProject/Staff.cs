namespace IT13_FinalProject
{
    public class Staff
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; } // Changed from Role to Position
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public System.Collections.Generic.List<string> RecentActivities { get; set; }
    }
}