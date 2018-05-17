using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class GeneralSettings : Android.Support.V4.App.Fragment
    {
        View view = null;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnResume()
        {
            base.OnResume();
            setBackgrounds();
        }

        public static GeneralSettings NewInstance()
        {
            GeneralSettings temp = new GeneralSettings();
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.settingsPage, container, false);
            setBackgrounds();

            SeekBar sound = view.FindViewById<SeekBar>(Resource.Id.sound);
            SeekBar music = view.FindViewById<SeekBar>(Resource.Id.music);
            Button reset = view.FindViewById<Button>(Resource.Id.ResetButton);
            Button achievements = view.FindViewById<Button>(Resource.Id.AchievementsButton);
            Switch note1 = view.FindViewById<Switch>(Resource.Id.note1);
            Switch note2 = view.FindViewById<Switch>(Resource.Id.note2);
            //object to store local data
            var localData = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            var edit = localData.Edit();
            //how to pull data
            sound.Progress = localData.GetInt("sound", 100);
            music.Progress = localData.GetInt("music", 100);
            note1.Checked = localData.GetBoolean("notifications", false);
            note2.Checked = localData.GetBoolean("bubbleNotifications", false);
            reset.Click += (sender, e) =>
            {
                View view = LayoutInflater.Inflate(Resource.Layout.DeleteDataPopup, null);
                AlertDialog builder = new AlertDialog.Builder(note1.Context).Create();
                builder.SetView(view);
                builder.SetCanceledOnTouchOutside(true);
                EditText textUsername = view.FindViewById<EditText>(Resource.Id.textUsername);
                Button buttonDelete = view.FindViewById<Button>(Resource.Id.DeleteButton);
                Button buttonCancel = view.FindViewById<Button>(Resource.Id.CancelButton);
                buttonCancel.Click += delegate
                {
                    builder.Dismiss();
                };
                buttonDelete.Click += delegate
                {
                    UserObject obj = SingleUserObject.getObject();
                    obj.CompletedBlocks = new int[0];
                    obj.TotalDonated = 0;
                    obj.TotalQuestions = 0;
                    Android.Widget.Toast.MakeText(textUsername.Context, "data deleted", ToastLength.Short).Show();
                    builder.Dismiss();
                };
                builder.Show();

            };
            sound.ProgressChanged += (sender, e) =>
            {
                //how to save data
                edit.PutInt("sound", sound.Progress);
                edit.Commit();
            };
            music.ProgressChanged += (sender, e) =>
            {
                edit.PutInt("music", music.Progress);
                edit.Commit();
            };
            note1.Click += (sender, e) =>
            {
                edit.PutBoolean("notifications", note1.Checked);
                edit.Commit();
            };
            note2.Click += (sender, e) =>
            {
                edit.PutBoolean("bubbleNotifications", note2.Checked);
                edit.Commit();
            };
            achievements.Click += (sender, e) =>
            {
            };


            return view;
        }

        protected void setBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.bubble_layout);
                background.Background = AppBackground.background;
            }
        }
    }
}