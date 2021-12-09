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
        public CourseEdit(Course course)
        {
            InitializeComponent();
            if (course != null)
            {
                txtId.Text = course.CourseID;
                txtName.Text = course.Name;
                txtSection.Text = course.Section;
                pickTeacher.SelectedItem = course.Teacher;
                btnSend.Text = "Update";
                btnSend.CommandParameter = course.Key;
            }
            else
            {
                btnSend.Text = "Add";
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            pickTeacher.ItemsSource = await App.Database.GetUserListName("teacher");
        }

        async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            String key = ((Button)sender).CommandParameter.ToString();
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtSection.Text) || pickTeacher.SelectedItem == null)
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else
            {
                if(btnSend.Text == "Update")
                {
                    await App.CourseDatabase.UpdateCourse(new Course
                    {
                        Key = key,
                        CourseID = txtId.Text,
                        Section = txtSection.Text,
                        Name = txtName.Text,
                        Teacher = (string)pickTeacher.SelectedItem
                    });
                    await DisplayAlert("Update Result", "Success", "OK");
                }
                else if(btnSend.Text == "Add")
                {
                await App.CourseDatabase.SaveCourse(new Course
                {
                    CourseID = txtId.Text,
                    Section = txtSection.Text,
                    Name = txtName.Text,
                    Teacher = (string)pickTeacher.SelectedItem
                });
                    await DisplayAlert("Adding Result", "Success", "OK");
                }
            }
        }
    }
}