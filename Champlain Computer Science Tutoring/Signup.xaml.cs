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
    public partial class Signup : ContentPage
    {
        String type = "";
        public Signup(string type)
        {
            this.type = type;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetUnauthenticated(type);
        }

        async void ApproveTap_Tapped(object sender, EventArgs e)
        {
            string Key = ((TappedEventArgs)e).Parameter.ToString();
            User user = await App.Database.GetOneUser(Key);
            await App.Database.UpdateUser(new User
            {
                Key = user.Key,
                UserID = user.UserID,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Type = user.Type,
                Authentication = "approved"
            });
            OnAppearing();
        }

        async void RefuseTap_Tapped(object sender, EventArgs e)
        {
            string Key = ((TappedEventArgs)e).Parameter.ToString();
            User user = await App.Database.GetOneUser(Key);
            if (user != null)
            {
                await App.Database.DeleteUser(Key);
                await DisplayAlert("Success", "User Deleted= " + user.FirstName, "OK");
            }
            else
            {
                await DisplayAlert("Required", "User doesn't exist", "OK");
            }
            OnAppearing();
        }
    }
}