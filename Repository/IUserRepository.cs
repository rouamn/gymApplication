using GymApplication.Repository.Models;

namespace GymApplication.Repository
{
    public interface IUserRepository
    {
        Task<List<Utilisateur>> GetUsersAsync();
        Task<Utilisateur> Authenticate(string username);
        Task<Utilisateur> RegisterUser(Utilisateur request);

        Task<Utilisateur> GetUserAsync(int userId);
        Task<Utilisateur> UpdateUserAsync(int userId, Utilisateur request);
        Task<Utilisateur> DeleteUserAsync(int userId);
        Task<Utilisateur> AddUserAsync(Utilisateur request);
        Task<bool> ExistUser(int userId);

        Task<bool> CheckUserNameExistAsync(string username);
        Task<bool> CheckEmailExistAsync(string email);
        string CheckPasswordStrengthAsync(string password);
        Task<IList<Utilisateur>> GetUsers();
    }
}
