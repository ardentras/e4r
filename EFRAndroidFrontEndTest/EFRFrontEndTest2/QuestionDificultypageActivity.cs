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
            setBackground();
            ImageButton continueButton = FindViewById<ImageButton>(Resource.Id.continueButton);
            ImageButton EasyButton = FindViewById<ImageButton>(Resource.Id.EasyButton);
            ImageButton NormalButton = FindViewById<ImageButton>(Resource.Id.NormalButton);
            ImageButton HardButton = FindViewById<ImageButton>(Resource.Id.HardButton);
            ImageButton HardestButton = FindViewById<ImageButton>(Resource.Id.HardestButton);
            ImageButton BackButton = FindViewById<ImageButton>(Resource.Id.backButton);
            UserObject user = SingleUserObject.getObject();
            int difficulty = user.Difficulty;

            continueButton.Click += (sender, e) =>
            {
                user.Difficulty = difficulty;
                var intent = new Intent(this, typeof(QuestionspageActivity));
                StartActivity(intent);
            };

            EasyButton.Click += (sender, e) =>
            {
                difficulty = 0;
            };

            NormalButton.Click += (sender, e) =>
            {
                difficulty = 0;
            };

            HardButton.Click += (sender, e) =>
            {
                difficulty = 0;
            };

            HardestButton.Click += (sender, e) =>
            {
                difficulty = 0;
            };
            BackButton.Click += (sender, e) =>
            {
                user.Difficulty = difficulty;
                Finish();
            };
        }
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = FindViewById<GridLayout>(Resource.Id.gridLayout1);
                background.Background = AppBackground.background;
            }
        }
    }
}