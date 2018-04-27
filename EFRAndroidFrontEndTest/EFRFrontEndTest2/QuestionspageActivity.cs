using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
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
        UserObject user;
        JsonValue m_questionBlock;
        TextView BigGrayButton;
        TextView Answer1;
        TextView Answer2;
        TextView Answer3;
        TextView Answer4;
        ImageButton BackArrow;
        ImageButton Continue;
        Question currentquestion;
        private int QuestionCount = 0;
        private int blockID = -1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            m_database = new CallDatabase(this);
            setup();
            
            //find correct and compare it to variable
            //add 1 to the correct answer tally
            //boolean the question is answered 
            //calls the block of questions
            //string y = m_currentquestion["QuestionID"];

            bool QuestionAnswered = false;

            BackArrow.Click += (sender, e) =>
            {
                Finish();
            };

            Continue.Click += (sender, e) =>
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

            BigGrayButton.Click += (sender, e) =>
            {
                QuestionAnswered = true;
            };

            Answer1.Click += (sender, e) =>
            {
                QuestionAnswered = true;
                if (currentquestion.m_QuestionOne == currentquestion.m_CorrectAnswer)
                {
                    Answer1.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    Answer1.Text = "incorrect";
                }
            };

            Answer2.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionTwo == currentquestion.m_CorrectAnswer)
                {
                    Answer2.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    Answer2.Text = "incorrect";
                }
            };
            
            Answer3.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionThree == currentquestion.m_CorrectAnswer)
                {
                    Answer3.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    Answer3.Text = "incorrect";
                }
            };

            Answer4.Click += (sender, e) =>
            {
                QuestionAnswered = true;

                if (currentquestion.m_QuestionFour == currentquestion.m_CorrectAnswer)
                {
                    Answer4.Text = "correct";
                    CorrectAnswer();
                }
                else
                {
                    Answer4.Text = "incorrect";
                }
            };

        }

        private void setup()
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.QuestionsPage);
            setBackground();

            user = SingleUserObject.getObject();
            BackArrow = FindViewById<ImageButton>(Resource.Id.BackArrow);
            Continue = FindViewById<ImageButton>(Resource.Id.Continue);
            BigGrayButton = FindViewById<TextView>(Resource.Id.BigGrayCircle);
            Answer1 = FindViewById<TextView>(Resource.Id.Answer1);
            Answer2 = FindViewById<TextView>(Resource.Id.Answer2);
            Answer3 = FindViewById<TextView>(Resource.Id.Answer3);
            Answer4 = FindViewById<TextView>(Resource.Id.Answer4);

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
                m_questionBlock = block["question_block"];
                blockID = m_questionBlock[0]["QuestionBlockID"];
                currentquestion = new Question(m_questionBlock[0]);
            }
        }

        private void NextQuestion()
        {
            if (m_database.responce.m_code != 200) // Go to home if the API call failed
                kick_to_home();
            else
            {
                BigGrayButton.Text = currentquestion.m_QuestionText;
                Answer1.Text = currentquestion.m_QuestionOne;
                Answer2.Text = currentquestion.m_QuestionTwo;
                Answer3.Text = currentquestion.m_QuestionThree;
                Answer4.Text = currentquestion.m_QuestionFour;
            }
            var stuff = user.UserObjectForm();
        }

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = FindViewById<LinearLayout>(Resource.Id.questionlayout);
                background.Background = AppBackground.background;
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
                AlertDialog builder = new AlertDialog.Builder(this).Create();
                builder.SetView(view);
                builder.Show();
            }
        }

        protected void kick_to_home()
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Uh Oh!");
            alert.SetMessage("Something went wrong with the server!");
            alert.SetButton("OK", (c, ev) =>
            {
                var intent = new Intent(this, typeof(DashboardActivity));
                StartActivity(intent);
            });
            alert.Show();
        }
    }
}