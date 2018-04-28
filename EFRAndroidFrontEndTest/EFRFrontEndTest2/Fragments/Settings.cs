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

namespace EFRFrontEndTest2.Fragments
{
    public class Settings : Android.Support.V4.App.Fragment
    {
        private EFRFrontEndTest2.BottomMenuTest _main;
        public Settings(EFRFrontEndTest2.BottomMenuTest main)
        {
            _main = main;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static Settings NewInstance(EFRFrontEndTest2.BottomMenuTest main)
        {
            Settings temp = new Settings(main);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.Settings, container, false);
            TextView accountSettings = view.FindViewById<TextView>(Resource.Id.account_settings);
            TextView generalSettings = view.FindViewById<TextView>(Resource.Id.general_settings);
            TextView charitySelection = view.FindViewById<TextView>(Resource.Id.charity_selection);
            TextView logoutBTN = view.FindViewById<TextView>(Resource.Id.logout);

            accountSettings.Click += delegate {
                _main.LoadFragment(accountSettings.Id);
            };
            generalSettings.Click += delegate {
                _main.LoadFragment(generalSettings.Id);
            };
            charitySelection.Click += delegate {
                _main.LoadFragment(charitySelection.Id);
            };
            logoutBTN.Click += delegate
            {
                _main.LogOut();
            };

            return view;
        }
    }
}