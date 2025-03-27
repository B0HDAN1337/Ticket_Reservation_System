using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetById(int id);
        User Create(User users);
        Task<User> Update(int id, User users);
        User Delete(int id);
    }
}
