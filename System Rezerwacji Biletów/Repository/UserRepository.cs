using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SystemReservationContext _context;

        public UserRepository(SystemReservationContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsQueryable();
        }
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();
            return users;
        }

        public async Task<User> Update(int id, User users)
        {
            var existingUser = await _context.Users.FindAsync(id);

            existingUser.Name = users.Name;
            existingUser.LastName = users.LastName;
            existingUser.BirthDate = users.BirthDate;
            existingUser.email = users.email;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public User Delete(int id)
        {
            var userModel = _context.Users.Find(id);

            _context.Users.Remove(userModel);
            _context.SaveChanges();
            return userModel;
        }

    }
}
