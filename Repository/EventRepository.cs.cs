using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace GymApplication.Repository
{
    public class EventRepository : IEventRepository
    {

        private readonly GymDbContext context;
        public EventRepository(GymDbContext context)
        {
            this.context = context;
        }
        public async Task<Evenement> AddEventAsync(Evenement request)
        {
            var evenements = await context.Evenements.AddAsync(request);
            await context.SaveChangesAsync();
            return evenements.Entity;
        }

        public async Task<Evenement> DeleteEventAsync(int eventId)
        {
            var evenement = await GetEventAsync(eventId);

            if (evenement != null)
            {
                context.Evenements.Remove(evenement);
                await context.SaveChangesAsync();

                return evenement;
            }

            return null;
        }

        public async Task<bool> Exist(int eventId)
        {
            return await context.Evenements.AnyAsync(s => s.IdEvenement == eventId);
        }

        public async Task<ICollection> GetEventAsync()
        {
            var evenements = await context.Evenements.ToListAsync();
            var evenementsToSend = evenements.Select(b => new
            {
                b.IdEvenement,
                b.Nom,
                b.Description,
                b.Date,
                b.HeureDebut,
                b.HeureFin,
            }).ToList();
            return evenementsToSend;
        }

        public async Task<Evenement> GetEventAsync(int eventId)
        {
            return await context.Evenements
                  .FirstOrDefaultAsync(u => u.IdEvenement == eventId);
        }

        public async Task<Evenement> UpdateEventAsync(int eventId, Evenement request)
        {
            var existingEvent = await GetEventAsync(eventId);

            if (existingEvent != null)
            {
                existingEvent.Nom = request.Nom;
                existingEvent.Description = request.Description;
                existingEvent.Date = request.Date;
                existingEvent.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return existingEvent;

            }

            return null;
        }
    }
}
