using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace GymApplication.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly GymDbContext context;
        public ContactRepository(GymDbContext context)
        {
            this.context = context;
        }

        public async Task<Contact> AddContacAsync(Contact request)
        {
            request.CreatedAt = DateTime.UtcNow;
            var contact = await context.Contacts.AddAsync(request);
            await context.SaveChangesAsync();
            return contact.Entity;
        }

      

        public async Task<Contact> DeleteContacAsync(int contactId)
        {
            var contact = await GetContacAsync(contactId);

            if (contact != null)
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();

                return contact;
            }

            return null;
        }

        public async Task<bool> Exist(int contactId)
        {
            return await context.Contacts.AnyAsync(s => s.IdContact == contactId);
        }

        public async Task<Contact> GetContacAsync(int contactId)
        {
            return await context.Contacts
                .FirstOrDefaultAsync(u => u.IdContact == contactId);
        }

        public async Task<ICollection> GetContactAsync()
        {
            var contacts = await context.Contacts.ToListAsync();
            var contactsToSend = contacts.Select(b => new
            {
                b.IdContact,
                b.Nom,
                b.Email,
                b.Description,               
            }).ToList();
            return contactsToSend;
        }

        public Task<Contact> UpdateContacAsync(int contactId, Contact request)
        {
            throw new NotImplementedException();
        }
    }
}
