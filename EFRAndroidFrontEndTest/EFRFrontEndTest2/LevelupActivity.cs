using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Timers;
namespace EFRFrontEndTest2
{
    [Activity(Label = "LevelupActivity")]
    public class LevelupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LevelUp);

            TextView lvText = FindViewById<TextView>(Resource.Id.lvLabel);
            UserObject obj = SingleUserObject.getObject();
            string text = Convert.ToString((int)(Math.Sqrt(obj.QuestionsAnswered / 10) + obj.MoneyEarned / 50));
            lvText.Text = "You are now level" + text; 
            var searchTimer = new Timer(600);
            searchTimer.Elapsed += delegate
            {
                Finish();
            };

        }
    }
}