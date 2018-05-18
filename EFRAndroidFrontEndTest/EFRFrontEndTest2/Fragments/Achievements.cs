using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class Achievements : Android.Support.V4.App.Fragment
    {
        View view = null;
        private BottomMenuTest _main;

        public Achievements(BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override void OnResume()
        {
            base.OnResume();
            SetBackgrounds();
        }

        public static Achievements NewInstance(BottomMenuTest main)
        {
            Achievements temp = new Achievements(main);

            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.AchievementsPage, container, false);
            SetBackgrounds();

            TextView lv = view.FindViewById<TextView>(Resource.Id.level);
            TextView questions = view.FindViewById<TextView>(Resource.Id.questionsAnswered);
            TextView money = view.FindViewById<TextView>(Resource.Id.moneyRaised);
            UserObject user = SingleUserObject.getObject();

            questions.Text = user.TotalQuestions.ToString();
            money.Text = user.TotalDonated.ToString();
            lv.Text = user.Level.ToString();

            return view;
        }

        protected void SetBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.bubble_layout);
                background.Background = AppBackground.background;
            }
        }
    }
}