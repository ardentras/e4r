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
    [Activity(Label = "QuestionDificultypageActivity")]
    public class QuestionDificultypageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionDifficultypage);

            ImageButton continueButton = FindViewById<ImageButton>(Resource.Id.continueButton);
            continueButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionspageActivity));
                StartActivity(intent);
            };
        }

    }
}