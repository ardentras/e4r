using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EFRFrontEndTest2.Assets;

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

            TextView BigGrayButton = FindViewById<TextView>(Resource.Id.BigGrayCircle);
            TextView Answer1 = FindViewById<TextView>(Resource.Id.Answer1);
            TextView Answer2 = FindViewById<TextView>(Resource.Id.Answer2);
            TextView Answer3 = FindViewById<TextView>(Resource.Id.Answer3);
            TextView Answer4 = FindViewById<TextView>(Resource.Id.Answer4);
            ImageButton BackArrow = FindViewById<ImageButton>(Resource.Id.BackArrow);
            ImageButton ForwardArrow = FindViewById<ImageButton>(Resource.Id.ForwardArrow);

            //calls the block of questions
            CallDatabase database = new CallDatabase(this);
            database.RetreaveQuestionBlock();

            int QuestionNum = 0;

            JsonValue block = database.responce.m_responce;
             
            BackArrow.Click += (sender, e) =>
            {
                Finish();
            };

            ForwardArrow.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionspageActivity));
            };

            BigGrayButton.Click += (sender, d) =>
            {
              
            };

            Answer1.Click += (sender, f) =>
            {
             //  if (Answer1 == CorrectAnswer)
             //  { 
                 
  
               //   Answer1.Update Text = { " " };
                 // StartActivity(intent);
              //  }
            };

            Answer2.Click += (sender, a) =>
            {
               // if (Answer2 == CorrectAnswer)
               // {

               //     var intent = new Intent(this, typeof(QuestionspageActivity));
               //     Answer2.Update Text = { " " };
               //     StartActivity(intent);
              //  }

              //  else
               // {

               // }
            };
            Answer3.Click += (sender, b) =>
            {
               /* if (Answer3 == CorrectAnswer)
                {

                    var intent = new Intent(this, typeof(QuestionspageActivity));
                    Answer3.Update Text = { " " };
                    StartActivity(intent);
                }*/
            };
            Answer4.Click += (sender, c) =>
            {
               /* if (Answer4 == CorrectAnswer)
                {

                    var intent = new Intent(this, typeof(QuestionspageActivity));
                    Answer4.Update Text = {" "};
                    StartActivity(intent);
                }*/
            };
        }
    }
}
