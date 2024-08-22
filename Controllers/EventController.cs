using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IUnitOfWork uow;
        public EventController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await uow.EventRepository.GetEventAsync();

            return Ok(events);
        }


        [HttpGet]
        [Route("CountAllEvents")]
        public async Task<IActionResult> CountAllEvents()
        {
            var events = await uow.EventRepository.CountEventAsync();

            return Ok(events);
        }

        [HttpPost]
        [Route("Insertevent")]
        public async Task<IActionResult> AddEventAsync([FromBody] Evenement request)
        {
            //Add abonnement
            await uow.EventRepository.AddEventAsync(request);

            //Return abonnement
            return Ok(new { Message = "event added successfully !!" });

        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var evenement = await uow.EventRepository.GetEventAsync(id);

            if (evenement == null)
            {
                return NotFound();
            }

            return Ok(evenement);
        }



        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEventAsync([FromRoute] int id, [FromBody] Evenement requet)
        {
            //Check course exist
            if (await uow.EventRepository.Exist(id))
            {
                //Update course
                var updatedevent = await uow.EventRepository.UpdateEventAsync(id, requet);
                if (updatedevent != null)
                {
                    //Return course
                    return Ok(updatedevent);
                }
            }
            return NotFound();

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEventAsync([FromRoute] int id)
        {
            // Check if the course exists
            if (await uow.EventRepository.Exist(id))
            {
                // Delete the course
                var deletedEvent = await uow.EventRepository.DeleteEventAsync(id);

                if (deletedEvent != null)
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
