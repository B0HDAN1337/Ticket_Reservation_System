using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Service
{
    public interface IUserService
    {
        IQueryable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User users);
        Task<User> UpdateUser(int id, User users);
        User DeleteUser(int id);
    }
}
