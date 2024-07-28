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
using System.Security.Cryptography;

namespace GymApplication.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly GymDbContext context;
        private readonly PasswordHacher PasswordHacher;
        private readonly string jwtSecret;

        public UserRepository(GymDbContext context, PasswordHacher PasswordHacher, string jwtSecret)
        {
            this.context = context;
            this.PasswordHacher = PasswordHacher;
            this.jwtSecret = jwtSecret;
        }

        public async Task<Utilisateur> AddUserAsync(Utilisateur request)
        {
            request.MotDePasse = PasswordHacher.HashPassword(request.MotDePasse);
            request.CreatedAt = DateTime.Now;

            var user = await context.Utilisateurs.AddAsync(request);
            await context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<Utilisateur> Authenticate(string email)
        {
            return await context.Utilisateurs.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<bool> CheckEmailExistAsync(string email)
           => context.Utilisateurs.AnyAsync(x => x.Email == email);

        public string CheckPasswordStrengthAsync(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Password must be at least 8 characters long &&" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password must contain at least one uppercase letter, one lowercase letter, and one number &&" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[!@#$%^&*()_+=\\[\\]{};':\"\\\\|,.<>\\/?]"))
                sb.Append("Password must contain at least one special character" + Environment.NewLine);
            return sb.ToString();
        }

        public Task<bool> CheckUserNameExistAsync(string username)
             => context.Utilisateurs.AnyAsync(x => x.Nom == username);

        public string CreateJwtToken(Utilisateur user)
        {
            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.NameIdentifier, user.IdUtilisateur.ToString())
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    issuer: "yourdomain.com",
            //    audience: "yourdomain.com",
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: creds);

            //return new JwtSecurityTokenHandler().WriteToken(token);
           
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var key = "veryverysecret.....";
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(nameof(key), "Key for token generation cannot be null or empty.");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? throw new ArgumentNullException(nameof(user.Email))),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        // Ajoutez d'autres revendications si nécessaire
    };

                var token = new JwtSecurityToken(
                    issuer: "yourIssuer",
                    audience: "yourAudience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            

        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
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
            return await context.Utilisateurs.FirstOrDefaultAsync(u => u.IdUtilisateur == userId);
        }

        public async Task<Utilisateur> GetUserByEmail(string email)
        {
            return await context.Utilisateurs.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Utilisateur> GetUserByEmailToken(string emailToken)
        {
            return await context.Utilisateurs.AsNoTracking().FirstOrDefaultAsync(u => u.Email == emailToken);
        }

        public async Task<IList<Utilisateur>> GetUsers()
        {
            return await context.Utilisateurs.ToListAsync();
        }

        public async Task<List<Utilisateur>> GetUsersAsync()
        {
            return await context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur> RefreshToken(TokenApiDto tokenApiDto) { 
            return null;
            //{
            //    var principal = GetPrincipaleFromExpireToken(tokenApiDto.AccessToken);
            //    var email = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //    var user = await GetUserByEmail(email);

            //    if (user == null || user.RefreshToken != tokenApiDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            //    {
            //        throw new SecurityTokenException("Invalid refresh token");
            //    }

            //    var newAccessToken = CreateJwtToken(user);
            //    var newRefreshToken = CreateRefreshToken();

            //    user.RefreshToken = newRefreshToken;
            //    await context.SaveChangesAsync();

            //    return new Utilisateur
            //    {
            //        IdUtilisateur = user.IdUtilisateur,
            //        Email = user.Email,
            //        Token = newAccessToken,
            //        RefreshToken = newRefreshToken
            //    };
        }

        public async Task<Utilisateur> RegisterUser(Utilisateur request)
        {
            request.MotDePasse = PasswordHacher.HashPassword(request.MotDePasse);
            request.Token = "";
            request.CreatedAt = DateTime.Now;

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
