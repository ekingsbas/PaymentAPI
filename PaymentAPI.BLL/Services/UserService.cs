using PaymentAPI.BLL.Contracts;
using PaymentAPI.DAL.Models;

namespace PaymentAPI.BLL.Services
{
    /// <summary>
    /// Service class for Users
    /// </summary>
    public class UserService : IUserService
    {
        // Set Users for authentication purposes
        private List<User> _users = 
            new List<User>()
            {
            new User
            {
                Id = Guid.Parse("5818ad54-c23b-43b8-ad0e-2f55fdd738e8"), Username = "emmanuel", Password = "emmanuel123"
            },
            new User
            {
                Id = Guid.Parse("dfc07638-26e0-4bca-b6a7-21cceb6fcda8"), Username = "juan", Password = "juan123"
            },
            new User
            {
                Id = Guid.Parse("da2376a9-fe7f-41c6-b00a-d60de03e096c"), Username = "daniel", Password = "daniel123"
            }
        };

        public UserService()
        {
            
        }
        /// <summary>
        /// Function for User Authentication
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>Boolean indicating if user was authenticated</returns>
        public async Task<bool> Authenticate(string username, string password)
        {
            if (await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password)) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function for Getting User names
        /// </summary>
        /// <returns>List of User names</returns>
        public async Task<List<string>> GetUserNames()
        {
            List<string> users = new List<string>();
            foreach (var user in _users)
            {
                users.Add(user.Username);
            }
            return await Task.FromResult(users);
        }
    }
}
