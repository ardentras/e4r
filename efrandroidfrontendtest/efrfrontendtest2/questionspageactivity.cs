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
            ImageButton Continue = FindViewById<ImageButton>(Resource.Id.Continue);
            // find correct and compare it to variable
            //add 1 to the correct answer tally
            //boolean the question is answered 
            //calls the block of questions
            CallDatabase database = new CallDatabase(this);
            database.RetreaveQuestionBlock();

            int QuestionNum = 0;

            JsonValue block = database.responce.m_json;

            if (database.responce.m_responce == "success")
            {
                SuccessFunct(database);
            }


            BackArrow.Click += (sender, e) =>
            {
                Finish();
            };

            Continue.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionspageActivity));
            };
            //commented out so it would build, otherwise it would error.

            //BigGrayButton.Click += (sender, d) =>
            //{

            //    CallDatabase.block.Question[];
            //};

            //Answer1.Click += (sender, f) =>
            //{
            //    var intent = new Intent(this, typeof(QuestionspageActivity));

            //    if (block.Answer1[] == block.CorrectAnswer[])
            //      {
            //       block.Answer1[].UpdateText = {"Correct!"};
            //        var intent = new Intent(this, typeof(QuestionspageActivity));
            //    }
            //      else
            //    {
            //        block.Answer1[].updatetext = { "Wrong Answer continue"};
            //    }
            //};

            //Answer2.Click += (sender, a) =>
            //{
            //    var intent = new Intent(this, typeof(QuestionspageActivity));
            //    if (block.Answer2[] == block.CorrectAnswer[])
            //    {
            //        block.Answer2[].UpdateText = { "Correct!"};
            //        var intent = new Intent(this, typeof(QuestionspageActivity));
            //        StartActivity(intent);
            //    }
            //    else
            //    {
            //        Qblock.Answer2[].Updatetext = { "Wrong Answer continue"};
            //        StartActivity(intent);
            //    }
            //};
            //Answer3.Click += (sender, b) =>
            //{

            //    var intent = new Intent(this, typeof(QuestionspageActivity));
            //    if (Qblock.Answer3[] == Qblock.CorrectAnswer[])
            //    {
            //        Qblock.Answer2[].UpdateText = { "Correct!"};
            //        var intent = new Intent(this, typeof(QuestionspageActivity));
            //        StartActivity(intent);
            //    }
            //    else
            //    {
            //        Qblock.Answer3[].Updatetext = { "Wrong Answer continue"};
            //         StartActivity(intent);
            //    }
               
            //};
            //Answer4.Click += (sender, c) =>
            //{

            //    var intent = new Intent(this, typeof(QuestionspageActivity));
            //    if (Qblock.Answer4[] == block.CorrectAnswer[])
            //    {
            //        Qblock.Answer4[].UpdateText = { "Correct!"};
            //        var intent = new Intent(this, typeof(QuestionspageActivity));
            //        StartActivity(intent);
            //    }
            //    else
            //    {
            //        Qblock.Answer2[].Updatetext = { "Wrong Answer continue"};
            //        StartActivity(intent);
            //    }
            //};
        }

        private void LoaderQuestionBlock ()
         {
            String[] Qblock= { "\0" };  
           //  array of strings to store the answer
       //  pull till empty 
         //reload questions block
         }
       private void SuccessFunct( CallDatabase database)
        {
            var Qblock = database.responce.m_json;
        }
        private void SetQuestions()
        {
            //Pass the five strings 
            //Or pass a list of strings
            //
        }
    }
}


    // int id = block[0]["questionID"];