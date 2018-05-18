using Android.App;
using Android.Content;
using System.Json;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

using EFRFrontEndTest2.Assets;
using System.Net;

namespace EFRFrontEndTest2.Fragments
{
    public struct Question
    {

        public Question(JsonValue block)
        {
            m_QuestionText = WebUtility.HtmlDecode(block["QuestionText"].ToString());
            m_AnswerOne = WebUtility.HtmlDecode(block["QuestionOne"].ToString());
            m_AnswerTwo = WebUtility.HtmlDecode(block["QuestionTwo"].ToString());
            m_AnswerThree = WebUtility.HtmlDecode(block["QuestionThree"].ToString());
            m_AnswerFour = WebUtility.HtmlDecode(block["QuestionFour"].ToString());
            m_CorrectAnswer = WebUtility.HtmlDecode(block["CorrectAnswer"].ToString());
            m_StatsOne = block["StatsOne"];
            m_StatsTwo = block["StatsTwo"];
            m_StatsThree = block["StatsThree"];
            m_StatsFour = block["StatsFour"];
            m_HelpID = 0;
            m_QuestionID = block["QuestionID"];
            m_QuestionBlockID = block["QuestionBlockID"];
        }

        public string m_QuestionText;
        public string m_AnswerOne;
        public string m_AnswerTwo;
        public string m_AnswerThree;
        public string m_AnswerFour;
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
        private int QuestionsInBlock = 0;
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
        private bool finished = false;
        private bool QuestionAnswered = false;

        public Questions(BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnStop()
        {
            m_database.UpdateUO().Wait();
            base.OnStop();
        }
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnResume()
        {
            base.OnResume();
            SetBackgrounds();
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
            SetBackgrounds();


            next_button.Click += (sender, e) =>
            {
                if (QuestionAnswered)
                {
                    var localData = Application.Context.GetSharedPreferences("CurrentBlock", FileCreationMode.Private);
                    var edit = localData.Edit();
                    QuestionCount += 1;
                    edit.PutInt("QuestionNum", QuestionCount);
                    edit.Apply();
                    QuestionAnswered = false;
                    if (QuestionCount >= QuestionsInBlock)    
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
                if (!QuestionAnswered)
                {
                    QuestionAnswered = true;
                    if (currentquestion.m_AnswerOne == currentquestion.m_CorrectAnswer)
                    {
                        answer_one.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        CorrectAnswer();
                    }
                    else
                    {
                        answer_one.SetBackgroundColor(Android.Graphics.Color.IndianRed);
                        HighlightAnswer();
                    }
                }
            };

            answer_two.Click += (sender, e) =>
            {
                if (!QuestionAnswered)
                {
                    QuestionAnswered = true;
                    if (currentquestion.m_AnswerTwo == currentquestion.m_CorrectAnswer)
                    {
                        answer_two.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        CorrectAnswer();
                    }
                    else
                    {
                        answer_two.SetBackgroundColor(Android.Graphics.Color.IndianRed);
                        HighlightAnswer();
                    }
                }
            };

            answer_three.Click += (sender, e) =>
            {
                if (!QuestionAnswered)
                {
                    QuestionAnswered = true;
                    if (currentquestion.m_AnswerThree == currentquestion.m_CorrectAnswer)
                    {
                        answer_three.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        CorrectAnswer();
                    }
                    else
                    {
                        answer_three.SetBackgroundColor(Android.Graphics.Color.IndianRed);
                        HighlightAnswer();
                    }
                }
            };

            answer_four.Click += (sender, e) =>
            {
                if (!QuestionAnswered)
                {
                    QuestionAnswered = true;
                    if (currentquestion.m_AnswerFour == currentquestion.m_CorrectAnswer)
                    {
                        answer_four.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                        CorrectAnswer();
                    }
                    else
                    {
                        answer_four.SetBackgroundColor(Android.Graphics.Color.IndianRed);
                        HighlightAnswer();
                    }
                }
            };

            return view;
        }

        private void setup()
        {

            m_database = new CallDatabase();
            user = SingleUserObject.getObject();
            next_button = view.FindViewById<Button>(Resource.Id.next_button);
            question_view = view.FindViewById<TextView>(Resource.Id.question_view);
            answer_one = view.FindViewById<Button>(Resource.Id.answer_one);
            answer_two = view.FindViewById<Button>(Resource.Id.answer_two);
            answer_three = view.FindViewById<Button>(Resource.Id.answer_three);
            answer_four = view.FindViewById<Button>(Resource.Id.answer_four);

            var localData = Application.Context.GetSharedPreferences("CurrentBlock", FileCreationMode.Private);

            if (localData.GetString("Block", "fail") == "fail" || localData.GetInt("subject", -1) != user.SubjectID || localData.GetInt("difficulty", -1) != user.Difficulty)
                Task.Run(async () => { await NextBlock(); }).Wait();
            else
            {
                string block = localData.GetString("Block", "fail");
                m_questionBlock = JsonValue.Parse(block);
                blockID = m_questionBlock[0]["QuestionBlockID"];
                QuestionCount = localData.GetInt("QuestionNum", 0);
                currentquestion = new Question(m_questionBlock[QuestionCount]);
            }

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
                    QuestionsInBlock = m_questionBlock.Count;
                    blockID = m_questionBlock[0]["QuestionBlockID"];
                    currentquestion = new Question(m_questionBlock[0]);

                    var localData = Application.Context.GetSharedPreferences("CurrentBlock", FileCreationMode.Private);
                    var edit = localData.Edit();
                    edit.PutString("Block", m_questionBlock.ToString());
                    edit.PutInt("QuestionNum", 0);
                    edit.PutInt("subject", user.SubjectID);
                    edit.PutInt("difficulty", user.Difficulty);
                    edit.Apply();
                }
            }
            else
            {
                finished = true;
                QuestionAnswered = true;
            }
        }

