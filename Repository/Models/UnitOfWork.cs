namespace GymApplication.Repository.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAbonnementRepository AbonnementRepository =>  new AbonnementRepository(_context);

        public ICourRepository CourRepository =>  new CourRepository(_context);


        private readonly GymDbContext _context;
        public UnitOfWork(GymDbContext context)
        {
            _context = context;

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
