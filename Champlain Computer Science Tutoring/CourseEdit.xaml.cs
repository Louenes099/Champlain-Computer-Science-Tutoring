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
    public partial class CourseEdit : ContentPage
    {
        Course course = new Course();
        public CourseEdit(Course course)
        {
            InitializeComponent();
            this.course = course;
            if (course.Name != "empty")
            {
                txtId.Text = course.CourseID;
                txtName.Text = course.Name;
                txtSection.Text = course.Section;
                pickTeacher.SelectedItem = course.Teacher;
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
            List<User> TeacherName = await App.Database.GetAuthenticated("teacher");
            foreach (User t in TeacherName)
            {
                pickTeacher.Items.Add(t.FirstName + " " + t.LastName);
            }
        }

        async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSemester.Text) ||
                string.IsNullOrWhiteSpace(txtSection.Text) || string.IsNullOrWhiteSpace((string)pickTeacher.SelectedItem))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else
            {
                if (course.Name != "empty")
                {
                    await App.CourseDatabase.UpdateCourse(new Course
                    {
                        Key = course.Key,
                        CourseID = txtId.Text,
                        Section = txtSection.Text,
                        Name = txtName.Text,
                        Semester = txtSemester.Text,
                        Teacher = (string)pickTeacher.SelectedItem
                    });
                    await DisplayAlert("Update Result", "Success", "OK");
                }
                else
                {
                await App.CourseDatabase.SaveCourse(new Course
                {
                    CourseID = txtId.Text,
                    Section = txtSection.Text,
                    Name = txtName.Text,
                    Semester = txtSemester.Text,
                    Teacher = (string)pickTeacher.SelectedItem
                });
                    await DisplayAlert("Adding Result", "Success", "OK");
                }
            }
            await Navigation.PushAsync(new CourseList());
        }
    }
}