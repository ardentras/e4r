using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Threading.Tasks;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

using EFRFrontEndTest2.Assets;
using EFRFrontEndTest2.Assets.BottomNavagation;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using EFRFrontEndTest2.Fragments;

namespace EFRFrontEndTest2
{
    [Activity(Label = "Main")]
    public class BottomMenuTest : AppCompatActivity
    {
        private BottomNavigationView bottomNavigation;
        //Main function, called on run
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.bottomMenu);
            BottomNavigationView test = FindViewById<BottomNavigationView>(Resource.Id.bottom_nav);
            ShiftMode.SetShiftMode(test, false, false);
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, Home.NewInstance())
                .Commit();
            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_nav);
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.action_home:
                    fragment = Home.NewInstance();
                    break;
                case Resource.Id.action_solve:
                    fragment = Solve.NewInstance();
                    break;
                case Resource.Id.action_feed:
                    fragment = Feeds.NewInstance();
                    break;
                case Resource.Id.action_setting:
                    fragment = Settings.NewInstance();
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
        public override void OnBackPressed()
        {
            return;
        }
    }
}


// Testing

//Great reference for calling event function out of main.
//SetContentView doesnt give a transition animation

/*createAccount.Click += OnTapGestureRecognizerTapped;
private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
{
    SetContentView(Resource.Layout.CreateAccountScreen);
}*/
