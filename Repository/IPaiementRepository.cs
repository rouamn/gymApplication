using GymApplication.Repository.Models;
using System.Collections;
namespace GymApplication.Repository
{
    public interface IPaiementRepository
    {
        Task<ICollection> GetPaiementAsync();
        Task<Paiement> GetPaiementAsync(int paiementId);
        Task<Paiement> GetPaiementByOperationId(string Id);
        Task<Paiement> AddPaiementAsync(Paiement request);
        Task<bool> Exist(int paiementId);
    }
}
