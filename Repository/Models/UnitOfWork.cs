using GymApplication.Helpers;

namespace GymApplication.Repository.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAbonnementRepository AbonnementRepository =>  new AbonnementRepository(_context);

        public ICourRepository CourRepository =>  new CourRepository(_context);

        public IUserRepository UserRepository => new UserRepository(_context, PasswordHacher, jwtSecret);

        public IEventRepository EventRepository => new EventRepository(_context);

        public IContactRepository ContactRepository =>  new ContactRepository(_context);
        public IImageRepository LocalStorageImageRepository => new LocalStorageImageRepository(_context);

        public IPaiementRepository PaiementRepository =>  new PaiementRepository(_context);

        private readonly GymDbContext _context;
        private readonly PasswordHacher PasswordHacher;
        private readonly string jwtSecret;
        public UnitOfWork(GymDbContext context)
        {
            _context = context;

        }
        public UnitOfWork(GymDbContext context, PasswordHacher PasswordHacher, string jwtSecret)
        {
            _context = context;
            PasswordHacher=PasswordHacher;
            this.jwtSecret = jwtSecret;

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
