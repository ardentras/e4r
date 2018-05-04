using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "AchievementsPageActivity")]
    public class AchievementsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.AchievementsPage);

            TextView lv = FindViewById<TextView>(Resource.Id.level);
            TextView questions = FindViewById<TextView>(Resource.Id.questionsAnswered);
            TextView money = FindViewById<TextView>(Resource.Id.moneyRaised);
            UserObject user = SingleUserObject.getObject();

            questions.Text = user.TotalQuestions.ToString();
            money.Text = user.TotalDonated.ToString();
            lv.Text = user.Level.ToString();
        }
    }
}