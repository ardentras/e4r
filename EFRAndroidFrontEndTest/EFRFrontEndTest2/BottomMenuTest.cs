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


/*
 * NOTE:  This new UI uses fragments, which means we don't need activities no more 
 *        If you don't know how to make fragments, go to the Fragments folder
 *        and just follow the guidelines on Home.cs
 *        
 * NOTE:  If the page has sub pages, make sure you pass an instance of this
 *        so it can access the LoadFragment(int id) function
 *        
 * NOTE:  If you edit something and it won't build, its because you need to clean
 *        the solution first, if clean fails, clean a few more times to see if
 *        it succeeds, if it does, then you can build again
 */

namespace EFRFrontEndTest2
{
    [Activity(Label = "Main")]
    public class BottomMenuTest : AppCompatActivity
    {
        //this is our bottom navigation
        private BottomNavigationView bottomNavigation;
        //this is use to keep track of the previous fragment
        //for back press
        private Android.Support.V4.App.Fragment previous;
        //Main function, called on run
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.bottomMenu);
            BottomNavigationView test = FindViewById<BottomNavigationView>(Resource.Id.bottom_nav);
            ShiftMode.SetShiftMode(test, false, false);
            //this is needed to display the home page
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, Home.NewInstance())
                .Commit();
            //add the handler so it knows where to go base on id of the tab
            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_nav);
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        public void LogOut()
        {
            //changed into dashboard activity for new userdashboard, only test
            var intent = new Intent(this, typeof(LoginScreenActivity));
            StartActivity(intent);
            //finish will destory this page
            Finish();
        }
        public void LoadFragment(int id)
        {
            //this checks for the id of the selection
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.EasyButton:
                case Resource.Id.NormalButton:
                case Resource.Id.HardButton:
                case Resource.Id.HardestButton:
                    fragment = Questions.NewInstance();
                    break;
                case Resource.Id.math_button:
                case Resource.Id.history_button:
                case Resource.Id.chemistry_button:
                case Resource.Id.biology_button:
                case Resource.Id.physics_button:
                    fragment = Difficulty.NewInstance(this);
                    break;
                case Resource.Id.action_home:
                    fragment = Home.NewInstance();
                    break;
                case Resource.Id.action_solve:
                    //passing this, because it needs to access LoadFragment(int id)
                    //only if this page has sub routes tho
                    fragment = Solve.NewInstance(this);
                    break;
                case Resource.Id.action_feed:
                    fragment = Feeds.NewInstance();
                    break;
                case Resource.Id.action_setting:
                    fragment = Settings.NewInstance(this);
                    break;
                case Resource.Id.account_settings:
                    fragment = AccountSettings.NewInstance();
                    break;
                case Resource.Id.general_settings:
                    fragment = GeneralSettings.NewInstance();
                    break;
                case Resource.Id.charity_selection:
                    fragment = CharitySelection.NewInstance(this);
                    break;
            }

            if (fragment == null)
                return;
            //if the selection is not the main navigators,
            //push it to stack so we can back button out of it
            if (id != Resource.Id.action_home &&
                id != Resource.Id.action_solve &&
                id != Resource.Id.action_feed &&
                id != Resource.Id.action_setting)
            {
                SupportFragmentManager.BeginTransaction()
                    .SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out)
                    .Replace(Resource.Id.content_frame, fragment)
                    .AddToBackStack(previous.Class.Name)
                    .Commit();
            }
            //if it is the main navigators
            //just simple replace the view
            else
            {
                if (SupportFragmentManager.BackStackEntryCount > 0)
                {
                    for(int i = 0; i < SupportFragmentManager.BackStackEntryCount; ++i)
                    {
                        SupportFragmentManager.PopBackStack();
                    }
                }
                SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
            }
            //keep track of previous fragment
            previous = fragment;
        }
        public override void OnBackPressed()
        {
            //if anything is on the stack, just back out
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
            }
            ////if nothing is on stack, dont do anything
            //else
            //{
            //    return;
            //}
        }
    }
}
