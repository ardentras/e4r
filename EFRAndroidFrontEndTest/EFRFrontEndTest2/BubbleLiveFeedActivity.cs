using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace EFRFrontEndTest2
{
    [Activity(Label = "BubbleLiveFeedActivity")]
    public class BubbleLiveFeedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.);
        }

        BubbleButton.Click += (sender, e) =>
            {
                //Closes the current view
            }
    
    };
}

}