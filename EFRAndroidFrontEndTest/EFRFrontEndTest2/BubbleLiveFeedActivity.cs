using System;
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
            bubble.Click +=  (sender, e) =>
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
}