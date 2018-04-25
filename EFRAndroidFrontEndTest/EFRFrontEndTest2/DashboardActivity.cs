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
using EFRFrontEndTest2.Assets.DynamicSize;

namespace EFRFrontEndTest2
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            setBackground();
        }
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
            dynamicSizeRendering();
            getButtons();
            InitializeUserBasicInformation();
            InitializeClickHandlers();
        }
        private void dynamicSizeRendering() {
            DynamicSize Device = new DynamicSize(Resources);
            LinearLayout user_info_container = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            LinearLayout control_btns = FindViewById<LinearLayout>(Resource.Id.control_btns);
            TextView welcome = FindViewById<TextView>(Resource.Id.textView1);
            TextView user_fname = FindViewById<TextView>(Resource.Id.user_fname);
            TextView donated_text = FindViewById<TextView>(Resource.Id.donated_text);
            TextView solved_text = FindViewById<TextView>(Resource.Id.solved_text);
            TextView level_text = FindViewById<TextView>(Resource.Id.level_text);
            TextView user_level = FindViewById<TextView>(Resource.Id.user_level);
            TextView total_donated = FindViewById<TextView>(Resource.Id.total_donated);
            TextView total_solved = FindViewById<TextView>(Resource.Id.total_solved);

            control_btns.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent)
            {
                TopMargin = Device.DpHeight(0.1)
            };
            user_info_container.LayoutParameters = new LinearLayout.LayoutParams(Device.PxWidth(0.6), Device.PxHeight(0.3));
            user_fname.SetPadding(0, Device.DpHeight(0.015), 0, Device.DpHeight(0.02));
            user_fname.TextSize = Device.DpHeight(0.035);
            user_level.TextSize = Device.DpHeight(0.03);
            total_solved.TextSize = Device.DpHeight(0.03);
            total_donated.TextSize = Device.DpHeight(0.03);
            donated_text.TextSize = Device.DpHeight(0.020);
            level_text.TextSize = Device.DpHeight(0.020);
            solved_text.TextSize = Device.DpHeight(0.020);
            welcome.TextSize = Device.DpHeight(0.020);
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

            user_fname.Text = (uo.FirstName.Length > 0 ? uo.FirstName : uo.Username);
            user_initials.Text = (new StringBuilder().Append((uo.FirstName.Length > 0 ? uo.FirstName[0] : 'N')).Append(".").Append((uo.LastName.Length > 0 ? uo.LastName[0] : 'U')).ToString());
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
                //please add api call to logout and check if success
                StartActivity(typeof(LoginScreenActivity));
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

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = FindViewById<GridLayout>(Resource.Id.settingsgrid);
                background.Background = AppBackground.background;
            }
        }
    }
}