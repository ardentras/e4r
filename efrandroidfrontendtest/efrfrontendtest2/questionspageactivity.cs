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
        CallDatabase m_database;
        JsonValue m_questionBlock;
        JsonValue m_currentquestion;
        TextView BigGrayButton;
        TextView Answer1;
        TextView Answer2;
        TextView Answer3;
        TextView Answer4;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TextView Answer4 = FindViewById<TextView>(Resource.Id.Answer4);
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionsPage);
            setBackground();

            ImageButton BackArrow = FindViewById<ImageButton>(Resource.Id.BackArrow);
            ImageButton Continue = FindViewById<ImageButton>(Resource.Id.Continue);
            BigGrayButton = FindViewById<TextView>(Resource.Id.BigGrayCircle);
            Answer1 = FindViewById<TextView>(Resource.Id.Answer1);
            Answer2 = FindViewById<TextView>(Resource.Id.Answer2);
            Answer3 = FindViewById<TextView>(Resource.Id.Answer3);
            Answer4 = FindViewById<TextView>(Resource.Id.Answer4);

            // find correct and compare it to variable
            //add 1 to the correct answer tally
            //boolean the question is answered 
            //calls the block of questions
            // string y = m_currentquestion["QuestionID"];
            m_database = new CallDatabase(this);
            Task.Run(async () => { await setup(); });

            int QuestionNum = 0;

            int QuestionBlockNum = 1;

            bool QuestionAnswered = false;

            if (m_database.responce.m_responce == "success")
            {
                SuccessFunct(m_database);
            }


            BackArrow.Click += (sender, e) =>
            {
                Finish();
            };

            Continue.Click += (sender, e) =>
            {

                //var intent = new Intent(this, typeof(QuestionspageActivity));

                if (QuestionAnswered)
                {

                    //JsonValue k = block["question_block"][QuestionBlockNum++];
                    //SetQuestions(k);
                    QuestionAnswered = false;
                    if (QuestionBlockNum >= 10)
                    {
                        NextBlock();
                        QuestionBlockNum = 0;
                    }
                }
            };
            async void NextBlock()
            {
                await m_database.RetreaveQuestionBlock();
                //block = m_database.responce.m_json;

            }

            BigGrayButton.Click += (sender, d) =>
            {

                QuestionAnswered = true;
            };

            Answer1.Click += (sender, f) =>
            {
                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));

                if (m_currentquestion["Question1"] == m_currentquestion["CorrectAnswer"])
                {
                    Answer1.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));

                }
                else
                {
                    Answer1.Text = "incorrect";
                }
            };

            Answer2.Click += (sender, a) =>
            {
                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));
                if (m_currentquestion["Question2"] == m_currentquestion["CorrectAnswer"])
                {
                    Answer2.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));

                }
                else
                {
                    Answer2.Text = "correct";
                }
            };
            // block of ten questions
            // load page puts it in layout
            // code all four buttons so when pressed 
            // match queston to answer in text button
            //set boolean to check if answer was selected

            Answer3.Click += (sender, b) =>
            {
                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));
                if (m_currentquestion["Question3"] == m_currentquestion["CorrectAnswer"])
                {
                    Answer3.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));

                }
                else
                {
                    Answer3.Text = "correct";
                }
            };

            Answer4.Click += (sender, c) =>
            {

                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));
                if (m_currentquestion["Question4"] == m_currentquestion["CorrectAnswer"])
                {
                    Answer4.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));

                }
                else
                {
                    Answer4.Text = "correct";
                }

            };

        }

        private async Task<bool> setup()
        {
            JsonValue block;
            await m_database.RetreaveQuestionBlock();
            block = m_database.responce.m_json;
            m_questionBlock = block["question_block"];
            m_currentquestion = m_questionBlock[0];
            SetQuestion(m_currentquestion);
            return true;
        }

        private void LoaderQuestionBlock()
        {
            String[] Qblock = { "\0" };
            //  array of strings to store the answer
            //  pull till empty 
            //reload questions block
        }
        private void SuccessFunct(CallDatabase database)
        {
            var Qblock = database.responce.m_json;
        }
        private void SetQuestion(JsonValue block)
        {
            BigGrayButton.Text = block["QuestionText"];
            Answer1.Text = block["QuestionOne"];
            Answer2.Text = block["QuestionTwo"];
            Answer3.Text = block["QuestionThree"];
            Answer4.Text = block["QuestionFour"];
        }
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = FindViewById<LinearLayout>(Resource.Id.questionlayout);
                background.Background = AppBackground.background;
            }
        }
    }
}




// int id = block[0]["questionID"];