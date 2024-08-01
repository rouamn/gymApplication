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

        [HttpPost]
        [Route("Insertcourse")]
        public async Task<IActionResult> AddCouseAsync([FromBody] Cour request)
        {
            //Add abonnement
            await uow.CourRepository.AddCourAsync(request);

            //Return abonnement
            return Ok(new { Message = "course added successfully !!" });

        }

        [HttpGet]
        [Route("/Cours/{id:int}")]
        public async Task<IActionResult> GetAbonnement(int courId)
        {
            var cour = await uow.CourRepository.GetCourAsync(courId);

            if (cour == null)
            {
                return NotFound();
            }

            return Ok(cour);

        }
        [HttpPut]
        [Route("UpdateCourse/{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Cour request)
        {
            var updatedCourse = await uow.CourRepository.UpdateCourAsync(id, request);
            if (updatedCourse == null)
            {
                return NotFound(new { Message = "Course not found." });
            }
            return Ok(new { Message = "Course updated successfully.", Course = updatedCourse });
        }

        [HttpDelete]
        [Route("DeleteCourse/{id:int}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var deletedCourse = await uow.CourRepository.DeleteCourAsync(id);
            if (deletedCourse == null)
            {
                return NotFound(new { Message = "Course not found." });
            }
            return Ok(new { Message = "Course deleted successfully." });
        }
    }

}


