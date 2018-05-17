using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

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
    public class Solve : Android.Support.V4.App.Fragment
    {
        private UserObject user = SingleUserObject.getObject();
        private View view = null;
        private RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider(); // So the garbage collector is called less often if a kid just LOVES tapping the shuffle button

        private EFRFrontEndTest2.BottomMenuTest _main;
        public Solve(EFRFrontEndTest2.BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnResume()
        {
            base.OnResume();
            setBackground();
        }

        public static Solve NewInstance(EFRFrontEndTest2.BottomMenuTest main)
        {
            Solve temp = new Solve(main);

            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Solve, container, false);
            setBackground();

            ImageButton Math = view.FindViewById<ImageButton>(Resource.Id.math_button);
            ImageButton History = view.FindViewById<ImageButton>(Resource.Id.history_button);
            ImageButton Science = view.FindViewById<ImageButton>(Resource.Id.science_button);
            ImageButton English = view.FindViewById<ImageButton>(Resource.Id.english_button);
            ImageButton Geography = view.FindViewById<ImageButton>(Resource.Id.geography_button);
            ImageButton General = view.FindViewById<ImageButton>(Resource.Id.general_button);

            Math.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Math.Id);
            };
            History.Click += delegate
            {
                user.SubjectID = 4;
                user.SubjectName = "History";
                _main.LoadFragment(History.Id);
            };
            Science.Click += delegate
            {
                user.SubjectID = 3;
                user.SubjectName = "Science";
                _main.LoadFragment(Science.Id);
            };
            English.Click += delegate
            {
                user.SubjectID = 2;
                user.SubjectName = "English";
                _main.LoadFragment(English.Id);
            };
            Geography.Click += delegate
            {
                user.SubjectID = 5;
                user.SubjectName = "Geography";
                _main.LoadFragment(Geography.Id);
            };
            General.Click += delegate
            {
                user.SubjectID = 6;
                user.SubjectName = "General";
                _main.LoadFragment(General.Id);
            };

            return view;
        }

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.subject_background);
                background.Background = AppBackground.background;
            }
        }
    }
}




// Code that hasnt been coppied over yet.
/*
        protected override void OnCreate(Bundle savedInstanceState)
        {
            setBackground();subject_background
        }


    }
}
*/
