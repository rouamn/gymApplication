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
        public async Task<IActionResult> AddAbonnementAsync([FromBody] Abonnement request)
        {
            //Add abonnement
            await uow.AbonnementRepository.AddAbonementAsync(request);

            //Return abonnement
            return Ok(new { Message = "Abonnement added successfully !!" });

        }

        [HttpGet]
        [Route("/Abonnements/{AbonnementId:int}")]
        public async Task<IActionResult> GetAbonnement(int abonnementId)
        {
            var abonnement = await uow.AbonnementRepository.GetAbonementAsync(abonnementId);

            if (abonnement == null)
            {
                return NotFound();
            }

            return Ok(abonnement);

        }

        [HttpPut]
        [Route("/Abonnements/{AbonnementId:int}")]
        public async Task<IActionResult> UpdateAbonnementAsync([FromRoute] int abonnementId, [FromBody] Abonnement requet)
        {
            //Check abonnement exist
            if (await uow.AbonnementRepository.Exist(abonnementId))
            {
                //Update abonnement
                var updatedAbonnement = await uow.AbonnementRepository.UpdateAbonementAsync(abonnementId, requet);
                if (updatedAbonnement != null)
                {
                    //Return abonnement
                    return Ok(updatedAbonnement);
                }
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("/DeleteAbonnement/{AbonnementId:int}")]
        public async Task<IActionResult> deleteAbonnementAsync([FromRoute] int abonnementId)
        {
            //Check livre exist
            if (await uow.AbonnementRepository.Exist(abonnementId))
            {
                //Delete livre
                var deletedLivre = await uow.AbonnementRepository.DeleteAbonementAsync(abonnementId);

                if (deletedLivre != null)
                {
                    //Return livre
                    return Ok("success");
                }

            }
            //Livre not found
            return Ok("fail");

        }

    }
}
