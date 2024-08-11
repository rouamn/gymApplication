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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var abonnement = await uow.AbonnementRepository.GetAbonementAsync(id);

            if (abonnement == null)
            {
                return NotFound();
            }

            return Ok(abonnement);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAbonnementAsync([FromRoute] int id, [FromBody] Abonnement requet)
        {
            //Check abonnement exist
            if (await uow.AbonnementRepository.Exist(id))
            {
                //Update abonnement
                var updatedabonnement = await uow.AbonnementRepository.UpdateAbonementAsync(id, requet);
                if (updatedabonnement != null)
                {
                    //Return abonnement
                    return Ok(updatedabonnement);
                }
            }
            return NotFound();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAbonnementAsync([FromRoute] int id)
        {
            // Check if the Abonnement exists
            if (await uow.AbonnementRepository.Exist(id))
            {
                // Delete the Abonnement
                var deletedAbonnement = await uow.AbonnementRepository.DeleteAbonementAsync(id);

                if (deletedAbonnement != null)
                {
                    // Return Abonnement
                    return Ok("success");
                }
                else
                {
                    // Return failure if deletion was unsuccessful
                    return StatusCode(500, "An error occurred while deleting the abonnement.");
                }
            }
            // Abonnement not found
            return NotFound("abonnement not found");
        }

    }
}
