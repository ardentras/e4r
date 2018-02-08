using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "EFRFrontEndTest2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //Main function, called on run
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Removes title bar
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            CallDatabase database = new CallDatabase(this);

            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            EditText passBox = FindViewById<EditText>(Resource.Id.passwordBox);
            Button login = FindViewById<Button>(Resource.Id.loginButton);
            TextView createAccount = FindViewById<TextView>(Resource.Id.createAccountButton);


            //Made this async so while we wait for the server to reply, the main GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {

                // Fetch the login information asynchronously, parse the results, then update the screen.
                Responce responce = await database.FetchLogin(userBox.Text, passBox.Text);
                if (responce.m_responce == "Success")
                {
                    LocalArchive archive;
                    archive.SetUserData(database.GetUserObject.GetObjectString());

                    var intent = new Intent(this, typeof(HomeScreenActivity));
                    StartActivity(intent);
                }
                else
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("You couldn't log in");
                    alert.SetMessage("Invalid Credentials");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        passBox.Text = "";
                    });
                    alert.Show();
                }
            };

            //Calls new activity with transition animation. (Requires changing focus in axml so text isnt selected at the beginning)
            createAccount.Click += StartAccountActivity;
        }

        private void StartAccountActivity(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateAccountScreenActivity));
            StartActivity(intent);
        }
    }
}


// Testing

//Great reference for calling event function out of main.
//SetContentView doesnt give a transition animation

/*createAccount.Click += OnTapGestureRecognizerTapped;
private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
{
    SetContentView(Resource.Layout.CreateAccountScreen);
}*/

//"{\"action\": \"LOGIN\", \"code\": 200, \"response\": \"Success\", \"session_id\": \"855ce8c1-8577-4a84-9199-9b8efaebe8b3\", \"type\": \"GET\", \"user_object\": {\"game_data\": {\"completed_blocks\": [], \"difficulty\": \"0\", \"subject_id\": \"1\", \"subject_name\": \"\"}, \"timestamp\": \"\", \"user_data\": {\"charity_name\": \"\", \"first_name\": \"\", \"last_name\": \"\", \"username\": \"abc\"}}}"