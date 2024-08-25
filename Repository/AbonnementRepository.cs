using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GymApplication.Repository
{
    public class AbonnementRepository : IAbonnementRepository

    {
        private readonly GymDbContext context;
        public AbonnementRepository(GymDbContext context)
        {
            this.context = context;
        }
        public async Task<Abonnement> AddAbonementAsync(Abonnement request)
        {
            var abonnement = await context.Abonnements.AddAsync(request);   
            await context.SaveChangesAsync();
            return abonnement.Entity;
        }

        public async Task<Abonnement> DeleteAbonementAsync(int abonnementId)
        {
            var abonnement = await GetAbonementAsync(abonnementId);

            if (abonnement != null)
            {
                context.Abonnements.Remove(abonnement);
                await context.SaveChangesAsync();

                return abonnement;
            }

            return null;
        }

        public async Task<bool> Exist(int abonnementId)
        {
            return await context.Abonnements.AnyAsync(s => s.IdAbonnement == abonnementId);
        }

        public async Task<ICollection> GetAbonementAsync()
        {
            var abonnements = await context.Abonnements.ToListAsync();
            var abonnementsToSend = abonnements.Select(b => new
            {
                b.IdAbonnement,
                b.TypeAbonnement,
                b.Statut,
                b.Date,
                b.Prix,
            }).ToList();
            return abonnementsToSend;
        }

        public async Task<Abonnement> GetAbonementAsync(int abonnementId)
        {
            return await context.Abonnements
                 .FirstOrDefaultAsync(u => u.IdAbonnement == abonnementId);
        }

        public async Task<Abonnement> UpdateAbonementAsync(int abonnementId, Abonnement request)
        {

            var existingAbonnement = await GetAbonementAsync(abonnementId);

            if (existingAbonnement != null)
            {
                existingAbonnement.TypeAbonnement = request.TypeAbonnement;
                existingAbonnement.Prix = request.Prix;

                existingAbonnement.Date = request.Date;
                existingAbonnement.Statut = request.Statut;
                existingAbonnement.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return existingAbonnement;

            }

            return null;
        }

     
    }
}
