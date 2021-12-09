using Xamarin.Forms;

namespace Champlain_Computer_Science_Tutoring
{
    public partial class App : Application
    {
        public static DBUser Database = new DBUser();
        public static DBCourse CourseDatabase = new DBCourse();
        public static User User { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
