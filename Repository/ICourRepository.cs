using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface ICourRepository
    {
        Task<ICollection> GetCourAsync();
        Task<Cour> GetCourAsync(int courId);
        Task<Cour> UpdateCourAsync(int courId, Cour request);
        Task<Cour> DeleteCourAsync(int courId);
        Task<Cour> AddCourAsync(Cour request);
        Task<bool> Exist(int courId);
    }
}
