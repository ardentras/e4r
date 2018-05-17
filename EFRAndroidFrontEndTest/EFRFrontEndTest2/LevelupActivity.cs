using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Timers;
using EFRFrontEndTest2.Assets;

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
            UserObject user = SingleUserObject.getObject();
            string text = user.Level.ToString();
            lvText.Text = "You are now level" + text + "!"; 
            var searchTimer = new Timer(600);
            searchTimer.Elapsed += delegate
            {
                Finish();
            };
        }
    }
}