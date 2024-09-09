using GymApplication.Repository;
using GymApplication.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public CoursController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCoursess()
        {
            var cours = await uow.CourRepository.GetCourAsync();

            return Ok(cours);
        }

        [HttpGet]
        [Route("CountAllCourses")]
        public async Task<IActionResult> CountAllCoursess()
        {
            var cours = await uow.CourRepository.CountCourAsync();

            return Ok(cours);
        }
        [HttpGet]
        [Route("coursecounts")]
        public ActionResult<Dictionary<string, int>> GetCourseCountsByInstructor()
        {
            var courseCounts = uow.CourRepository.GetCourseCountByInstructor();

            if (courseCounts == null || courseCounts.Count == 0)
            {
                return NotFound("No courses found or no instructors available.");
            }

            return Ok(courseCounts);
        }

        //[HttpPost]
        //[Route("Insertcourse")]
        //public async Task<IActionResult> AddCouseAsync([FromBody] Cour request)
        //{
        //    //Add abonnement
        //    await uow.CourRepository.AddCourAsync(request);

        //    //Return abonnement
        //    return Ok(new { Message = "course added successfully !!" });

        //}



        [HttpPost]
        [Route("Insertcourse")]
        public async Task<IActionResult> AddCouseAsync([FromForm] Cour request, [FromForm] IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var extension = Path.GetExtension(image.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Define the path where the image will be saved
                var path = Path.Combine("wwwroot/images/courses", uniqueFileName);

                // Save the image to the specified path
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Update the course request with just the image filename (without path)
                request.ImagePath = uniqueFileName;
            }

            // Add the course to the database
            await uow.CourRepository.AddCourAsync(request);

            // Return a success message
            return Ok(new { Message = "Course added successfully with image!" });
        }

      

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await uow.CourRepository.GetCourAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }



        //[HttpPut]
        //[Route("{id:int}")]
        //public async Task<IActionResult> UpdateCourseAsync([FromRoute] int id, [FromBody] Cour requet)
        //{
        //    //Check course exist
        //    if (await uow.CourRepository.Exist(id))
        //    {
        //        //Update course
        //        var updatedcour = await uow.CourRepository.UpdateCourAsync(id, requet);
        //        if (updatedcour != null)
        //        {
        //            //Return course
        //            return Ok(updatedcour);
        //        }
        //    }
        //    return NotFound();

        //}

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCourseAsync([FromRoute] int id, [FromForm] Cour request, [FromForm] IFormFile image)
        {
            // Check if the course exists
            if (await uow.CourRepository.Exist(id))
            {
                if (image != null && image.Length > 0)
                {
                    // Generate a unique file name
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    // Define the path where the image will be saved
                    var path = Path.Combine("wwwroot/images/courses", uniqueFileName);

                    // Save the image to the specified path
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Update the course request with just the image filename (without path)
                    request.ImagePath = uniqueFileName;
                }

                // Update course
                var updatedcour = await uow.CourRepository.UpdateCourAsync(id, request);
                if (updatedcour != null)
                {
                    // Return updated course
                    return Ok(updatedcour);
                }
            }
            return NotFound();
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCourAsync([FromRoute] int id)
        {
            // Check if the course exists
            if (await uow.CourRepository.Exist(id))
            {
                // Delete the course
                var deletedCourse = await uow.CourRepository.DeleteCourAsync(id);

                if (deletedCourse != null)
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


        [HttpGet]
        [Route("GetImage/{id}")]
        public async Task<IActionResult> GetCourseImageByIdAsync(int id)
        {
            // Step 1: Retrieve the course record from the database
            var course = await uow.CourRepository.GetCourAsync(id);
            if (course == null)
            {
                // Log and return if course is not found
                Console.WriteLine($"Course with ID {id} not found.");
                return NotFound();
            }

            // Step 2: Get the image filename from the retrieved course record
            var imageFileName = course.ImagePath; // Assuming ImagePath is just the filename
            if (string.IsNullOrEmpty(imageFileName))
            {
                // Log and return if image filename is not found
                Console.WriteLine($"No image associated with course ID {id}.");
                return NotFound();
            }

            // Step 3: Construct the full file path
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/courses", imageFileName);
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



        [HttpPut]
        [Route("UpdateCourseImage/{id:int}")]
        public async Task<IActionResult> UpdateCourseImageAsync([FromRoute] int id, [FromForm] IFormFile image)
        {
            // Check if the event exists
            if (!await uow.CourRepository.Exist(id))
            {
                return NotFound(new { Message = "Course not found." });
            }

            if (image == null || image.Length == 0)
            {
                return BadRequest(new { Message = "No image uploaded." });
            }

            // Generate a unique file name
            var fileName = Path.GetFileNameWithoutExtension(image.FileName);
            var extension = Path.GetExtension(image.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

            // Define the path where the image will be saved
            var path = Path.Combine("wwwroot/images/courses", uniqueFileName);

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Save the image to the specified path
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Update the event with the new image filename
            var courseToUpdate = await uow.CourRepository.GetCourAsync(id);
            if (courseToUpdate != null)
            {
                courseToUpdate.ImagePath = uniqueFileName;

                // Update the event in the database
                var updatedcourse = await uow.CourRepository.UpdateCourAsync(id, courseToUpdate);
                if (updatedcourse != null)
                {
                    return Ok(new { Message = "Course image updated successfully!", Event = updatedcourse });
                }
            }

            return StatusCode(500, new { Message = "An error occurred while updating the course image." });
        }



    }
}


