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
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        private TextView play_btn = null;
        private TextView charity_btn = null;
        private TextView feed_btn = null;
        private TextView setting_btn = null;
        private ImageButton logout_btn = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);
            getButtons();
            initializeClickHandlers();
        }
        private void getButtons()
        {
            play_btn = FindViewById<TextView>(Resource.Id.play_btn);
            charity_btn = FindViewById<TextView>(Resource.Id.charity_btn);
            feed_btn = FindViewById<TextView>(Resource.Id.feeds_btn);
            setting_btn = FindViewById<TextView>(Resource.Id.setting_btn);
            logout_btn = FindViewById<ImageButton>(Resource.Id.logout_btn);
        }
        private void initializeClickHandlers()
        {
            play_btn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SelectSubjectScreenActivity));
                StartActivity(intent);
            };
            charity_btn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(CharitySelectionScreenActivity));
                StartActivity(intent);
            };
            feed_btn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(BubbleLiveFeedActivity));
                StartActivity(intent);
            };
            setting_btn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(settingsPageActivity));
                StartActivity(intent);
            };
            logout_btn.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Logout");
                alert.SetMessage("Coming soon...");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            };
        }

        /***************************************************************************************************************************
         * Author: Kevin Xu - if you change anything, update this!!!
         * Function: OnBackPressed
         * Purpose: Prevent going back to login page.(should not happen!)
        ****************************************************************************************************************************/
        public override void OnBackPressed()
        {
            return;
        }
    }
}