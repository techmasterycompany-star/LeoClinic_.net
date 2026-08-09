using LeoClinic.Domain.Entities;

namespace LeoClinic.Application.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<Availability?> GetSlotByIdAsync(int id);
    }
}
