using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface IPlanninigRepository
    {
        Task<ICollection> GetPlanningAsync();
        Task<Planning> GetPlanningAsync(int planningId);
        Task<Planning> UpdatePlanningAsync(int planningId, Planning request);
        Task<Planning> DeletePlanningAsync(int planningId);
        Task<Planning> AddPlanningAsync(Planning request);
        Task<bool> Exist(int planningId);
    }
}
