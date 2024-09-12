using GymApplication.Repository;
using GymApplication.Repository.Models;
using GymApplication.UtilityService;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;

namespace GymApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaiementsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public PaiementsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        [Route("GetAllPaiements")]
        public async Task<IActionResult> GetAllPaiments()
        {
            var paiements = await uow.PaiementRepository.GetPaiementAsync();

            return Ok(paiements);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaiementById(int id)
        {
            var paiement = await uow.PaiementRepository.GetPaiementAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }
            return Ok(paiement);
        }
        [HttpGet("operation/{id}")]
        public async Task<IActionResult> GetPaiementByOperationId(string id)
        {
            // Fetch the paiement using the Unit of Work's PaiementRepository
            var paiement = await uow.PaiementRepository.GetPaiementByOperationId(id);

            // Check if the paiement was found
            if (paiement == null)
            {
                return NotFound(new { message = "Paiement not found with the given OperationId." });
            }

            // Return the found paiement
            return Ok(paiement);
        }



        [HttpPost]
        [Route("Insertpaiement")]
        public async Task<IActionResult> AddPaiementAsync([FromBody] Paiement request)
        {     
                //Add paiment
                await uow.PaiementRepository.AddPaiementAsync(request);
            //Return paiment
            return Ok(new { Message = "paiement added successfully !!" });         
        }

        [HttpGet]
        [Route("CountAllPaiements")]
        public async Task<IActionResult> CountAllCoursess()
        {
            var paiements = await uow.PaiementRepository.CountPaiementAsync();

            return Ok(paiements);
        }


        [HttpPut("{id:int}/visibility")]
        public async Task<IActionResult> UpdateVisibility([FromRoute] int id, [FromBody] bool newVisibility)
        {
            newVisibility = false;
            await uow.PaiementRepository.UpdateVisibilityAsync(id, newVisibility);
            return Ok(new { Message = "Visibility updated successfully." });    
        }


    }
}
