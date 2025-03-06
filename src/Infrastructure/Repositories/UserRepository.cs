using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class UserRepository : IUserRepository
    {
        private List<User> Users = new List<User>();
        public UserRepository()
        {

        }
        public Task Create(User user)
        {
            Users.Add(user);
            return Task.CompletedTask;
        }

        public Task<bool> Delete(int id)
        {
            if (Users.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            Users.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<List<User>> ReadAll()
        {
            return Task.FromResult(Users);
        }

        public Task<User?> ReadById(int id)
        {
            var rev = Users.Find(x => x.ID == id);
            return Task.FromResult(rev);
        }

        public Task<bool> Update(User user)
        {
            var userToUpt = Users.Find(x => x.ID == user.ID);
            if (userToUpt == null)
            {
                return Task.FromResult(false);
            }
            userToUpt.Name = user.Name;
            userToUpt.Reviews = user.Reviews;
            return Task.FromResult(true);
        }
    }
}
