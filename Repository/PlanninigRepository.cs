using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace GymApplication.Repository
{
    public class PlanninigRepository : IPlanninigRepository
    {
        private readonly GymDbContext context;
        public PlanninigRepository(GymDbContext context)
        {
            this.context = context;
        }
        public async Task<Planning> AddPlanningAsync(Planning request)
        {
            var planning = await context.Plannings.AddAsync(request);
            await context.SaveChangesAsync();
            return planning.Entity;
        }

        public async Task<Planning> DeletePlanningAsync(int planningId)
        {
            var planning = await GetPlanningAsync(planningId);

            if (planning != null)
            {
                context.Plannings.Remove(planning);
                await context.SaveChangesAsync();

                return planning;
            }

            return null;
        }

        public async Task<bool> Exist(int planningId)
        {
            return await context.Plannings.AnyAsync(s => s.IdPlanning == planningId);
        }

        public async Task<ICollection> GetPlanningAsync()
        {
            var plannings = await context.Plannings.ToListAsync();
            var planningsToSend = plannings.Select(b => new
            {
                b.IdEvenement,
                b.Jour,
                b.HeureDebut,
                b.HeureFin,
            }).ToList();
            return planningsToSend;
        }

        public async Task<Planning> GetPlanningAsync(int planningId)
        {
            return await context.Plannings
                .FirstOrDefaultAsync(u => u.IdPlanning == planningId);
        }

        public async Task<Planning> UpdatePlanningAsync(int planningId, Planning request)
        {
            var existingPlanning = await GetPlanningAsync(planningId);

            if (existingPlanning != null)
            {
                existingPlanning.IdEvenement = request.IdEvenement;
                existingPlanning.Jour = request.Jour;
                existingPlanning.HeureDebut = request.HeureDebut;
                existingPlanning.HeureFin = request.HeureFin;
                existingPlanning.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return existingPlanning;

            }

            return null;
        }
    }
}
 