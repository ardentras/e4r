using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFRFrontEndTest2
{
    [Activity(Label = "HomeScreenActivity")]
    public class HomeScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomeScreen);
            ImageButton settingsButton = FindViewById<ImageButton>(Resource.Id.settingsButton);
            Button playButton = FindViewById<Button>(Resource.Id.playButton);
            Button bubbleLiveFeedButton = FindViewById<Button>(Resource.Id.bubbleLiveFeedButton);

            settingsButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(settingsPageActivity));
                StartActivity(intent);
            };

            playButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(SelectSubjectScreenActivity));
                StartActivity(intent);
            };

            bubbleLiveFeedButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(BubbleLiveFeedActivity));
                StartActivity(intent);
            };
        }
    }
}