using Android.App;
<<<<<<< HEAD
using Android.Content;
=======
>>>>>>> 0b8aa8398c4a62d5478089e2cd442602fd9e574a
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
<<<<<<< HEAD
            SetContentView(Resource.Layout.SelectSubjectScreen);

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);

            backButton.Click += (sender, e) =>
            {
                Finish();
            };
=======
            SetContentView(Resource.Layout.HomeScreen);
>>>>>>> 0b8aa8398c4a62d5478089e2cd442602fd9e574a

        }
    }
}