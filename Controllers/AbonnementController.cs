using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AbonnementController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public AbonnementController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        [Route("GetAllAbonnements")]
        public async Task<IActionResult> GetAllAbonnements()
        {
            var abonnements = await uow.AbonnementRepository.GetAbonementAsync();

            return Ok(abonnements);
        }


        [HttpPost]
        [Route("InsertAbonnement")]
        public async Task<IActionResult> AddLivreAsync([FromBody] Abonnement request)
        {
            //Add livre
            await uow.AbonnementRepository.AddAbonementAsync(request);

            //Return livre
            return Ok(new { Message = "Abonnement added successfully !!" });

        }

    }
}
