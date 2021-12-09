using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Champlain_Computer_Science_Tutoring
{
    public class DBCourse
{
        FirebaseClient firebaseClient =
        new FirebaseClient
        ("https://appxamarin-ab9c4-default-rtdb.firebaseio.com/");
        public async Task<List<Course>> GetCourses()
        {
            return (await firebaseClient.Child(nameof(Course)).OnceAsync<Course>()).Select(item => new Course
            {
                Key = item.Key,
                CourseID = item.Object.CourseID,
                Name = item.Object.Name,
                Section = item.Object.Section,
                Semester = item.Object.Semester,
                Teacher = item.Object.Teacher
            }).ToList();
        }
        public async Task<bool> SaveCourse(Course course)
        {
            var data = await firebaseClient.Child(nameof(Course)).PostAsync(JsonConvert.SerializeObject(course));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            await firebaseClient.Child(nameof(Course)
                + "/" + course.Key).
                PutAsync(JsonConvert.SerializeObject(course));
            return true;
        }
        public async Task<bool> DeleteCourse(string Key)
        {
            await firebaseClient.Child(nameof(Course)
                + "/" + Key).DeleteAsync();
            return true;
        }

        public async Task<Course> GetOneCourse(string Key)
        {
            return (await firebaseClient.Child(nameof(Course)).OnceAsync<Course>()).Select(item => new Course
            {
                Key = item.Key,
                CourseID = item.Object.CourseID,
                Name = item.Object.Name,
                Section = item.Object.Section,
                Semester = item.Object.Semester,
                Teacher = item.Object.Teacher
            }).Where(i => i.Key == Key).FirstOrDefault();
        }
    }
}