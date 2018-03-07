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
using EFRFrontEndTest2.Assets;

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
        private UserObject uo = SingleUserObject.getObject();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);
            getButtons();
            InitializeUserBasicInformation();
            InitializeClickHandlers();
        }
        private void getButtons()
        {
            play_btn = FindViewById<TextView>(Resource.Id.play_btn);
            charity_btn = FindViewById<TextView>(Resource.Id.charity_btn);
            feed_btn = FindViewById<TextView>(Resource.Id.feeds_btn);
            setting_btn = FindViewById<TextView>(Resource.Id.setting_btn);
            logout_btn = FindViewById<ImageButton>(Resource.Id.logout_btn);
        }
        private void InitializeUserBasicInformation() {
            TextView user_fname = FindViewById<TextView>(Resource.Id.user_fname);
            TextView user_initials = FindViewById<TextView>(Resource.Id.user_initials);
            TextView user_donated = FindViewById<TextView>(Resource.Id.total_donated);
            TextView user_solved = FindViewById<TextView>(Resource.Id.total_solved);
            TextView user_level = FindViewById<TextView>(Resource.Id.user_level);

            user_fname.Text = uo.FirstName;
            user_initials.Text = new StringBuilder().Append(uo.FirstName[0]).Append(".").Append(uo.LastName[0]).ToString();
            user_donated.Text = new StringBuilder().Append("$").Append(" ").Append(uo.TotalDonated).ToString();
            user_solved.Text = uo.TotalQuestions.ToString();
            user_level.Text = uo.Difficulty.ToString();
        }
        private void InitializeClickHandlers()
        {
            play_btn.Click += delegate
            {
                StartActivity(typeof(SelectSubjectScreenActivity));
            };
            charity_btn.Click += delegate
            {
                StartActivity(typeof(CharitySelectionScreenActivity));
            };
            feed_btn.Click += delegate
            {
                StartActivity(typeof(BubbleLiveFeedActivity));
            };
            setting_btn.Click += delegate
            {
                StartActivity(typeof(settingsPageActivity));
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