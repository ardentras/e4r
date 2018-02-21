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
using EFRFrontEndTest2.Assets;

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
            ImageButton EasyButton = FindViewById<ImageButton>(Resource.Id.EasyButton);
            ImageButton NormalButton = FindViewById<ImageButton>(Resource.Id.EasyButton);
            ImageButton HardButton = FindViewById<ImageButton>(Resource.Id.EasyButton);
            ImageButton HardestButton = FindViewById<ImageButton>(Resource.Id.EasyButton);
            ImageButton BackButton = FindViewById<ImageButton>(Resource.Id.backButton);

            continueButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionspageActivity));
                StartActivity(intent);
            };

            EasyButton.Click += (sender, e) =>
            {
                UserObject var = SingleUserObject.getObject();
                var.Difficulty = "1";

            };

            NormalButton.Click += (sender, e) =>
            {
                UserObject var = SingleUserObject.getObject();
                var.Difficulty = "2";

            };

            HardButton.Click += (sender, e) =>
            {
                UserObject var = SingleUserObject.getObject();
                var.Difficulty = "3";
            };

            HardestButton.Click += (sender, e) =>
            {
                UserObject var = SingleUserObject.getObject();
                var.Difficulty = "4";
            };
            BackButton.Click += (sender, e) =>
            {
                Finish();
            };


        }

    }
}