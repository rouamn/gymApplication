using GymApplication.Repository.Models;
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
        public Task<Evenement> AddEventAsync(Evenement request)
        {
            throw new NotImplementedException();
        }

        public Task<Evenement> DeleteEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection> GetEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Evenement> GetEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<Evenement> UpdateEventAsync(int eventId, Abonnement request)
        {
            throw new NotImplementedException();
        }
    }
}
