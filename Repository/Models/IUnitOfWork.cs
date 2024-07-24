namespace GymApplication.Repository.Models
{
    public interface IUnitOfWork
    {
        IAbonnementRepository AbonnementRepository { get; }
        ICourRepository CourRepository { get; }
        int Complete();
    }
}
