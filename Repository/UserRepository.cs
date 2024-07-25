using GymApplication.Helpers;
using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GymApplication.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly GymDbContext context;
        private readonly PasswordHacher PasswordHacher;
        public UserRepository(GymDbContext context , PasswordHacher PasswordHacher)
        {
            this.context = context;
            this.PasswordHacher = PasswordHacher;
        }
        public Task<Utilisateur> AddUserAsync(Utilisateur request)
        {
            throw new NotImplementedException();
        }

        public async Task<Utilisateur> Authenticate(string email)
        {
            return await context.Utilisateurs.FirstOrDefaultAsync(x => x.Email == email );
        }

        public async Task<Utilisateur> DeleteUserAsync(int userId)
        {
            var user = await GetUserAsync(userId);
            if (user != null)
            {
                context.Utilisateurs.Remove(user);
                await context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<bool> ExistUser(int userId)
        {
            return await context.Utilisateurs.AnyAsync(u => u.IdUtilisateur == userId);
        }

        public async Task<Utilisateur> GetUserAsync(int userId)
        {
            return await context.Utilisateurs
               .FirstOrDefaultAsync(u => u.IdUtilisateur == userId);
        }

        public async Task<List<Utilisateur>> GetUsersAsync()
        {
            return await context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur> RegisterUser(Utilisateur request)
        {
         
                request.MotDePasse = PasswordHacher.HashPassword(request.MotDePasse);
                request.Token = "";  
                request.CreatedAt = DateTime.Now; 
                request.Nom = request.Nom; 
                request.Prenom = request.Prenom; 
                request.DateNaissance = request.DateNaissance; 
                request.Adresse = request.Adresse; 
                request.Telephone = request.Telephone;

            // Add and save the user
            var user = await context.Utilisateurs.AddAsync(request);
                await context.SaveChangesAsync();
                return user.Entity;
            
        }

        public async Task<Utilisateur> UpdateUserAsync(int userId, Utilisateur request)
        {
            var existingUser = await GetUserAsync(userId);
            if (existingUser != null)
            {
                existingUser.Nom = request.Nom;
                existingUser.Prenom = request.Prenom;
                existingUser.DateNaissance = request.DateNaissance;
                existingUser.Email = request.Email;
                existingUser.MotDePasse = existingUser.MotDePasse;
                existingUser.Adresse = existingUser.Adresse;
                existingUser.Telephone = request.Telephone;
                existingUser.Token = existingUser.Token;
                existingUser.UpdatedAt = DateTime.UtcNow;


                await context.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }
    }
}
