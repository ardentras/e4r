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
    [Activity(Label = "CharitySelectionScreenActivity")]
    public class CharitySelectionScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CharitySelectionScreen);

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.back_arrow);
            LinearLayout charities_list = FindViewById<LinearLayout>(Resource.Id.charities_list);

            TextView newt = new TextView(this){Text="Test"};

            charities_list.AddView(newt);

            backButton.Click += delegate {
                base.OnBackPressed();
            };
        }
    }
}