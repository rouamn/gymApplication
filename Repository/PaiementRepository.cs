using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace GymApplication.Repository
{
    public class PaiementRepository : IPaiementRepository
    {
        private readonly GymDbContext context;
        public PaiementRepository(GymDbContext context)
        {
            this.context = context;
        }
        public async Task<Paiement> AddPaiementAsync(Paiement request)
        {   request.CreatedAt= DateTime.Now;
            var paiements = await context.Paiements.AddAsync(request);
            await context.SaveChangesAsync();
            return paiements.Entity;
        }

        public async Task<int> CountPaiementAsync()
        {
            var paiements = await context.Paiements.ToListAsync();
            var count = paiements.Count;
            return count;
        }

       

        public async Task<bool> Exist(int paiementId)
        {
            return await context.Paiements.AnyAsync(s => s.IdPaiement == paiementId);
        }

        public async Task<ICollection> GetPaiementAsync()
        {
            var paiements = await context.Paiements
                .Where(p => p.Visibility) // Filter by Visibility = true
                .ToListAsync();

            var paiementsToSend = paiements.Select(b => new
            {
                b.IdPaiement,
                b.Email,
                b.OperationId,
                b.FullName,
                b.Cin,
                b.TypeAbonnement,
                b.DureeAbonnement,
                b.Prixabonnement,
                b.CreatedAt,
            }).ToList();

            return paiementsToSend;
        }

        public async Task<Paiement> GetPaiementAsync(int paiementId)
        {
            return await context.Paiements
                  .FirstOrDefaultAsync(u => u.IdPaiement == paiementId);
        }

        public async Task<Paiement> GetPaiementByOperationId(string Id)
        {
             return await context.Paiements
                  .FirstOrDefaultAsync(u => u.OperationId == Id);
        }

        public async Task<string> UpdateVisibilityAsync(int id,  bool newVisibility = false)
        {
            newVisibility = false;
            var paiement = await context.Paiements.FindAsync(id);
            paiement.Visibility = newVisibility;
            await context.SaveChangesAsync();
            return "Visibility updated successfully.";
        }

       
    }
}
