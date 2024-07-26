using GymApplication.Helpers;

namespace GymApplication.Repository.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAbonnementRepository AbonnementRepository =>  new AbonnementRepository(_context);

        public ICourRepository CourRepository =>  new CourRepository(_context);

        public IUserRepository UserRepository => new UserRepository(_context, PasswordHacher);

        private readonly GymDbContext _context;
        private readonly PasswordHacher PasswordHacher;
        public UnitOfWork(GymDbContext context)
        {
            _context = context;

        }
        public UnitOfWork(GymDbContext context, PasswordHacher PasswordHacher)
        {
            _context = context;
            PasswordHacher=PasswordHacher;

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
