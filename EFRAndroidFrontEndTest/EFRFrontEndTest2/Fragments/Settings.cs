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
    public class Settings : Android.Support.V4.App.Fragment
    {
        private EFRFrontEndTest2.BottomMenuTest _main;
        private UserObject uo = SingleUserObject.getObject();
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
            TextView initials = view.FindViewById<TextView>(Resource.Id.initials);
            TextView backgroundSelection = view.FindViewById<TextView>(Resource.Id.Color_Picker);


            if (uo.FirstName != "" && uo.LastName != "")
            {
                initials.Text = new StringBuilder(uo.FirstName[0].ToString().ToUpper()).Append('.').Append(uo.LastName[0].ToString().ToUpper()).ToString();
            }
            else if (uo.FirstName != "")
            {
                initials.Text = uo.FirstName;
            }
            else if (uo.LastName != "")
            {
                initials.Text = uo.LastName;
            }
            else
            {
                initials.Text = "New User";
            }
            

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
                AlertDialog.Builder dialog = new AlertDialog.Builder(_main);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Logout");
                alert.SetMessage("Are you sure you want to log out?");
                alert.SetButton("YES", (c, ev) =>
                {
                    _main.LogOut();
                });
                alert.Show();
            };
            backgroundSelection.Click += delegate 
            {
                _main.LoadFragment(backgroundSelection.Id);
            };

            return view;
        }
    }
}