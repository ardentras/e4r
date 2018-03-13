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
                setBackgrounds();
            };
            red.Click += (sender, e) =>
            {
                AppBackground.background = red.Background;
                setBackgrounds();
            };
            green.Click += (sender, e) =>
            {
                AppBackground.background = green.Background;
                setBackgrounds();
            };
            gray.Click += (sender, e) =>
            {
                AppBackground.background = gray.Background;
                setBackgrounds();
            };
            purple.Click += (sender, e) =>
            {
                AppBackground.background = purple.Background;
                setBackgrounds();
            };
            darkblue.Click += (sender, e) =>
            {
                AppBackground.background = darkblue.Background;
                setBackgrounds();
            };
            darkred.Click += (sender, e) =>
            {
                AppBackground.background = darkred.Background;
                setBackgrounds();
            };
            darkgreen.Click += (sender, e) =>
            {
                AppBackground.background = darkgreen.Background;
                setBackgrounds();
            };
        }
        protected void setBackgrounds()
        {
            GridLayout background = FindViewById<GridLayout>(Resource.Id.colorgrid);
            background.Background = AppBackground.background; ;
        }

    }
}