using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class ColorPicker : Android.Support.V4.App.Fragment
    {
        private BottomMenuTest _main;
        private UserObject user = SingleUserObject.getObject();
        private View view = null;
        public ColorPicker(BottomMenuTest main)
        {
            _main = main;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static ColorPicker NewInstance(BottomMenuTest main)
        {
            ColorPicker temp = new ColorPicker(main);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.ColorPicker, container, false);

            Button blue = view.FindViewById<Button>(Resource.Id.setblue);
            Button red = view.FindViewById<Button>(Resource.Id.setred);
            Button green = view.FindViewById<Button>(Resource.Id.setgreen);
            Button gray = view.FindViewById<Button>(Resource.Id.setgray);
            Button darkblue = view.FindViewById<Button>(Resource.Id.setdarkblue);
            Button darkred = view.FindViewById<Button>(Resource.Id.setdarkred);
            Button darkgreen = view.FindViewById<Button>(Resource.Id.setdarkgreen);
            Button purple = view.FindViewById<Button>(Resource.Id.setpurple);


            blue.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientBlue);
                setBackgrounds();
            };
            red.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientRed);
                setBackgrounds();
            };
            green.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientGreen);
                setBackgrounds();
            };
            gray.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientGrey);
                setBackgrounds();
            };
            purple.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientPurple);
                setBackgrounds();
            };
            darkblue.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientDarkBlue);
                setBackgrounds();
            };
            darkred.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientDarkRed);
                setBackgrounds();
            };
            darkgreen.Click += (sender, e) =>
            {
                AppBackground.background = Resources.GetDrawable(Resource.Drawable.GradientDarkGreen);
                setBackgrounds();
            };

            return view;
        }

        protected void setBackgrounds()
        {
            GridLayout background = view.FindViewById<GridLayout>(Resource.Id.colorgrid);
            background.Background = AppBackground.background;
        }

    }
}