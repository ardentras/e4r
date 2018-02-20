using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "settingsPageActivity")]
    public class settingsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingsPage);
            SeekBar sound = FindViewById<SeekBar>(Resource.Id.sound);
            SeekBar music = FindViewById<SeekBar>(Resource.Id.music);
            Button reset = FindViewById<Button>(Resource.Id.ResetButton);
            Switch note1 = FindViewById<Switch>(Resource.Id.note1);
            Switch note2 = FindViewById<Switch>(Resource.Id.note2);

            var localData = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            var edit = localData.Edit();

            sound.Progress = localData.GetInt("sound",0);
            music.Progress = localData.GetInt("music", 0);
            note1.Checked = localData.GetBoolean("notifications", false);
            note2.Checked = localData.GetBoolean("bubbleNotifications", false);
            reset.Click += (sender, e) =>
            {
                UserObject obj = SingleUserObject.getObject();
                obj.CompletedBlocks = new int[0];
                obj.MoneyEarned = 0;
                obj.QuestionsAnswered = 0;
                Android.Widget.Toast.MakeText(this,"data deleted",ToastLength.Short).Show();

            };
            sound.ProgressChanged += (ender, e) =>
            {
                edit.PutInt("sound", sound.Progress);
                edit.Commit();
            };
            music.ProgressChanged += (ender, e) =>
            {
                edit.PutInt("music", music.Progress);
                edit.Commit();
            };
            note1.Click += (ender, e) =>
            {
                edit.PutBoolean("notifications", note1.Checked);
                edit.Commit();
            };
            note2.Click += (ender, e) =>
            {
                edit.PutBoolean("bubbleNotifications", note2.Checked);
                edit.Commit();
            };

        }
    };
}