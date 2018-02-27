using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "SelectSubjectScreenActivity")]
    public class SelectSubjectScreenActivity : Activity
    {
        const int PHYSICS = 4;
        const int CHEMISTRY = 3;
        const int BIOLOGY = 2;
        const int HISTORY = 1;
        const int MATH = 0;
        int currentSubjectID;
        RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider(); // So the garbage collector is called less often if a kid just LOVES tapping the shuffle button
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectSubjectScreen);
            UserObject uo = SingleUserObject.getObject();
            currentSubjectID = uo.SubjectID;

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);
            ImageButton continueButton = FindViewById<ImageButton>(Resource.Id.continueButton);
            ImageButton physicsOption = FindViewById<ImageButton>(Resource.Id.physicsOption);
            ImageButton chemistryOption = FindViewById<ImageButton>(Resource.Id.chemistryOption);
            ImageButton biologyOption = FindViewById<ImageButton>(Resource.Id.biologyOption);
            ImageButton mathOption = FindViewById<ImageButton>(Resource.Id.mathOption);
            ImageButton historyOption = FindViewById<ImageButton>(Resource.Id.historyOption);
            ImageButton shuffleOption = FindViewById<ImageButton>(Resource.Id.shuffleOption);

            backButton.Click += (sender, e) =>
            {
                uo.SubjectID = currentSubjectID;
                Finish();
            };

            continueButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionDificultypageActivity));
                uo.SubjectID = currentSubjectID;
                StartActivity(intent);
            };
            physicsOption.Click += (sender, e) =>
            {
                selectButton(PHYSICS);
            };
            chemistryOption.Click += (sender, e) =>
            {
                selectButton(CHEMISTRY);
            };
            biologyOption.Click += (sender, e) =>
            {
                selectButton(BIOLOGY);
            };
            mathOption.Click += (sender, e) =>
            {
                selectButton(MATH);
            };
            historyOption.Click += (sender, e) =>
            {
                selectButton(HISTORY);
            };
            shuffleOption.Click += (sender, e) =>
            {
                byte[] number = new byte[1];
                rand.GetBytes(number);
                selectButton((int)number[0] % 4); // Create a number 0 - 4 and selects the corresponding button
            };

            void selectButton(int selected)
            {
                currentButton(currentSubjectID).SetBackgroundResource(Resource.Drawable.GreenButtonIcon);
                switch (selected)
                {
                    case 0:
                        mathOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = MATH;
                        break;
                    case 1:
                        historyOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = HISTORY;
                        break;
                    case 2:
                        biologyOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = BIOLOGY;
                        break;
                    case 3:
                        chemistryOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = CHEMISTRY;
                        break;
                    case 4:
                        physicsOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = PHYSICS;
                        break;
                    default: // Should never reach this stage, but defaults to math just in case
                        mathOption.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
                        currentSubjectID = MATH;
                        break;
                }

                ImageButton currentButton(int button)
                {
                    switch (button)
                    {
                        case 0:
                            return mathOption;
                        case 1:
                            return historyOption;
                        case 2:
                            return biologyOption;
                        case 3:
                            return chemistryOption;
                        case 4:
                            return physicsOption;
                        default: // Should never reach this stage, but defaults to math just in case
                            return mathOption;
                    }
                }
            }
        }
    }
}
//test

//Multi-subject functionality, replace if stretch goal is achieved.
/*
 * 

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.backButton);
            ImageButton continueButton = FindViewById<ImageButton>(Resource.Id.continueButton);
            ImageButton physicsOption = FindViewById<ImageButton>(Resource.Id.physicsOption);          // 10000
            ImageButton chemistryOption = FindViewById<ImageButton>(Resource.Id.chemistryOption);      // 01000
            ImageButton biologyOption = FindViewById<ImageButton>(Resource.Id.biologyOption);          // 00100
            ImageButton mathOption = FindViewById<ImageButton>(Resource.Id.mathOption);                // 00010
            ImageButton historyOption = FindViewById<ImageButton>(Resource.Id.historyOption);          // 00001
            ImageButton shuffleOption = FindViewById<ImageButton>(Resource.Id.shuffleOption);          // ?????

            backButton.Click += (sender, e) =>
            {
                Finish();
            };

            continueButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(QuestionDificultypageActivity));
                intent.PutExtra("subjects", binaryChoice);
                StartActivity(intent);
            };
            physicsOption.Click += (sender, e) =>
            {
                binaryChoice = binaryChoice ^ PHYSICS;
                updateButton(physicsOption, PHYSICS);
            };
            chemistryOption.Click += (sender, e) =>
            {
                binaryChoice = binaryChoice ^ CHEMISTRY;
                updateButton(chemistryOption, CHEMISTRY);
            };
            biologyOption.Click += (sender, e) =>
            {
                binaryChoice = binaryChoice ^ BIOLOGY;
                updateButton(biologyOption, BIOLOGY);
            };
            mathOption.Click += (sender, e) =>
            {
                binaryChoice = binaryChoice ^ MATH;
                updateButton(mathOption, MATH);
            };
            historyOption.Click += (sender, e) =>
            {
                binaryChoice = binaryChoice ^ HISTORY;
                updateButton(historyOption, HISTORY);
            };
            shuffleOption.Click += (sender, e) =>
            {
                byte[] number = new byte[1];
                rand.GetBytes(number);
                binaryChoice = (int)number[0] % 32; //Creates a number from 0 - 31
                updateButton(physicsOption, PHYSICS);
                updateButton(chemistryOption, CHEMISTRY);
                updateButton(biologyOption, BIOLOGY);
                updateButton(mathOption, MATH);
                updateButton(historyOption, HISTORY);
            };
        }

        //For readability and saving repeated code
        void updateButton(ImageButton button, int check)
        {
            if ((binaryChoice ^ check) < binaryChoice) //Wasn't selected, but is now. So update to selected icon
                button.SetBackgroundResource(Resource.Drawable.GreenButtonSelectedIcon);
            else
                button.SetBackgroundResource(Resource.Drawable.GreenButtonIcon);
        }
    }

    */