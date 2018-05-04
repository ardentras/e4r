using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class Solve : Android.Support.V4.App.Fragment
    {
        private UserObject user = SingleUserObject.getObject();
        private View view = null;
        private RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider(); // So the garbage collector is called less often if a kid just LOVES tapping the shuffle button

        private EFRFrontEndTest2.BottomMenuTest _main;
        public Solve(EFRFrontEndTest2.BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Solve NewInstance(EFRFrontEndTest2.BottomMenuTest main)
        {
            Solve temp = new Solve(main);

            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Solve, container, false);
            ImageButton Math = view.FindViewById<ImageButton>(Resource.Id.math_button);
            ImageButton History = view.FindViewById<ImageButton>(Resource.Id.history_button);
            ImageButton Chemistry = view.FindViewById<ImageButton>(Resource.Id.chemistry_button);
            ImageButton Biology = view.FindViewById<ImageButton>(Resource.Id.biology_button);
            ImageButton Physics = view.FindViewById<ImageButton>(Resource.Id.physics_button);
            ImageButton Shuffle = view.FindViewById<ImageButton>(Resource.Id.shuffle_button);

            Math.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Math.Id);
            };
            History.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(History.Id);
            };
            Chemistry.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Chemistry.Id);
            };
            Biology.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Biology.Id);
            };
            Physics.Click += delegate
            {
                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Physics.Id);
            };
            Shuffle.Click += delegate
            {
                byte[] number = new byte[1];
                rand.GetBytes(number);
                switch ((int)number[0] % 5)
                {
                    case 2: // Implement 2 - 5 when questions are added and API call allows it
                    case 3:
                    case 4:
                    case 5:
                    case 1:
                        {
                            user.SubjectID = 1;
                            user.SubjectName = "Mathematics";
                            _main.LoadFragment(Math.Id);
                            break;
                        }
                    default: // Should never reach this stage, but defaults to math just in case
                        user.SubjectID = 1;
                        user.SubjectName = "Mathematics";
                        _main.LoadFragment(Math.Id);
                        break;
                }

                user.SubjectID = 1;
                user.SubjectName = "Mathematics";
                _main.LoadFragment(Physics.Id);
            };

            return view;
        }

        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = view.FindViewById<GridLayout>(Resource.Id.gridLayout1);
                background.Background = AppBackground.background;
            }
        }
    }
}




// Code that hasnt been coppied over yet.
/*
        protected override void OnCreate(Bundle savedInstanceState)
        {
            setBackground();
        }


    }
}
*/
