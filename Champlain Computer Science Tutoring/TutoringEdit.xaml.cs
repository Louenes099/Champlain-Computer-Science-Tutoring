using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Champlain_Computer_Science_Tutoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutoringEdit : ContentPage
    {
        Tutoring tutoring = new Tutoring();
        public TutoringEdit(Tutoring tutoring)
        {
            InitializeComponent();
            this.tutoring = tutoring;
            if (tutoring.CourseName != "empty")
            {
                txtId.Text = tutoring.TutoringID;
                txtDuration.Text = tutoring.TutoringSessionTime;
                pickCourse.SelectedItem = tutoring.CourseName + " "+ tutoring.CourseSection + " " + tutoring.CourseTeacher;
                txtDate.Date = tutoring.TutoringTime;
                btnSend.Text = "Update";
            }
            else
            {
                btnSend.Text = "Add";
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Course> course = await App.CourseDatabase.GetTutorCourses(App.User.AssignedTeacher);
            foreach (Course t in course)
            {
                pickCourse.Items.Add(t.Name + " " + t.Section + " " + t.Teacher);
            }
        }

        async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtDuration.Text) || txtDate.Date == null)
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else
            {
                Course course = await App.CourseDatabase.GetCourse((string)pickCourse.SelectedItem);
                if (tutoring.CourseName != "empty")
                {
                    await App.TutoringDatabase.UpdateTutoring(new Tutoring
                    {
                        Key = tutoring.Key,
                        TutoringID = tutoring.TutoringID,
                        CourseName = course.Name,
                        CourseSection = course.Section,
                        CourseSemester = course.Semester,
                        CourseTeacher = course.Teacher,
                        Tutor = App.User.FirstName + " " + App.User.LastName,
                        TutoringTime = txtDate.Date,
                        TutoringSessionTime = txtDuration.Text,
                        Grade = null
                    });
                    await DisplayAlert("Update Result", "Success", "OK");
                }
                else
                {
                await App.TutoringDatabase.SaveTutoring(new Tutoring
                {
                    TutoringID = tutoring.TutoringID,
                    CourseName = course.Name,
                    CourseSection = course.Section,
                    CourseSemester = course.Semester,
                    CourseTeacher = course.Teacher,
                    Tutor = App.User.FirstName + " " + App.User.LastName,
                    TutoringTime = txtDate.Date,
                    TutoringSessionTime = txtDuration.Text,
                    Grade = null
                });
                    await DisplayAlert("Adding Result", "Success", "OK");
                }
            }
            await Navigation.PushAsync(new HomePage());
        }
    }
}