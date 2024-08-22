using GymApplication.Repository.Models;
using System.Collections;

namespace GymApplication.Repository
{
    public interface IContactRepository
    {
        Task<ICollection> GetContactAsync();
        Task<Contact> GetContacAsync(int contactId);
        Task<Contact> UpdateContacAsync(int contactId, Contact request);
        Task<Contact> DeleteContacAsync(int contactId);
        Task<Contact> AddContacAsync(Contact request);
        Task<bool> Exist(int contactId);
    }
}
