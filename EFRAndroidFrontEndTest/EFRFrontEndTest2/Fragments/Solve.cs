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
    public class Solve : Android.Support.V4.App.Fragment
    {
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
        public static Solve NewInstance(EFRFrontEndTest2.BottomMenuTest main)
        {
            Solve temp = new Solve(main);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.Solve, container, false);
            ImageButton Math = view.FindViewById<ImageButton>(Resource.Id.math_button);
            Math.Click += delegate
            {
                _main.LoadFragment(Math.Id);
            };
            return view;
        }
    }
}