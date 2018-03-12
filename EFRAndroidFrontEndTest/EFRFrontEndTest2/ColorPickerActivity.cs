using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "ColorPickerActivity")]
    public class ColorPickerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ColorPicker);

            Button blue = FindViewById<Button>(Resource.Id.setblue);
            Button red = FindViewById<Button>(Resource.Id.setred);
            Button green = FindViewById<Button>(Resource.Id.setgreen);
            Button gray = FindViewById<Button>(Resource.Id.setgray);
            Button darkblue = FindViewById<Button>(Resource.Id.setdarkblue);
            Button darkred = FindViewById<Button>(Resource.Id.setdarkred);
            Button darkgreen = FindViewById<Button>(Resource.Id.setdarkgreen);
            Button purple = FindViewById<Button>(Resource.Id.setpurple);
            

            blue.Click += (sender, e) =>
            {
                AppBackground.background = blue.Background;
            };
            red.Click += (sender, e) =>
            {
                AppBackground.background = red.Background;
            };
            green.Click += (sender, e) =>
            {
                AppBackground.background = green.Background;
            };
            gray.Click += (sender, e) =>
            {
                AppBackground.background = gray.Background;
            };
            purple.Click += (sender, e) =>
            {
                AppBackground.background = purple.Background;
            };
            darkblue.Click += (sender, e) =>
            {
                AppBackground.background = darkblue.Background;
            };
            darkred.Click += (sender, e) =>
            {
                AppBackground.background = darkred.Background;
            };
            darkgreen.Click += (sender, e) =>
            {
                AppBackground.background = darkgreen.Background;
            };
        }

    }
}