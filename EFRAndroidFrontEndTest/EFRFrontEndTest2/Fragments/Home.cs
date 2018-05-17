using System.Text;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class Home : Android.Support.V4.App.Fragment
    {
        private UserObject user = SingleUserObject.getObject();
        private View view = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Do everything else here that's not UI related
            // Create your fragment here
        }

        public override void OnResume()
        {
            base.OnResume();
            setBackground();
        }

        public static Home NewInstance()
        {
            Home temp = new Home();

            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Use this to initialize all the click handlers and stuff
            //all UI related functionalities here
            view = inflater.Inflate(Resource.Layout.Home, container, false);
            setBackground();

            TextView user_fname = view.FindViewById<TextView>(Resource.Id.user_fname);
            TextView user_level = view.FindViewById<TextView>(Resource.Id.user_level);
            TextView total_solved = view.FindViewById<TextView>(Resource.Id.total_solved);
            TextView total_donated = view.FindViewById<TextView>(Resource.Id.total_donated);
            user_fname.Text = (user.FirstName.Length > 0 ? user.FirstName : user.Username);
            user_level.Text = user.Level.ToString();
            total_solved.Text = user.TotalQuestions.ToString();
            total_donated.Text = new StringBuilder().Append("$").Append(" ").Append(user.TotalDonated).ToString();

            return view;
        }

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.home_background);
                background.Background = AppBackground.background;
            }
        }
    }
}


// Code that hasnt been coppied over yet.
/*
using EFRFrontEndTest2.Assets.DynamicSize;

        protected override void OnResume()
        {
            base.OnResume();
            setBackground();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            dynamicSizeRendering();
        }
        private void dynamicSizeRendering()
        {
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
}
*/
