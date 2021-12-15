using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Champlain_Computer_Science_Tutoring
{
    public class DBRegisteredTutoring
    {
        FirebaseClient firebaseClient =
        new FirebaseClient
        ("https://appxamarin-ab9c4-default-rtdb.firebaseio.com/");
        public async Task<RegisteredTutoring> GetTutorings(string key)
        {
            return (await firebaseClient.Child(nameof(RegisteredTutoring)).OnceAsync<RegisteredTutoring>()).Select(item => new RegisteredTutoring
            {
                Key = item.Key,
                StudentID = item.Object.StudentID,
                TutoringSession = item.Object.TutoringSession
            }).Where(i => i.StudentID == key).FirstOrDefault();
        }
        public async Task<bool> SaveRegisteredTutoring(RegisteredTutoring tutoring)
        {
            var data = await firebaseClient.Child(nameof(RegisteredTutoring)).PostAsync(JsonConvert.SerializeObject(tutoring));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateTutoring(RegisteredTutoring tutoring)
        {
            await firebaseClient.Child(nameof(RegisteredTutoring)
                + "/" + tutoring.Key).
                PutAsync(JsonConvert.SerializeObject(tutoring));
            return true;
        }
        public async Task<bool> DeleteTutoring(string Key)
        {
            await firebaseClient.Child(nameof(RegisteredTutoring)
                + "/" + Key).DeleteAsync();
            return true;
        }
    }
}