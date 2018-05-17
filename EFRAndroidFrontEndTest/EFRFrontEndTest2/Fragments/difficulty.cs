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
    public class Difficulty : Android.Support.V4.App.Fragment
    {
        private BottomMenuTest _main;
        private UserObject user = SingleUserObject.getObject();
        private View view = null;
        public Difficulty(BottomMenuTest main)
        {
            _main = main;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static Difficulty NewInstance(BottomMenuTest main)
        {
            Difficulty temp = new Difficulty(main);
            return temp;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.QuestionDifficultypage, container, false);
            Button easyButton = view.FindViewById<Button>(Resource.Id.easyButton);
            Button mediumButton = view.FindViewById<Button>(Resource.Id.mediumButton);
            Button hardButton = view.FindViewById<Button>(Resource.Id.hardButton);
            easyButton.Click += delegate
            {
                user.Difficulty = 0;
                _main.LoadFragment(easyButton.Id); // Because any button will work
            };
            mediumButton.Click += delegate
            {
                user.Difficulty = 1;
                _main.LoadFragment(easyButton.Id); // Because any button will work
            };
            hardButton.Click += delegate
            {
                user.Difficulty = 2;
                _main.LoadFragment(easyButton.Id); // Because any button will work
            };
            return view;
        }
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = view.FindViewById<GridLayout>(Resource.Id.gridLayout1);
                background.Background = AppBackground.background;
            }
        }
    }
}


// Code that hasnt been coppied over yet.
/*
    protected override void OnCreate(Bundle savedInstanceState)
    {
        setBackground();
    }
*/
