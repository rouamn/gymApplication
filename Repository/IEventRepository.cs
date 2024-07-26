using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface IEventRepository
    {
        Task<ICollection> GetEventAsync();
        Task<Evenement> GetEventAsync(int eventId);
        Task<Evenement> UpdateEventAsync(int eventId, Evenement request);
        Task<Evenement> DeleteEventAsync(int eventId);
        Task<Evenement> AddEventAsync(Evenement request);
        Task<bool> Exist(int eventId);
    }
}
