<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EFRFrontEndTest2
{
    [Activity(Label = "BubbleLiveFeedActivity")]
    public class BubbleLiveFeedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Removes title bar
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BubbleLiveFeed);
            Button bubble = FindViewById<Button>(Resource.Id.bigbubble);
            bubble.Click += async (sender, e) =>
            {
                LinearLayout layoutBase = FindViewById<LinearLayout>(Resource.Id.bubble_layout);
                ImageView img = new ImageView(this);
                img.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
                img.Visibility = ViewStates.Visible;
                img.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.Bubble));
                Random rnd = new Random();
                int x = rnd.Next(1, 101);
                layoutBase.AddView(img,x,100);
                ValueAnimator animator = ValueAnimator.OfInt(100, 10);
                animator.SetDuration(3000);
                animator.Start();
                animator.Update += (object sender2, ValueAnimator.AnimatorUpdateEventArgs f) =>
                {
                    int newValue = (int)f.Animation.AnimatedValue;
                    // Apply this new value to the object being animated.
                    img.TranslationY = newValue;
                };
            };
        }


    };
=======
﻿using Android.App;
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

>>>>>>> 0b8aa8398c4a62d5478089e2cd442602fd9e574a
}