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
    public partial class TutoringList : ContentPage
    {
        public TutoringList(string demand)
        {
            InitializeComponent();
            collectionView.IsVisible = false;
            collectionView1.IsVisible = false;
            collectionView2.IsVisible = false;
            collectionView3.IsVisible = false;
            collectionView4.IsVisible = false;
            AddToolBarItem.IsEnabled = false;
            if (demand == "grade")
            {
                collectionView.IsVisible = true;
            }
            if (demand == "graded")
            {
                collectionView2.IsVisible = true;
            }
            if (demand == "teacher")
            {
                collectionView1.IsVisible = true;
            }
            if (demand == "tutor")
            {
                collectionView1.IsVisible = true;
                AddToolBarItem.IsEnabled = true;
            }
            if (demand == "student")
            {
                collectionView3.IsVisible = true;
            }
            if (demand == "registered")
            {
                collectionView4.IsVisible = true;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (App.User.Type == "Admin")
            {
                collectionView2.ItemsSource = await App.TutoringDatabase.GetAllGradedTutorings();
            }
            if (App.User.Type == "teacher")
            {
                collectionView.ItemsSource = await App.TutoringDatabase.GetTeacherUngradedTutorings(App.User.FirstName + " " + App.User.LastName);
                collectionView1.ItemsSource = await App.TutoringDatabase.GetTeacherTutorings(App.User.FirstName + " " + App.User.LastName);
                collectionView2.ItemsSource = await App.TutoringDatabase.GetTeacherGradedTutorings(App.User.FirstName + " " + App.User.LastName);
            }
            if(App.User.Type == "tutor")
            {
                collectionView1.ItemsSource = await App.TutoringDatabase.GetTutorTutorings(App.User.FirstName + " " + App.User.LastName);
                collectionView2.ItemsSource = await App.TutoringDatabase.GetTutorGradedTutorings(App.User.FirstName + " " + App.User.LastName);
            }
            if (App.User.Type == "student")
            {
                collectionView3.ItemsSource = await App.TutoringDatabase.GetAllTutorings();
                RegisteredTutoring registeredTutoring = await App.RegisteredTutoringDatabase.GetTutorings(App.User.Key);
                collectionView4.ItemsSource = registeredTutoring.TutoringSession;
            }
        }
        async void EditTap_Tapped(object sender, EventArgs e)
        {
            string Key = ((TappedEventArgs)e).Parameter.ToString();
            Tutoring tutoring = await App.TutoringDatabase.GetOneTutoring(Key);
            if (tutoring != null && tutoring.Tutor == (App.User.FirstName + " " + App.User.LastName))
            {
                await Navigation.PushAsync(new TutoringEdit(tutoring));
            }
            else
            {
                await DisplayAlert("Denied", "You don't have permission to access", "OK");
            }
        }

        async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            String Key = ((TappedEventArgs)e).Parameter.ToString();
            Tutoring tutoring = await App.TutoringDatabase.GetOneTutoring(Key);
            if (tutoring != null && tutoring.Tutor == (App.User.FirstName + " " + App.User.LastName))
            {
                await App.CourseDatabase.DeleteCourse(Key);
                await DisplayAlert("Success", "Course session for Deleted= " + tutoring.CourseName, "OK");
            }
            else
            {
                await DisplayAlert("Denied", "You don't have permission to access", "OK");
            }
            OnAppearing();
        }

        async void GradeTap_Tapped(object sender, EventArgs e)
        {
            string grade = await DisplayPromptAsync("Tutoring Session Grading", "Enter the grade for this tutoring session");
            String Key = ((TappedEventArgs)e).Parameter.ToString();
            Tutoring tutoring = await App.TutoringDatabase.GetOneTutoring(Key);
            if (tutoring != null && grade != null)
            {
                await App.TutoringDatabase.UpdateTutoring(new Tutoring
                {
                    Key = tutoring.Key,
                    TutoringID = tutoring.TutoringID,
                    CourseName = tutoring.CourseName,
                    CourseSection = tutoring.CourseSection,
                    CourseSemester = tutoring.CourseSemester,
                    CourseTeacher = tutoring.CourseTeacher,
                    Tutor = tutoring.Tutor,
                    TutoringTime = tutoring.TutoringTime,
                    TutoringSessionTime = tutoring.TutoringSessionTime,
                    Grade = grade
                });
                await DisplayAlert("Update Result", "Success", "OK");
            }
            else
            {
                await DisplayAlert("Denied", "You are missing information", "OK");
            }
            OnAppearing();
        }
        async void Delete1Tap_Tapped(object sender, EventArgs e)
        {
            String Key = ((TappedEventArgs)e).Parameter.ToString();
            RegisteredTutoring registeredTutoring = await App.RegisteredTutoringDatabase.GetTutorings(App.User.Key);
            Tutoring tutoring = await App.TutoringDatabase.GetOneTutoring(Key);
            List<Tutoring> list = new List<Tutoring>();
            for (int r = 0; r < registeredTutoring.TutoringSession.Length; r++)
            {
                list.Add(registeredTutoring.TutoringSession[r]);
            }
            list.Remove(tutoring);
            await App.RegisteredTutoringDatabase.UpdateTutoring(new RegisteredTutoring
            {
                Key = registeredTutoring.Key,
                StudentID = registeredTutoring.StudentID,
                TutoringSession = list.ToArray()
            });
            await DisplayAlert("Success", "Tutoring session for Deleted= " + tutoring.CourseName, "OK");
            OnAppearing();
        }
        async void SubTap_Tapped(object sender, EventArgs e)
        {
            String Key = ((TappedEventArgs)e).Parameter.ToString();
            RegisteredTutoring registeredTutoring = await App.RegisteredTutoringDatabase.GetTutorings(App.User.Key);
            Tutoring tutoring = await App.TutoringDatabase.GetOneTutoring(Key);
            if(registeredTutoring != null)
            {
                List<Tutoring> list = new List<Tutoring>();
                for (int r = 0; r < registeredTutoring.TutoringSession.Length; r++)
                {
                    list.Add(registeredTutoring.TutoringSession[r]);
                }
                list.Add(tutoring);
                await App.RegisteredTutoringDatabase.UpdateTutoring(new RegisteredTutoring
                {
                    Key = registeredTutoring.Key,
                    StudentID = registeredTutoring.StudentID,
                    TutoringSession = list.ToArray()
                });
            }
            else
            {
                List<Tutoring> list = new List<Tutoring>();
                list.Add(tutoring);
                await App.RegisteredTutoringDatabase.SaveRegisteredTutoring(new RegisteredTutoring
                {
                    StudentID = App.User.Key,
                    TutoringSession = list.ToArray()
                });
            }
            await DisplayAlert("Success", "Tutoring session for Deleted= " + tutoring.CourseName, "OK");
            OnAppearing();
        }

        async void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Tutoring tutoring = new Tutoring();
            tutoring.CourseName = "empty";
            if (App.User.Type == "tutor")
            {
                await Navigation.PushAsync(new TutoringEdit(tutoring));
            }
            else
            {
                await DisplayAlert("Denied", "You don't have permission to access", "OK");
            }
        }
    }
}