using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Classes
{
    public class StaticUserRepository: IUserRepository
    {
        private List<User> users = new List<User>()
        {
            //new User()
            //{
            //    Firstname = "Read", Lastname = "Only", EmailAddress = "readonly@user.com",
            //    Id = Guid.NewGuid(), Username = "ReadOnly", Password = "ReadOnly",
            //    Roles =  new List<string> { "reader"}
            //},
            //new User()
            //{
            //    Firstname = "Read", Lastname = "Write", EmailAddress = "readwrite@user.com",
            //    Id = Guid.NewGuid(), Username = "ReadWrite", Password = "ReadWrite",
            //    Roles =  new List<string> { "reader", "writer"}
            //}
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
            && (x.Password == password));

            return user;
        }
    }
}
