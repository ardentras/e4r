﻿using System;
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
    public class Difficulty : Android.Support.V4.App.Fragment
    {
        private EFRFrontEndTest2.BottomMenuTest _main;
        public Difficulty(EFRFrontEndTest2.BottomMenuTest main)
        {
            _main = main;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public static Difficulty NewInstance(EFRFrontEndTest2.BottomMenuTest main)
        {
            Difficulty temp = new Difficulty(main);
            return temp;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.QuestionDifficultypage, container, false);
            ImageButton easy = view.FindViewById<ImageButton>(Resource.Id.EasyButton);
            easy.Click += delegate
            {
                _main.LoadFragment(easy.Id);
            };
            return view;
        }
    }
}