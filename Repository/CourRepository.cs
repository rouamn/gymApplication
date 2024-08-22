using GymApplication.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GymApplication.Repository
{
    public class CourRepository : ICourRepository
    {
        private readonly GymDbContext context;
        public CourRepository(GymDbContext context)
        {
            this.context = context;
        }

        public async Task<Cour> AddCourAsync(Cour request)
        {
            var cour = await context.Cours.AddAsync(request);
            await context.SaveChangesAsync();
            return cour.Entity;
        }

        public async Task<int> CountCourAsync()
        {
            var cours = await context.Cours.ToListAsync();
            var count = cours.Count;
            return count;
        }

        public async Task<Cour> DeleteCourAsync(int courId)
        {
            var cour = await GetCourAsync(courId);

            if (cour != null)
            {
                context.Cours.Remove(cour);
                await context.SaveChangesAsync();

                return cour;
            }

            return null;
        }

        public async Task<bool> Exist(int courId)
        {
            return await context.Cours.AnyAsync(s => s.IdCours == courId);
        }

        public async Task<ICollection> GetCourAsync()
        {
            var cours = await context.Cours.ToListAsync();
            var coursToSend = cours.Select(b => new
            {
                b.IdCours,
                b.Nom,
                b.Description,
                b.Duree,
                b.InstructorName,
                b.CourseDate,
            }).ToList();
            return coursToSend;
        }

        public async Task<Cour> GetCourAsync(int courId)
        {
            return await context.Cours
                .FirstOrDefaultAsync(u => u.IdCours == courId);
        }

        public Dictionary<string, int> GetCourseCountByInstructor()
        {
            {
                var courseCounts = context.Cours
                    .GroupBy(c => c.InstructorName)
                    .Select(group => new
                    {
                        InstructorName = group.Key,
                        CourseCount = group.Count()
                    })
                    .ToDictionary(x => x.InstructorName, x => x.CourseCount);

                return courseCounts;
            }
        }

        public async Task<Cour> UpdateCourAsync(int courId, Cour request)
        {
            var existingCour = await GetCourAsync(courId);

            if (existingCour != null)
            {
                existingCour.Nom = request.Nom;
                existingCour.Description = request.Description;
                existingCour.Duree = request.Duree;
                existingCour.CourseDate = request.CourseDate;
                existingCour.InstructorName = request.InstructorName;

                await context.SaveChangesAsync();

                return existingCour;

            }

            return null;
        }
    }
}
