using System.Collections.Generic;
using System.Linq;
using knewinAPI.Models;

namespace knewinAPI.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password){
            var users = new List<User>();
            users.Add(new User {Id = 1, Username = "adm", Password = "adm" , Role = "manager"});
            
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();

        }
        
    }
}