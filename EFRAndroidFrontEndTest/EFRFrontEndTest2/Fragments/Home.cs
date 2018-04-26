using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class Home : Android.Support.V4.App.Fragment
    {
        private UserObject uo = SingleUserObject.getObject();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Do everything else here that's not UI related
            // Create your fragment here
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
            View view = inflater.Inflate(Resource.Layout.Home, container, false);
            TextView uid_txt = view.FindViewById<TextView>(Resource.Id.uid_txt);
            TextView level_txt = view.FindViewById<TextView>(Resource.Id.level_txt);
            TextView solve_txt = view.FindViewById<TextView>(Resource.Id.solved_val);
            TextView donate_txt = view.FindViewById<TextView>(Resource.Id.donate_val);
            uid_txt.Text = uo.FirstName;
            level_txt.Text = (uo.Difficulty + 1).ToString();
            solve_txt.Text = uo.TotalQuestions.ToString();
            donate_txt.Text = uo.TotalDonated.ToString();
            return view;
        }
    }
}