        private void NextQuestion()
        {
            answer_one.SetBackgroundColor(Android.Graphics.Color.White);
            answer_two.SetBackgroundColor(Android.Graphics.Color.White);
            answer_three.SetBackgroundColor(Android.Graphics.Color.White);
            answer_four.SetBackgroundColor(Android.Graphics.Color.White);
            if (finished == false)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(_main);
                AlertDialog alert = dialog.Create();
                alert.SetButton("OK", (c, ev) =>
                {
                    _main.OnBackPressed();
                    _main.OnBackPressed();
                });
                switch (m_database.responce.m_code)
                {
                    case 200:
                    case 0:
                        {
                            if (user.BlocksRemaining != 0)
                            {
                                question_view.Text = currentquestion.m_QuestionText;
                                answer_one.Text = currentquestion.m_AnswerOne;
                                answer_two.Text = currentquestion.m_AnswerTwo;
                                answer_three.Text = currentquestion.m_AnswerThree;
                                answer_four.Text = currentquestion.m_AnswerFour;
                            }
                            else
                            {
                                finished = true;
                                QuestionAnswered = true;

                                question_view.Text = "You've answered all of the questions for this subject and difficulty";
                                alert.SetTitle("Congratulations!");
                                alert.SetMessage("It looks like you've completed this subjects difficulty. Now it's time to try another!");
                                alert.Show();
                                answer_one.Clickable = false;
                                answer_one.Text = "";
                                answer_two.Clickable = false;
                                answer_two.Text = "";
                                answer_three.Clickable = false;
                                answer_three.Text = "";
                                answer_four.Clickable = false;
                                answer_four.Text = "";
                            }
                            break;
                        }
                    case 503: // Network issues
                    case 504:
                        {
                            alert.SetMessage(m_database.responce.m_reason);
                            alert.Show();
                            break;
                        }
                    default:
                        {
                            alert.SetMessage("Unknown Error");
                            alert.Show();
                            break;
                        }
                }
            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(_main);
                AlertDialog alert = dialog.Create();
                alert.SetButton("OK", (c, ev) =>
                {
                    _main.OnBackPressed();
                    _main.OnBackPressed();
                });
                alert.SetTitle("Congratulations!");
                alert.SetMessage("It looks like you've completed this subjects difficulty. Now it's time to try another!");
                alert.Show();
                finished = true;
                QuestionAnswered = true;
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
                user.Level = newlv;
                View view = LayoutInflater.Inflate(Resource.Layout.LevelUp, null);
                AlertDialog builder = new AlertDialog.Builder(_main).Create();
                builder.SetView(view);
                builder.Show();
            }
        }

        private void HighlightAnswer()
        {
                    if (currentquestion.m_AnswerOne == currentquestion.m_CorrectAnswer)
                        answer_one.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                    else if (currentquestion.m_AnswerTwo == currentquestion.m_CorrectAnswer)
                        answer_two.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                    else if (currentquestion.m_AnswerThree == currentquestion.m_CorrectAnswer)
                        answer_three.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                    else if (currentquestion.m_AnswerFour == currentquestion.m_CorrectAnswer)
                        answer_four.SetBackgroundColor(Android.Graphics.Color.LightGreen);
        }

        protected void SetBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.questionlayout);
                background.Background = AppBackground.background;
            }
        }
    }
}
