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
    public partial class CourseList : ContentPage
    {
        public CourseList()
        {
            InitializeComponent();
            collectionView.IsVisible = false;
            collectionView1.IsVisible = false;
            if (App.User.Type == "admin")
            {
                collectionView1.IsVisible = true;
            }
            if (App.User.Type == "teacher")
            {
                collectionView.IsVisible = true;
            }
            if (App.User.Type == "tutor")
            {
                collectionView.IsVisible = true;
            }
            if (App.User.Type == "student")
            {
                collectionView.IsVisible = true;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.CourseDatabase.GetCourses();
            collectionView1.ItemsSource = await App.CourseDatabase.GetCourses();
        }
        async void EditTap_Tapped(object sender, EventArgs e)
        {
            string Key = ((TappedEventArgs)e).Parameter.ToString();
            Course course = await App.CourseDatabase.GetOneCourse(Key);
            await Navigation.PushAsync(new CourseEdit(course));
        }

        async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            String Key = ((TappedEventArgs)e).Parameter.ToString();
            Course course = await App.CourseDatabase.GetOneCourse(Key);
            if (course != null)
            {
                await App.CourseDatabase.DeleteCourse(Key);
                await DisplayAlert("Success", "Course Deleted= " + course.Name, "OK");
            }
            else
            {
                await DisplayAlert("Required", "Course doesn't exist", "OK");
            }
            OnAppearing();
        }

        async void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Course course = new Course();
            course.Name = "empty";
            await Navigation.PushAsync(new CourseEdit(course));
        }
    }
}