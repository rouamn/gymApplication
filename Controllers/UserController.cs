using GymApplication.Helpers;
using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public UserController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        //[HttpPost("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] Utilisateur userObj)
        //{
        //    if (userObj == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await uow.UserRepository.Authenticate(userObj.Nom);

        //    if (user == null)
        //        return NotFound(new { Message = "User not found !" });

        //    if (!PasswordHacher.VerifyPassword(userObj.Password, user.PasswordHashed))
        //    {
        //        return BadRequest(new { Message = "Password is incorrect !" });
        //    }


        //    user.Token = uow.UserRepository.CreateJwtToken(user);
        //    var newAccessToken = user.Token;
        //    var newRefreshToken = uow.UserRepository.CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    user.RefreshTOkenExpiryTime = DateTime.Now.AddDays(5);
        //    uow.Complete();


        //    return Ok(new TokenApiDto()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}



        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Utilisateur request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var pass = uow.UserRepository.CheckPasswordStrengthAsync(request.MotDePasse);

            if (await uow.UserRepository.CheckUserNameExistAsync(request.Nom))
            {
                return BadRequest("UserName already exists");
            }
            else if (await uow.UserRepository.CheckEmailExistAsync(request.Email))
            {
                return BadRequest("Email already exists");
            }
            else if (!string.IsNullOrEmpty(pass))
            {
                return BadRequest(pass);
            }
            else
            {
                await uow.UserRepository.RegisterUser(request);

                return Ok(new { Message = "User registered successfully !!" });
            }
        }


        [HttpGet]
        [Route("/AllUsers")]
        public async Task<IActionResult> GetAllUsersWithFine()
        {
            var users = await uow.UserRepository.GetUsers();

            var result = users.Select(user => new
            {
                user.IdUtilisateur,
                user.Nom,
                user.Prenom,
                user.Email,
               

            });

            return Ok(users);
        }

    }
}
