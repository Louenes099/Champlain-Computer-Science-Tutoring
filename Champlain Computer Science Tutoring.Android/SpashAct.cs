using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Threading;

namespace Champlain_Computer_Science_Tutoring.Droid
{
    [Activity(Label = "Champlain_Computer_Science_Tutoring", Icon = "@mipmap/icon", NoHistory = true,
    Theme = "@style/MyTheme.Splash", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize |
    ConfigChanges.Orientation | ConfigChanges.UiMode |
    ConfigChanges.ScreenLayout |
    ConfigChanges.SmallestScreenSize)]
    public class SpashAct : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Thread.Sleep(3000);
            Finish();
        }
        public override void OnBackPressed()
        {
            //base.OnBackPressed();
        }

    }
}