using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            Task.Run(async () => { await CallDatabase(); });
            //Removes title bar
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BubbleLiveFeed);
            Button bubble = FindViewById<Button>(Resource.Id.bigbubble);
            bubble.Click += (sender, e) =>
            {
                AbsoluteLayout layoutBase = FindViewById<AbsoluteLayout>(Resource.Id.bubble_layout);
                ImageView img = new ImageView(this);
                Button bubbleButton = FindViewById<Button>(Resource.Id.bigbubble);
                //create new bubble image
                img.LayoutParameters = new LinearLayout.LayoutParams(width: ViewGroup.LayoutParams.MatchParent, height: ViewGroup.LayoutParams.MatchParent);
                img.Visibility = ViewStates.Visible;
                img.SetImageResource(Resource.Drawable.Bubble);
                var metrics = Resources.DisplayMetrics;
                //place the bubble on screen randomizing it's x pos
                layoutBase.AddView(img,100,100);
                Random rnd = new Random();
                img.SetX(rnd.Next(0, metrics.WidthPixels));
                img.SetY(metrics.HeightPixels);
                // create an animator to move the bubble

                ValueAnimator animator = ValueAnimator.OfInt(metrics.HeightPixels, metrics.HeightPixels/11);
                animator.SetDuration(1500);
                animator.Start();
                /* 
                 * function to move bubble
                 * called each frame the animation occurs
                 * and moved the bubble upward and in a sine wave
                 */
                animator.Update += (object sender2, ValueAnimator.AnimatorUpdateEventArgs f) =>
                {
                    //animation value
                    int newValue = (int)f.Animation.AnimatedValue;
                    // Apply this new value to the object being animated.
                    img.TranslationY = newValue;
                    //move the bubble in a sine wave pattern
                    if(newValue >= metrics.HeightPixels / 9)
                    img.TranslationX += 10*(float)Math.Sin(newValue/100);
                    //if near the top slide the bubble into the big bubble at the top
                    else
                    {
                        if (img.TranslationX > 500)
                            img.TranslationX -= 3;
                        else
                            img.TranslationX += 3;
                    }
                    //remove the bubble and increment the counter
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
        public async Task<bool> CallDatabase()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://34.216.143.255:3002/api/bubble_feed"));
            request.ContentType = "application/json";
            request.Method = "Get";
            bool spawn = true;
            while (spawn)
            {
                JsonArray prevdata = null;
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = JsonObject.Load(stream);
                        JsonArray data = (JsonArray)jsonDoc["data"];
                        if (prevdata != data)
                        {
                            if (((JsonValue)data[0])["donated"] != ((JsonValue)prevdata[0])["donated"])
                            {
                                for (int x = 0; x < data.Count; x++)
                                {
                                    spawnBubble(((JsonValue)data[x])["donated"]);
                                    Thread.Sleep(100);
                                }
                            }
                            else
                            {
                                int y;
                                for (int x = 0; x < data.Count && x < prevdata.Count; x++)
                                {
                                    if (((JsonValue)data[x])["donated"] != ((JsonValue)prevdata[x])["donated"])
                                    {
                                        spawnBubble(((JsonValue)data[x])["donated"]);
                                        Thread.Sleep(100);
                                    }
                                }
                                for (y =0; y < data.Count; y++)
                                {
                                    spawnBubble(((JsonValue)data[y])["donated"]);
                                    Thread.Sleep(100);
                                }
                            }
                            prevdata = data;
                        }
                    }
                }
                Thread.Sleep(10000);
            }
            return true;
        }
        public void spawnBubble(int spawn)
        {
            AbsoluteLayout layoutBase = FindViewById<AbsoluteLayout>(Resource.Id.bubble_layout);
            ImageView img = new ImageView(this);
            Button bubbleButton = FindViewById<Button>(Resource.Id.bigbubble);
            //create new bubble image
            img.LayoutParameters = new LinearLayout.LayoutParams(width: ViewGroup.LayoutParams.MatchParent, height: ViewGroup.LayoutParams.MatchParent);
            img.Visibility = ViewStates.Visible;
            img.SetImageResource(Resource.Drawable.Bubble);
            var metrics = Resources.DisplayMetrics;
            //place the bubble on screen randomizing it's x pos
            layoutBase.AddView(img, 100+spawn, 100 + spawn);
            Random rnd = new Random();
            img.SetX(rnd.Next(0, metrics.WidthPixels));
            img.SetY(metrics.HeightPixels);
            // create an animator to move the bubble

            ValueAnimator animator = ValueAnimator.OfInt(metrics.HeightPixels, metrics.HeightPixels / 11);
            animator.SetDuration(1500);
            animator.Start();
            /* 
             * function to move bubble
             * called each frame the animation occurs
             * and moved the bubble upward and in a sine wave
             */
            animator.Update += (object sender2, ValueAnimator.AnimatorUpdateEventArgs f) =>
            {
                //animation value
                int newValue = (int)f.Animation.AnimatedValue;
                // Apply this new value to the object being animated.
                img.TranslationY = newValue;
                //move the bubble in a sine wave pattern
                if (newValue >= metrics.HeightPixels / 9)
                    img.TranslationX += 10 * (float)Math.Sin(newValue / 100);
                //if near the top slide the bubble into the big bubble at the top
                else
                {
                    if (img.TranslationX > 500)
                        img.TranslationX -= 3;
                    else
                        img.TranslationX += 3;
                }
                //remove the bubble and increment the counter
                if (1 == f.Animation.AnimatedFraction)
                {
                    double cashout = img.Width-100;
                    cashout /= 100;
                    layoutBase.RemoveView(img);
                    string newval = '$' + Convert.ToString(double.Parse(bubbleButton.Text.Remove(0, 1)) + cashout);
                    if (newval.IndexOf('.') - newval.Length == -2)
                        newval += '0';
                    bubbleButton.Text = newval;


                }

            };

        }

    };
}