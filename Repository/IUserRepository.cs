using GymApplication.Repository.Models;
using GymApplication.Repository.Models.Dto;
using System.Security.Claims;

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

        string CreateJwtToken(Utilisateur user);
        string CreateRefreshToken();
        Task<Utilisateur> RefreshToken(TokenApiDto tokenApiDto);
        ClaimsPrincipal GetPrincipaleFromExpireToken(string token);

        Task<Utilisateur> GetUserByEmail(string email);

        Task SendEmail(Utilisateur user);
        Task<Utilisateur> GetUserByEmailToken(string emailToken);

        Task SendResetEmail(Utilisateur user);
    }
}
