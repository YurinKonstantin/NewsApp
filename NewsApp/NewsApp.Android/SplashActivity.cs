using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Threading.Tasks;
using Android.Util;
using Android.Content;

namespace NewsApp.Droid
{
    [Activity(Label = "Новости", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
           base.OnCreate(savedInstanceState, persistentState);
     
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
             //Task startupWork = new Task(() => { SimulateStartup(); });
            //startupWork.Start();
         SimulateStartup();
        }

        // Simulates background work that happens behind the splash screen
         void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
           // await Task.Delay(5000); // Simulate a bit of startup work.
           Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}