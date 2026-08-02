using LeoClinic.Domain.Common;


namespace LeoClinic.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public int Rate { get; set; }
        public string Review { get; set; } = string.Empty;


        public int DoctorId { get; set; }
        public DoctorProfile DoctorProfile { get; set; } = null!;
        public int PatientId { get; set; }
        public PatientProfile PatientProfile { get; set; } = null!;
    }
}
