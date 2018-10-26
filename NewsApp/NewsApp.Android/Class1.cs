using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using NewsApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeech_Android))]
namespace NewsApp.Droid
{
    public class TextToSpeech_Android : Java.Lang.Object, Interface1, TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;

        public void Speak(string text)
        {
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);
            }
            else
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
              //  Debug.WriteLine("spoke " + toSpeak);
            }
        }
        public void LongAlert(string message)
        {
            Toast f = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            f.SetGravity(Gravity.GetAbsoluteGravity(GravityFlags.Center, GravityFlags.Center), 0, 0);
            f.Show();
        }

        public void ShortAlert(string message)
        {
            // Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
            Toast f = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short);
            f.SetGravity(Gravity.GetAbsoluteGravity(GravityFlags.Center, GravityFlags.Center), 0, 0);
            f.Show();
         

        }
        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
              //  Debug.WriteLine("speaker init");
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
            else
            {
              //  Debug.WriteLine("was quiet");
            }
        }
        #endregion
    }
}