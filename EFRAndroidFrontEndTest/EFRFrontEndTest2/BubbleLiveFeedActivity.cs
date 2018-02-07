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
            bubble.Click += (sender, e) =>
            {
                LinearLayout layoutBase = FindViewById<LinearLayout>(Resource.Id.bubble_layout);
                ImageView img = new ImageView(this);
                Button bubbleButton = FindViewById<Button>(Resource.Id.bigbubble);
                img.LayoutParameters = new LinearLayout.LayoutParams(width: ViewGroup.LayoutParams.FillParent, height: ViewGroup.LayoutParams.FillParent);
                img.Visibility = ViewStates.Visible;
                img.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.Bubble));
                layoutBase.AddView(img,100,100);
                Random rnd = new Random();
                img.SetX(rnd.Next(0, 1000));
                ValueAnimator animator = ValueAnimator.OfInt(0, -1100);
                animator.SetDuration(1500);
                animator.Start();
                animator.Update += (object sender2, ValueAnimator.AnimatorUpdateEventArgs f) =>
                {
                    
                    int newValue = (int)f.Animation.AnimatedValue;
                    // Apply this new value to the object being animated.
                    img.TranslationY = newValue;
                    if(newValue >= -900)
                    img.TranslationX += 10*(float)Math.Sin(newValue/100);
                    else
                    {
                        if (img.TranslationX > 500)
                            img.TranslationX -= 3;
                        else
                            img.TranslationX += 3;
                    }
                    if (1 == f.Animation.AnimatedFraction)
                    {
                        layoutBase.RemoveView(img);
                        string newval  = '$' + Convert.ToString(double.Parse(bubbleButton.Text.Remove(0, 1)) + 0.01);
                        if (newval.IndexOf('.') - newval.Length == -2)
                            newval += '0';
                        bubbleButton.Text = newval;
                        

                    }
                };


            };
        }//
        


    };
}