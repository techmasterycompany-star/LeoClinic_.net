using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class DoctorLocation : BaseEntity
    {
        public int DoctorId { get; set; }
        public int LocationId { get; set; }

        public DoctorProfile DoctorProfile { get; set; } = null!;
        public Location Location { get; set; } = null!;
    }
}
