namespace GymApplication.Repository.Models
{
    public interface IUnitOfWork
    {
        IAbonnementRepository AbonnementRepository { get; }
        ICourRepository CourRepository { get; }
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        IPaiementRepository PaiementRepository { get; }
        IContactRepository ContactRepository { get; }
        IImageRepository LocalStorageImageRepository { get; }
        int Complete();
    }
}
