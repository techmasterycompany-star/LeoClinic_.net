namespace LeoClinic.Application.DTOs.Admin
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }

        public int TotalDoctors { get; set; }

        public int TotalPatients { get; set; }

        public int PendingDoctors { get; set; }

        public int TotalAppointments { get; set; }

        public int CompletedAppointments { get; set; }

        public int CancelledAppointments { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
