using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using System.Json;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
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

    public class Questions : Android.Support.V4.App.Fragment
    {
        private CallDatabase m_database;
        private JsonValue m_questionBlock;
        private Question currentquestion;
        private int QuestionCount = 0;
        private int blockID = -1;
        private TextView question_view;
        private Button answer_one;
        private Button answer_two;
        private Button answer_three;
        private Button answer_four;
        private Button next_button;
        private UserObject user = SingleUserObject.getObject();
        private View view = null;
        private BottomMenuTest _main;

        public Questions(BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Questions NewInstance(BottomMenuTest main)
        {
            Questions temp = new Questions(main);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.Questions, container, false);
            setup();

            bool QuestionAnswered = false;

            next_button.Click += (sender, e) =>
            {
                if (QuestionAnswered)
                {
                    QuestionCount += 1;
                    QuestionAnswered = false;
                    if (QuestionCount >= 10)
                    {
                        Task.Run(async () => { await NextBlock(); }).Wait();
                        QuestionCount = 0;
                    }
                    else
                        currentquestion = new Question(m_questionBlock[QuestionCount]);

                    NextQuestion();
                }
            };

            question_view.Click += (sender, e) =>
            {
                QuestionAnswered = true;
            };

            answer_one.Click += (sender, e) =>
            {
                QuestionAnswered = true;
                if (currentquestion.m_QuestionOne == currentquestion.m_CorrectAnswer)
                {
                    answer_one.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    answer_one.Text = "incorrect";
                }
            };

            answer_two.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionTwo == currentquestion.m_CorrectAnswer)
                {
                    answer_two.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    answer_two.Text = "incorrect";
                }
            };

            answer_three.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionThree == currentquestion.m_CorrectAnswer)
                {
                    answer_three.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    answer_three.Text = "incorrect";
                }
            };

            answer_four.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionFour == currentquestion.m_CorrectAnswer)
                {
                    answer_four.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    answer_four.Text = "incorrect";
                }
            };
            return view;
        }

        private void setup()
        {
            //setBackground();

            m_database = new CallDatabase();
            user = SingleUserObject.getObject();
            next_button = view.FindViewById<Button>(Resource.Id.next_button);
            question_view = view.FindViewById<TextView>(Resource.Id.question_view);
            answer_one = view.FindViewById<Button>(Resource.Id.answer_one);
            answer_two = view.FindViewById<Button>(Resource.Id.answer_two);
            answer_three = view.FindViewById<Button>(Resource.Id.answer_three);
            answer_four = view.FindViewById<Button>(Resource.Id.answer_four);

            Task.Run(async () => { await NextBlock(); }).Wait();

            NextQuestion();
        }

        private async Task NextBlock()
        {
            if (blockID != -1)
                user.AddCompletedBlock(blockID);

            await m_database.RetreaveQuestionBlock();
            if (m_database.responce.m_code == 200)
            {
                JsonValue block = m_database.responce.m_json;
                user.BlocksRemaining = block["userobject"]["game_data"]["blocksRemaining"];
                if (user.BlocksRemaining != 0)
                {
                    m_questionBlock = block["question_block"];
                    blockID = m_questionBlock[0]["QuestionBlockID"];
                    currentquestion = new Question(m_questionBlock[0]);
                }
            }
        }

        private void NextQuestion()
        {
            //if (m_database.responce.m_code != 200) // Go to home if the API call failed
            //kick_to_home();
            if (user.BlocksRemaining != 0)
            {
                question_view.Text = currentquestion.m_QuestionText;
                answer_one.Text = currentquestion.m_QuestionOne;
                answer_two.Text = currentquestion.m_QuestionTwo;
                answer_three.Text = currentquestion.m_QuestionThree;
                answer_four.Text = currentquestion.m_QuestionFour;
            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(_main);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Congratulations!");
                alert.SetMessage("It looks like you've completed this subjects difficulty. Now it's time to try another!");
                alert.SetButton("OK", (c, ev) =>
                {
                    _main.OnBackPressed();
                    _main.OnBackPressed();
                });
                alert.Show();
            }
        }

        protected void CorrectAnswer()
        {
            int lv = user.Level;
            user.TotalQuestions += 1;
            // note replace with real value gained/////////////////////////////////////////////////////////////////
            user.TotalDonated += .01;
            int newlv = user.Level;
            if (lv != newlv)
            {
                View view = LayoutInflater.Inflate(Resource.Layout.LevelUp, null);
                AlertDialog builder = new AlertDialog.Builder(_main).Create();
                builder.SetView(view);
                builder.Show();
            }
        }
    }
}


// Code that hasnt been coppied over yet.
/*
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.questionlayout);
                background.Background = AppBackground.background;
            }
        }
*/
