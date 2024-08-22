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

        [HttpPost]
        [Route("Insertcourse")]
        public async Task<IActionResult> AddCouseAsync([FromBody] Cour request)
        {
            //Add abonnement
            await uow.CourRepository.AddCourAsync(request);

            //Return abonnement
            return Ok(new { Message = "course added successfully !!" });

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



        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCourseAsync([FromRoute] int id, [FromBody] Cour requet)
        {
            //Check course exist
            if (await uow.CourRepository.Exist(id))
            {
                //Update course
                var updatedcour = await uow.CourRepository.UpdateCourAsync(id, requet);
                if (updatedcour != null)
                {
                    //Return course
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

    }

}


