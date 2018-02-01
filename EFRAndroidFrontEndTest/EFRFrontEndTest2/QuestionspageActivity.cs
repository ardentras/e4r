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
    [Activity(Label = "QuestionspageActivity")]
    public class QuestionspageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionsPage);

            ImageButton imageButton3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
            ImageButton imageButton6 = FindViewById<ImageButton>(Resource.Id.imageButton6);


            imageButton3.Click += OnTapGestureRecognizerTapped;

            /*imageButton3.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(WinPageActivity));
                StartActivity(intent);
            };*/

            imageButton6.Click += (sender, e) =>
            {
                    Finish();
            };
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.CreateAccountScreen);
        }
    }
}