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
        private List<User> _users = new List<User>();

        public UserRepository()
        {
            _users = new List<User> { new User { ID = 1, Name = "Test", 
                Reviews= new List<Review>{
                            new Review {ID=1,GameId=1,UserId=1,Score=8,Name="TOP",Content="Very good game" } } } };
        }

        public Task<int> Create(User user)
        {
            _users.Add(user);
            return Task.FromResult(user.ID);
        }

        public Task<bool> Delete(int id)
        {
            if (!_users.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            _users.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<User>> ReadAll()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<User?> ReadById(int id)
        {
            var rev = _users.Find(x => x.ID == id);
            return Task.FromResult(rev);
        }

        public Task<bool> Update(User user)
        {
            var userToUpd = _users.Find(x => x.ID == user.ID);
            if (userToUpd == null)
            {
                return Task.FromResult(false);
            }
            userToUpd.Name = user.Name;
            userToUpd.Reviews = user.Reviews;
            return Task.FromResult(true);
        }
    }
}
