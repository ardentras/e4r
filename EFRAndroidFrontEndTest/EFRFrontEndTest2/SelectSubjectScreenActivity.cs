using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFRFrontEndTest2
{
    [Activity(Label = "SelectSubjectScreenActivity")]
    public class SelectSubjectScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectSubjectScreen);

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);

            backButton.Click += (sender, e) =>
            {
                Finish();
            };

        }
    }
}