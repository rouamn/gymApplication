using GymApplication.Helpers;
using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Claims;
using GymApplication.Repository.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

        public Task<bool> CheckEmailExistAsync(string email)
           => context.Utilisateurs.AnyAsync(x => x.Email == email);

        public string CheckPasswordStrengthAsync(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Password must be at least 8 characters long &&" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") &&
                Regex.IsMatch(password, "[0-9]")))
                sb.Append(" Password must contain at least one uppercase letter, one lowercase letter and one number &&" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[!@#$%^&*()_+=\\[\\]{};':\"\\\\|,.<>\\/?]"))
                sb.Append(" Password must contain at least one special character" + Environment.NewLine);
            return sb.ToString();
        }

        public Task<bool> CheckUserNameExistAsync(string username)
             => context.Utilisateurs.AnyAsync(x => x.Nom == username);

        public string CreateJwtToken(Utilisateur user)
        {
            //List<string> claimsNames = new List<string>();
            //claimsNames = context.Claims.Where(x => x.RoleId == user.RoleId).
            //Select(x => x.claimName).
            //ToList();

            //string jsonList = JsonConvert.SerializeObject(claimsNames);

            //var role = context.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            //var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("veryverysecret.....");

            //var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = identity,
            //    Expires = DateTime.Now.AddSeconds(10),
            //    SigningCredentials = credentials
            //};
            //var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            //return jwtTokenHandler.WriteToken(token);
            throw new NotImplementedException();
        }

        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
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

        public ClaimsPrincipal GetPrincipaleFromExpireToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("veryverysecret.....")),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecuriteToken = securityToken as JwtSecurityToken;
            if (jwtSecuriteToken == null || !jwtSecuriteToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }

        public async Task<Utilisateur> GetUserAsync(int userId)
        {
            return await context.Utilisateurs
               .FirstOrDefaultAsync(u => u.IdUtilisateur == userId);
        }

        public Task<Utilisateur> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Utilisateur> GetUserByEmailToken(string emailToken)
        {
            return await context.Utilisateurs.AsNoTracking().FirstOrDefaultAsync(u => u.Email == emailToken);
        }

        public async Task<IList<Utilisateur>> GetUsers()
        {
              var users = await context.Utilisateurs.ToListAsync();

           
            await context.SaveChangesAsync();

            return users;
        
        
        }

        public async Task<List<Utilisateur>> GetUsersAsync()
        {
            return await context.Utilisateurs.ToListAsync();
        }

        public Task<Utilisateur> RefreshToken(TokenApiDto tokenApiDto)
        {
            throw new NotImplementedException();
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

        public async Task SendEmail(Utilisateur user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task SendResetEmail(Utilisateur user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
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
