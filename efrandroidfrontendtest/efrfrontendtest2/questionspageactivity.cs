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
    public struct Question
    {
        public Question(JsonValue block)
        {
            m_QuestionText = block["QuestionText"].ToString();
            m_QuestionOne = block["QuestionOne"].ToString();
            m_QuestionTwo = block["QuestionTwo"].ToString();
            m_QuestionThree = block["QuestionThree"].ToString();
            m_QuestionFour = block["QuestionFour"].ToString();
            m_CorrectAnswer = block["CorrectAnswer"].ToString();
            m_StatsOne = block["StatsOne"];
            m_StatsTwo = block["StatsTwo"];
            m_StatsThree = block["StatsThree"];
            m_StatsFour = block["StatsFour"];
            m_HelpID = block["HelpID"];
            m_QuestionID = block["QuestionID"];
            m_QuestionBlockID = block["QuestionBlockID"];
        }

        public string m_QuestionText;
        public string m_QuestionOne;
        public string m_QuestionTwo;
        public string m_QuestionThree;
        public string m_QuestionFour;
        public string m_CorrectAnswer;
        public int m_StatsOne;
        public int m_StatsTwo;
        public int m_StatsThree;
        public int m_StatsFour;
        public int m_HelpID;
        public int m_QuestionID;
        public int m_QuestionBlockID;
    }

    [Activity(Label = "QuestionspageActivity")]
    public class QuestionspageActivity : Activity
    {
        CallDatabase m_database;
        JsonValue m_questionBlock;
        TextView BigGrayButton;
        TextView Answer1;
        TextView Answer2;
        TextView Answer3;
        TextView Answer4;
        ImageButton BackArrow;
        ImageButton Continue;
        Question currentquestion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
           int inca = 0;
            inca++;
            if (inca == 0)
            {
                m_database = new CallDatabase(this);
                Task.Run(async () => { currentquestion = await setup(); }).Wait(); //Wait should not be used, rewrite when a fix is found.

                RequestWindowFeature(WindowFeatures.NoTitle);
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.QuestionsPage);
                setBackground();

                BackArrow = FindViewById<ImageButton>(Resource.Id.BackArrow);
                Continue = FindViewById<ImageButton>(Resource.Id.Continue);
                BigGrayButton = FindViewById<TextView>(Resource.Id.BigGrayCircle);
                Answer1 = FindViewById<TextView>(Resource.Id.Answer1);
                Answer2 = FindViewById<TextView>(Resource.Id.Answer2);
                Answer3 = FindViewById<TextView>(Resource.Id.Answer3);
                Answer4 = FindViewById<TextView>(Resource.Id.Answer4);

                BigGrayButton.Text = currentquestion.m_QuestionText;
                Answer1.Text = currentquestion.m_QuestionOne;
                Answer2.Text = currentquestion.m_QuestionTwo;
                Answer3.Text = currentquestion.m_QuestionThree;
                Answer4.Text = currentquestion.m_QuestionFour;

              
            }

     
            else {
                m_database = new CallDatabase(this);
                Task.Run(async () => { currentquestion = await setup(); }).Wait(); //Wait should not be used, rewrite when a fix is found.

               // RequestWindowFeature(WindowFeatures.NoTitle);
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.QuestionsPage);
                setBackground();

                BackArrow = FindViewById<ImageButton>(Resource.Id.BackArrow);
                Continue = FindViewById<ImageButton>(Resource.Id.Continue);
                BigGrayButton = FindViewById<TextView>(Resource.Id.BigGrayCircle);
                Answer1 = FindViewById<TextView>(Resource.Id.Answer1);
                Answer2 = FindViewById<TextView>(Resource.Id.Answer2);
                Answer3 = FindViewById<TextView>(Resource.Id.Answer3);
                Answer4 = FindViewById<TextView>(Resource.Id.Answer4);

                BigGrayButton.Text = currentquestion.m_QuestionText;
                Answer1.Text = currentquestion.m_QuestionOne;
                Answer2.Text = currentquestion.m_QuestionTwo;
                Answer3.Text = currentquestion.m_QuestionThree;
                Answer4.Text = currentquestion.m_QuestionFour;
            }
            //find correct and compare it to variable
            //add 1 to the correct answer tally
            //boolean the question is answered 
            //calls the block of questions
            //string y = m_currentquestion["QuestionID"];


            int QuestionNum = 0;

            int QuestionBlockNum = 1;

            bool QuestionAnswered = false;

            BackArrow.Click += (sender, e) =>
            {
                Finish();
            };

            Continue.Click += (sender, e) =>
            {
                //var intent = new Intent(this, typeof(QuestionspageActivity));

                if (QuestionAnswered)
                {
                    
                    OnCreate(savedInstanceState);

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
          

                if (currentquestion.m_QuestionOne == currentquestion.m_CorrectAnswer)
                {
                    Answer1.Text = "correct";
           
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

                if (currentquestion.m_QuestionTwo == currentquestion.m_CorrectAnswer)
                {
                    Answer2.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));
                }
                else
                {
                    Answer2.Text = "incorrect";
                }
            };


            Answer3.Click += (sender, b) =>
            {
                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));

                if (currentquestion.m_QuestionThree == currentquestion.m_CorrectAnswer)
                {
                    Answer3.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));
                }
                else
                {
                    Answer3.Text = "incorrect";
                }
            };

            Answer4.Click += (sender, c) =>
            {
                QuestionAnswered = true;
                //var intent = new Intent(this, typeof(QuestionspageActivity));

                if (currentquestion.m_QuestionFour == currentquestion.m_CorrectAnswer)
                {
                    Answer4.Text = "correct";
                    //   var intent = new Intent(this, typeof(QuestionspageActivity));
                }
                else
                {
                    Answer4.Text = "incorrect";
                }
            };

        }

        private async Task<Question> setup()
        {
            JsonValue block;
            await m_database.RetreaveQuestionBlock();
            block = m_database.responce.m_json;
            m_questionBlock = block["question_block"];
            return new Question(m_questionBlock[0]);
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