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

            ImageButton imageButton6 = FindViewById<ImageButton>(Resource.Id.imageButton6);

            imageButton6.Click += (sender, e) =>
            {
                Finish();
            };
        }
    }
}

// NOTE FOR KELCEY
// This code is how you can pull what subjects the user chose in the subject screen

// int stuff = Intent.GetIntExtra("subjects", 0);


// I did the format this way to allow for expandability as well as processor efficiency.
// Check 'SelectSubjectScreen' for a bit of documentation at the top. But here are some examples.

// 10010 = Physics and Math subjects are selected
// 11111 = All 5 subjects are selected
// 01101 = Chemistry, biology, and history subjects are selected
// 00100 = Only biology was selected
// 00000 = No subjects were selected

// Shoot me a text if you have any questions! :-)