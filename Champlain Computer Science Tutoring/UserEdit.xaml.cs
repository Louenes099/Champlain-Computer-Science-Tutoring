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
    public partial class UserEdit : ContentPage
    {
        User user = new User();
        public UserEdit(User user)
        {
            this.user = user;
            InitializeComponent();
            txtId.Text = user.UserID;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
            positionEntry.Content = user.Type;
            if(user.Type == "tutor")
            {
                pickTeacher.IsVisible = true;
            }
            else
            {
                pickTeacher.IsVisible = false;
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
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text) || 
                string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else if (txtId.Text == "admin" || txtId.Text == "Admin" || txtId.Text == "ADMIN")
            {
                await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
            }
            else
            {
                if(user.Type == "tutor")
                {
                    await App.Database.UpdateUser(new User
                    {
                        Key = user.Key,
                        UserID = txtId.Text,
                        Password = user.Password,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        AssignedTeacher = (string)pickTeacher.SelectedItem,
                        Type = (string)positionEntry.Content,
                        Authentication = user.Authentication
                    });
                    await DisplayAlert("Register Result", "Success", "OK");
                }
                else
                {
                    await App.Database.UpdateUser(new User
                    {
                        Key = user.Key,
                        UserID = txtId.Text,
                        Password = user.Password,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        AssignedTeacher = null,
                        Type = (string)positionEntry.Content,
                        Authentication = user.Authentication
                    });
                    await DisplayAlert("Register Result", "Success", "OK");
                }
                await Navigation.PushAsync(new UserList(user.Type));
            }
        }
    }
}