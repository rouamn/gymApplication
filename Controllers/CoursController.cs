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

    }
}
