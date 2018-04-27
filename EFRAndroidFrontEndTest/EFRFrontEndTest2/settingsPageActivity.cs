using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "settingsPageActivity")]
    public class settingsPageActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            setBackground();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settingsPage);
            setBackground();

            SeekBar sound = FindViewById<SeekBar>(Resource.Id.sound);
            SeekBar music = FindViewById<SeekBar>(Resource.Id.music);
            Button reset = FindViewById<Button>(Resource.Id.ResetButton);
            Button achievements = FindViewById<Button>(Resource.Id.AchievementsButton);
            Button background = FindViewById<Button>(Resource.Id.BackgroundButton);
            Switch note1 = FindViewById<Switch>(Resource.Id.note1);
            Switch note2 = FindViewById<Switch>(Resource.Id.note2);
            //object to store local data
            var localData = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            var edit = localData.Edit();
            //how to pull data
            sound.Progress = localData.GetInt("sound",100);
            music.Progress = localData.GetInt("music", 100);
            note1.Checked = localData.GetBoolean("notifications", false);
            note2.Checked = localData.GetBoolean("bubbleNotifications", false);
            reset.Click += (sender, e) =>
            {
                View view = LayoutInflater.Inflate(Resource.Layout.DeleteDataPopup, null);
                AlertDialog builder = new AlertDialog.Builder(this).Create();
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
                    Android.Widget.Toast.MakeText(this, "data deleted", ToastLength.Short).Show();
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
                var intent = new Intent(this, typeof(AchievementsPageActivity));
                StartActivity(intent);
            };
            background.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ColorPickerActivity));
                StartActivity(intent);
            };
        }
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                GridLayout background = FindViewById<GridLayout>(Resource.Id.settingsgrid);
                background.Background = AppBackground.background;
            }
        }
    };
}