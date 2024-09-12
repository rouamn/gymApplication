using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;
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
        Task<int> CountPaiementAsync();

        Task<string> UpdateVisibilityAsync(int id, bool newVisibility);
    }
}
