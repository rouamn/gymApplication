namespace GymApplication.Repository.Models
{
    public interface IUnitOfWork
    {
        IAbonnementRepository AbonnementRepository { get; }
        ICourRepository CourRepository { get; }
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        int Complete();
    }
}
