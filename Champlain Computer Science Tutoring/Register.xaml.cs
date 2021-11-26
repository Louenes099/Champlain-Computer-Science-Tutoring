﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Champlain_Computer_Science_Tutoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        string UserType = "";
        public Register(string userType)
        {
            InitializeComponent();
            UserType = userType;
            if (userType == "teacher")
            {
                txtId.Placeholder = "Teacher ID";
                positionEntry.Content = "Teacher";
                position1Entry.IsVisible = false;
            }
            else if (userType == "student")
            {
                txtId.Placeholder = "Student ID";
                positionEntry.Content = "Student";
            }
            else
            {
                errorMessage();
            }
        }
        private async void errorMessage()
        {
            await DisplayAlert("ERROR", "UNAUTHORIZED METHOD!", "OK");
            await Navigation.PopAsync();
        }

        async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (txtPassword.Text == null || txtPassword.Text.Length < 10)
            {
                await DisplayAlert("Register Result", "Failure, Password should have 10 characters", "OK");
            }
            else if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string action = await DisplayActionSheet("Error", "Cancel", null, "Missing Fields, Please fill out properly.");
            }
            else if (txtId.Text == "admin" || txtId.Text == "Admin" || txtId.Text == "ADMIN")
            {
                string action = await DisplayActionSheet("Error", "Cancel", null, "Forbidden, Cannot use reserved username");
            }
            else
            {
                if (positionEntry.IsChecked)
                {
                    await App.Database.SaveUser(new User
                    {
                        UserID = txtId.Text,
                        Password = txtPassword.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        Type = UserType,
                        Authentication = "pending"
                    });
                }
                else if (position1Entry.IsChecked){

                    await App.Database.SaveUser(new User
                    {
                        UserID = txtId.Text,
                        Password = txtPassword.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        Type = "tutor",
                        Authentication = "pending"
                    });
                }
                await DisplayAlert("Register Result", "Success", "OK");
            }
        }
    }
}