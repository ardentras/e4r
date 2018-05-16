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
    public class GeneralSettings : Android.Support.V4.App.Fragment
    {
        View view = null;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnResume()
        {
            base.OnResume();
            setBackgrounds();
        }

        public static GeneralSettings NewInstance()
        {
            GeneralSettings temp = new GeneralSettings();
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.settingsPage, container, false);
            setBackgrounds();


            return view;
        }

        protected void setBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.bubble_layout);
                background.Background = AppBackground.background;
            }
        }
    }
}