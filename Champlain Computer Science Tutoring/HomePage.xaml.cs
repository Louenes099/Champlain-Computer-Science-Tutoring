using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Champlain_Computer_Science_Tutoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            AdminPanel.IsVisible = false;
            TeacherPanel.IsVisible = false;
            TutorPanel.IsVisible = false;
            StudentPanel.IsVisible = false;
            if (App.User.Type == "admin")
            {
                AdminPanel.IsVisible = true;
            }
            if (App.User.Type == "teacher")
            {
                TeacherPanel.IsVisible = true;
            }
            if (App.User.Type == "tutor")
            {
                TutorPanel.IsVisible = true;
            }
            if (App.User.Type == "student")
            {
                StudentPanel.IsVisible = true;
            }
        }

        async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        //Signup Approval
        async void Button_Clicked(object sender, EventArgs e)
        {
            String Type = ((Button)sender).CommandParameter.ToString();
            await Navigation.PushAsync(new Signup(Type));
        }
        //User List
        async void Button_Clicked_1(object sender, EventArgs e)
        {
            String Type = ((Button)sender).CommandParameter.ToString();
            await Navigation.PushAsync(new UserList(Type));
        }
        //Course List
        async void Button_Clicked_2(object sender, EventArgs e)
        {
            String Type = ((Button)sender).CommandParameter.ToString();
            await Navigation.PushAsync(new Signup(Type));
        }
        //Tutoring Session List
        async void Button_Clicked_3(object sender, EventArgs e)
        {
            String Type = ((Button)sender).CommandParameter.ToString();
            await Navigation.PushAsync(new Signup(Type));
        }
    }
}