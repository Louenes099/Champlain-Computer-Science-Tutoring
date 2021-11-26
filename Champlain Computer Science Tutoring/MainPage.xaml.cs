using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Champlain_Computer_Science_Tutoring
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void btnteacher_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login("teacher"));
        }

        async void btnstudent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login("student"));
        }
    }
}
