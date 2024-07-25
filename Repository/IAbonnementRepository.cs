using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface IAbonnementRepository
    {

        Task<ICollection> GetAbonementAsync();
        Task<Abonnement> GetAbonementAsync(int abonnementId);
        Task<Abonnement> UpdateAbonementAsync(int abonnementId, Abonnement request);
        Task<Abonnement> DeleteAbonementAsync(int abonnementId);
        Task<Abonnement> AddAbonementAsync(Abonnement request);
        Task<bool> Exist(int abonnementId);
    }
}
