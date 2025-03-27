using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Repository;

namespace System_Rezerwacji_Biletów.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IQueryable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User CreateUser(User users)
        {
            return _userRepository.Create(users);
        }
        public async Task<User> UpdateUser(int id, User users)
        {
            return await _userRepository.Update(id, users);
        }
        public User DeleteUser(int id)
        {
            return _userRepository.Delete(id);
        }
    }
}
