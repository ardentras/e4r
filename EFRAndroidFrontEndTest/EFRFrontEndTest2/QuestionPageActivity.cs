using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EFRFrontEndTest2
{
    [Activity(Label = "QuestionPageActivity")]
    public class QuestionPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += (o, e) => {
                Toast.MakeText(this, "Beep Boop", ToastLength.Short).Show();
            };

        }
    }
}