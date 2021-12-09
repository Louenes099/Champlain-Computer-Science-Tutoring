using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Champlain_Computer_Science_Tutoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private string UserType;
        public Login(string userType)
        {
            InitializeComponent();
            UserType = userType;
            if (userType == "teacher")
            {
                txtId.Placeholder = "Teacher ID";
            }
            else if (userType == "student")
            {
                txtId.Placeholder = "Student ID";
            }
            else
            {
                ErrorMessage();
            }
            CreateAdmin();
        }
        public async void ErrorMessage()
        {
            await DisplayAlert("ERROR", "UNAUTHORIZED METHOD!", "OK");
            await Navigation.PopAsync();
        }
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            string userName = txtId.Text;
            string PassWord = txtPassword.Text;
            App.User = await App.Database.GetLogin(userName, PassWord);
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else if (App.User != null)
            {
                if (App.User.Authentication == "approved")
                {
                    await DisplayAlert("Login result", "Success", "OK");
                    await Navigation.PushAsync(new HomePage());
                }
                else if (App.User.Authentication == "pending")
                {
                    await DisplayAlert("Login result", "Failure", "OK");
                }
            }
            else
            {
                await DisplayAlert("Login result", "Failure", "OK");
            }
        }
        async void CreateAdmin()
        {
            var x = await App.Database.GetUsers();
            if (x.Count == 0)
            {
                await App.Database.SaveUser(new User
                {
                    UserID = "0981234",
                    Password = "Amin123",
                    FirstName = "Amin",
                    LastName = "Ranj Bar",
                    Email = "arbar@crcmail.net",
                    Type = "admin",
                    Authentication = "approved"

                });
            }
        }

        async void BtnGoRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register(UserType));
        }
    }
}