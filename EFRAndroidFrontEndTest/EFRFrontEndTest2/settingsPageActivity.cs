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

namespace EFRFrontEndTest2
{
    [Activity(Label = "settingsPageActivity")]
    public class settingsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingsPage);
            SeekBar sound = FindViewById<SeekBar>(Resource.Id.sound);
            sound.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                
            };

        }
    };
}