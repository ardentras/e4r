using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "SelectSubjectScreenActivity")]
    public class SelectSubjectScreenActivity : Activity
    {
        private struct Subject
        {
            public int id;
            public string name;

            public Subject(int m_id, string m_name)
            {
                id = m_id;
                name = m_name;
            }
        }

        private Subject Physics = new Subject(1, "Mathematics");
        private Subject Chemistry = new Subject(1, "Mathematics");
        private Subject Biology = new Subject(1, "Mathematics");
        private Subject History = new Subject(1, "Mathematics");
        private Subject Math = new Subject(1, "Mathematics");
        private Subject currentSubject;
        RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider(); // So the garbage collector is called less often if a kid just LOVES tapping the shuffle button
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectSubjectScreen);
   
            setBackground();
            UserObject uo = SingleUserObject.getObject();

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);
            ImageButton continueButton = FindViewById<ImageButton>(Resource.Id.continueButton);
            ImageButton physicsOption = FindViewById<ImageButton>(Resource.Id.physicsOption);
            ImageButton chemistryOption = FindViewById<ImageButton>(Resource.Id.chemistryOption);
            ImageButton biologyOption = FindViewById<ImageButton>(Resource.Id.biologyOption);
            ImageButton mathOption = FindViewById<ImageButton>(Resource.Id.mathOption);
            ImageButton historyOption = FindViewById<ImageButton>(Resource.Id.historyOption);
            ImageButton shuffleOption = FindViewById<ImageButton>(Resource.Id.shuffleOption);
            currentSubject.id = uo.SubjectID;
            currentSubject.name = uo.SubjectName;

            backButton.Click += (sender, e) =>
            {
                uo.SubjectID = currentSubject.id;
                uo.SubjectName = currentSubject.name;
                Finish();
            };

            continueButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionDificultypageActivity));
                uo.SubjectID = currentSubject.id;
                uo.SubjectName = currentSubject.name;
                StartActivity(intent);
            };
            physicsOption.Click += (sender, e) =>
            {
                selectButton(Physics.id);
            };
            chemistryOption.Click += (sender, e) =>
            {
                selectButton(Chemistry.id);
            };
            biologyOption.Click += (sender, e) =>
            {
                selectButton(Biology.id);
            };
            mathOption.Click += (sender, e) =>
            {
                selectButton(History.id);
            };
            historyOption.Click += (sender, e) =>
            {
                selectButton(Math.id);
            };
            shuffleOption.Click += (sender, e) =>
            {
                byte[] number = new byte[1];
                rand.GetBytes(number);
                selectButton(1); // Remove and uncomment next line when new subjects are added
                //selectButton((int)number[0] % 5); // Create a number 0 - 4 and selects the corresponding button
            };

            void selectButton(int selected)
            {
                currentButton(selected).SetBackgroundResource(Resource.Drawable.GreenButtonIcon);
                switch (selected)
                {
                    case 1:
                        mathOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = Math;
                        break;
                    case 2:
                        historyOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = History;
                        break;
                    case 3:
                        biologyOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = Biology;
                        break;
                    case 4:
                        chemistryOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = Chemistry;
                        break;
                    case 5:
                        physicsOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = Physics;
                        break;
                    default: // Should never reach this stage, but defaults to math just in case
                        mathOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubject = Math;
                        break;
                }

                ImageButton currentButton(int button)
                {
                    switch (button)
                    {
                        case 1:
                            return mathOption;
                        case 2:
                            return historyOption;
                        case 3:
                            return biologyOption;
                        case 4:
                            return chemistryOption;
                        case 5:
                            return physicsOption;
                        default: // Should never reach this stage, but defaults to math just in case
                            return mathOption;
                    }
                }
            }
        }
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = FindViewById<GridLayout>(Resource.Id.gridLayout1);
                background.Background = AppBackground.background;
            }
        }
    }
}