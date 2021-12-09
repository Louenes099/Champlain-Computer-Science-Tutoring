using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Champlain_Computer_Science_Tutoring
{
    public class DBUser
    {
        FirebaseClient firebaseClient =
        new FirebaseClient
        ("https://appxamarin-ab9c4-default-rtdb.firebaseio.com/");
        public async Task<List<User>> GetUsers()
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).ToList();
        }
        public async Task<bool> SaveUser(User user)
        {
            var data = await firebaseClient.Child(nameof(User)).PostAsync(JsonConvert.SerializeObject(user));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            await firebaseClient.Child(nameof(User)
                + "/" + user.Key).
                PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }
        public async Task<bool> DeleteUser(string Key)
        {
            await firebaseClient.Child(nameof(User)
                + "/" + Key).DeleteAsync();
            return true;
        }

        public async Task<User> GetOneUser(string Key)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).Where(i => i.Key == Key).FirstOrDefault();
        }
        public async Task<User> GetLogin(string userid, string password)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).Where(i => (i.UserID == userid) && i.Password == password).FirstOrDefault();
        }
        public async Task<List<User>> GetUnauthenticated(string type)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).Where(i => (i.Type == type) && i.Authentication == "pending").ToList();
        }
        public async Task<List<User>> GetAuthenticated(string type)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).Where(i => i.Type == type && i.Authentication == "approved").ToList();
        }
        public async Task<List<User>> GetUserListName(string type)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                FirstName = item.Object.FirstName
            }).Where(i => i.Type == type && i.Authentication == "approved").ToList();
        }
        public async Task<List<User>> GetUserBasedOnName(string name)
        {
            return (await firebaseClient.Child(nameof(User)).OnceAsync<User>()).Select(item => new User
            {
                Key = item.Key,
                UserID = item.Object.UserID,
                Password = item.Object.Password,
                FirstName = item.Object.FirstName,
                LastName = item.Object.LastName,
                Email = item.Object.Email,
                Type = item.Object.Type,
                Authentication = item.Object.Authentication
            }).Where(i => (i.FirstName == name) && i.Authentication == "approved").ToList();
        }
    }
}
