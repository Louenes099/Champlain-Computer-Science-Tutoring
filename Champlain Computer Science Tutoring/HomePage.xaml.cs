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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            AdminPanel.IsVisible = false;
            TeacherPanel.IsVisible = false;
            if (App.User.Type == "admin")
            {
                AdminPanel.IsVisible = true;
            }
            if (App.User.Type == "teacher")
            {
                AdminPanel.IsVisible = true;
            }
            if (App.User.Type == "tutor")
            {
                AdminPanel.IsVisible = true;
            }
            if (App.User.Type == "student")
            {
                AdminPanel.IsVisible = true;
            }
        }

        async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}