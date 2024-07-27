using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface IPlanninigRepository
    {
        Task<ICollection> GetEventAsync();
        Task<Planning> GetEventAsync(int planningId);
        Task<Planning> UpdateEventAsync(int planningId, Planning request);
        Task<Planning> DeleteEventAsync(int planningId);
        Task<Planning> AddEventAsync(Planning request);
        Task<bool> Exist(int planningId);
    }
}
