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
    public partial class UserList : ContentPage
    {
        String type = "";
        public UserList(string type)
        {
            this.type = type;
            InitializeComponent();
            collectionView.IsVisible = false;
            collectionView1.IsVisible = false;
            if (App.User.Type == "admin")
            {
                collectionView.IsVisible = true;
            }
            if (App.User.Type == "teacher")
            {
                collectionView1.IsVisible = true;
            }
            if (App.User.Type == "tutor")
            {
                collectionView1.IsVisible = true;
            }
            if (App.User.Type == "student")
            {
                collectionView1.IsVisible = true;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetAuthenticated(type);
            collectionView1.ItemsSource = await App.Database.GetAuthenticated(type);
        }

        async void EditTap_Tapped(object sender, EventArgs e)
        {
            string Key = ((TappedEventArgs)e).Parameter.ToString();
            User user = await App.Database.GetOneUser(Key);
            await Navigation.PushAsync(new UserEdit(user));
        }

        async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            String Key = ((TappedEventArgs)e).Parameter.ToString();
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