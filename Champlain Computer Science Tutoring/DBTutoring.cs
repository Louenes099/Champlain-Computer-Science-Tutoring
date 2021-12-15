using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Champlain_Computer_Science_Tutoring
{
    public class DBTutoring
    {
        FirebaseClient firebaseClient =
        new FirebaseClient
        ("https://appxamarin-ab9c4-default-rtdb.firebaseio.com/");
        public async Task<List<Tutoring>> GetTutorings()
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).ToList();
        }
        public async Task<bool> SaveTutoring(Tutoring tutoring)
        {
            var data = await firebaseClient.Child(nameof(Tutoring)).PostAsync(JsonConvert.SerializeObject(tutoring));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateTutoring(Tutoring tutoring)
        {
            await firebaseClient.Child(nameof(Tutoring)
                + "/" + tutoring.Key).
                PutAsync(JsonConvert.SerializeObject(tutoring));
            return true;
        }
        public async Task<bool> DeleteTutoring(string Key)
        {
            await firebaseClient.Child(nameof(Tutoring)
                + "/" + Key).DeleteAsync();
            return true;
        }

        public async Task<Tutoring> GetOneTutoring(string Key)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.Key == Key).FirstOrDefault();
        }
        public async Task<List<Tutoring>> GetTeacherTutorings(string name)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.CourseTeacher == name && i.Grade == null && i.TutoringTime > DateTime.Now).ToList();
        }
        public async Task<List<Tutoring>> GetTutorTutorings(string name)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.Tutor == name && i.Grade == null && i.TutoringTime > DateTime.Now).ToList();
        }
        public async Task<List<Tutoring>> GetAllTutorings()
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.Grade == null && i.TutoringTime > DateTime.Now).ToList();
        }
        public async Task<List<Tutoring>> GetTeacherUngradedTutorings(string name)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.CourseTeacher == name && i.Grade == null && i.TutoringTime < DateTime.Now).ToList();
        }
        public async Task<List<Tutoring>> GetTeacherGradedTutorings(string name)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.CourseTeacher == name && i.Grade != null).ToList();
        }
        public async Task<List<Tutoring>> GetTutorGradedTutorings(string name)
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.Tutor == name && i.Grade != null).ToList();
        }
        public async Task<List<Tutoring>> GetAllGradedTutorings()
        {
            return (await firebaseClient.Child(nameof(Tutoring)).OnceAsync<Tutoring>()).Select(item => new Tutoring
            {
                Key = item.Key,
                TutoringID = item.Object.TutoringID,
                CourseName = item.Object.CourseName,
                CourseSection = item.Object.CourseSection,
                CourseSemester = item.Object.CourseSemester,
                CourseTeacher = item.Object.CourseTeacher,
                Tutor = item.Object.Tutor,
                TutoringTime = item.Object.TutoringTime,
                TutoringSessionTime = item.Object.TutoringSessionTime,
                Grade = item.Object.Grade
            }).Where(i => i.Grade != null).ToList();
        }
    }
}