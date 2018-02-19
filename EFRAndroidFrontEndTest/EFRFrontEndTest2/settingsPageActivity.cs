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
    [Activity(Label = "settingsPageActivity")]
    public class settingsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingsPage);
            SeekBar sound = FindViewById<SeekBar>(Resource.Id.sound);
            Button reset = FindViewById<Button>(Resource.Id.ResetButton);
            reset.Click += (sender, e) =>
            {
                UserObject obj = SingleUserObject.getObject();
                obj.CompletedBlocks = new int[0];
                obj.MoneyEarned = 0;
                obj.QuestionsAnswered = 0;
            };

        }
    };
}