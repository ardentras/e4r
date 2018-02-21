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
            UserObject obj = SingleUserObject.getObject();

            questions.Text = Convert.ToString(obj.QuestionsAnswered);
            money.Text = Convert.ToString(obj.MoneyEarned);
            lv.Text = Convert.ToString((int)(Math.Sqrt(obj.QuestionsAnswered/10) + obj.MoneyEarned / 50));
            if (lv.Text == "0")
                lv.Text = "1";
        }
    }
}