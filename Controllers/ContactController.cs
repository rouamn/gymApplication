using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController  : ControllerBase
    {

        private readonly IUnitOfWork uow;
        public ContactController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        [HttpGet]
        [Route("GetAllContacts")]
        public async Task<IActionResult> GetAllContact()
        {
            var contacts = await uow.ContactRepository.GetContactAsync();

            return Ok(contacts);
        }


      

        [HttpPost]
        [Route("InsertContact")]
        public async Task<IActionResult> AddContactAsync([FromBody] Contact request)
        {
            //Add abonnement
            await uow.ContactRepository.AddContacAsync(request);

            //Return abonnement
            return Ok(new { Message = "contact added successfully !!" });

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await uow.ContactRepository.GetContacAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }





        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] int id)
        {
            // Check if the course exists
            if (await uow.ContactRepository.Exist(id))
            {
                // Delete the course
                var deletedContact = await uow.ContactRepository.DeleteContacAsync(id);

                if (deletedContact != null)
                {
                    // Return success
                    return Ok("success");
                }
                else
                {
                    // Return failure if deletion was unsuccessful
                    return StatusCode(500, "An error occurred while deleting the course.");
                }
            }
            // Course not found
            return NotFound("Course not found");
        }

    }
}
