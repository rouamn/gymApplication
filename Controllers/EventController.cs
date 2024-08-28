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

        //[HttpPost]
        //[Route("Insertevent")]
        //public async Task<IActionResult> AddEventAsync([FromBody] Evenement request)
        //{
        //    //Add abonnement
        //    await uow.EventRepository.AddEventAsync(request);

        //    //Return abonnement
        //    return Ok(new { Message = "event added successfully !!" });

        //}

        [HttpPost]
        [Route("Insertevent")]
        public async Task<IActionResult> AddEventAsync([FromForm] Evenement request, [FromForm] IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Define the path where the image will be saved
                var path = Path.Combine("wwwroot/images/events", uniqueFileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // Save the image to the specified path
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Update the event request with just the image filename (without path)
                request.ImagePath = uniqueFileName;
            }

            // Add the event to the database
            await uow.EventRepository.AddEventAsync(request);

            // Return a success message
            return Ok(new { Message = "Event added successfully with image!" });
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
                    return StatusCode(500, "An error occurred while deleting the event.");
                }
            }
            // Course not found
            return NotFound("event not found");
        }





        [HttpGet]
        [Route("GetImage/{id}")]
        public async Task<IActionResult> GetEventImageByIdAsync(int id)
        {
            // Step 1: Retrieve the course record from the database
            var course = await uow.EventRepository.GetEventAsync(id);
            if (course == null)
            {
                // Log and return if course is not found
                Console.WriteLine($"Event with ID {id} not found.");
                return NotFound();
            }

            // Step 2: Get the image filename from the retrieved course record
            var imageFileName = course.ImagePath; // Assuming ImagePath is just the filename
            if (string.IsNullOrEmpty(imageFileName))
            {
                // Log and return if image filename is not found
                Console.WriteLine($"No image associated with event ID {id}.");
                return NotFound();
            }

            // Step 3: Construct the full file path
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/events", imageFileName);
            Console.WriteLine($"Image Path: {imagePath}");

            // Step 4: Check if the file exists in the specified path
            if (!System.IO.File.Exists(imagePath))
            {
                // Log and return if file does not exist
                Console.WriteLine($"File not found at path: {imagePath}");
                return NotFound();
            }

            // Step 5: If the file exists, return the image
            var fileBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            // Determine the content type based on the file extension
            string contentType = GetContentType(imageFileName);

            // Return the image file with the correct content type
            return File(fileBytes, contentType);
        }

        // Helper function to determine content type
        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream", // default for unknown types
            };
        }

    }
}
