using GymApplication.Helpers;
using GymApplication.Repository.Models;
using GymApplication.Repository.Models.Dto;
using GymApplication.UtilityService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public UserController(IUnitOfWork uow , IConfiguration configuration,
            IEmailService emailService)
        {
            this.uow = uow;
            _configuration = configuration;
            _emailService = emailService;
        }


     

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


        [HttpPost]
        [Route("/authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request payload.");
            }

            var user = await uow.UserRepository.Authenticate(request.Email);
            if (user == null || !PasswordHacher.VerifyPassword(request.Password, user.MotDePasse))
            {
                return Unauthorized();
            }

            var token = uow.UserRepository.CreateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("/send-mail")]
        public async Task<IActionResult> SendMail(MailData mailData)
        {
            _emailService.SendMail(mailData);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Reset password link has been sent to your email"
            });

        }



        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await uow.UserRepository.GetUserByEmail(email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User not found"
                });
            }

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);

            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(
                email,
                "Reset Password",
                EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);

            await uow.UserRepository.SendEmail(user);


            return Ok(new
            {
                StatusCode = 200,
                Message = "Reset password link has been sent to your email"
            });

        }
    }
}
