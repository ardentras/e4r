using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFRFrontEndTest2
{
    [Activity(Label = "HomeScreenActivity")]
    public class UserDashboardActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            setBackground();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserDashboardScreen);
            setBackground();

            ImageButton settingsButton = FindViewById<ImageButton>(Resource.Id.settingsButton);
            ImageButton charityButton = FindViewById<ImageButton>(Resource.Id.charityButton);
            Button playButton = FindViewById<Button>(Resource.Id.playButton);
            Button bubbleLiveFeedButton = FindViewById<Button>(Resource.Id.bubbleLiveFeedButton);

            settingsButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(settingsPageActivity));
                StartActivity(intent);
            };

            charityButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CharitySelectionScreenActivity));
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

        /***************************************************************************************************************************
         * Author: Kevin Xu - if you change anything, update this!!!
         * Function: OnBackPressed
         * Purpose: Prevent going back to login page.(should not happen!)
        ****************************************************************************************************************************/
        public override void OnBackPressed()
        {
            
        }

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = FindViewById<GridLayout>(Resource.Id.dashboardgrid);
                background.Background = AppBackground.background; ;
            }
        }
    }
}