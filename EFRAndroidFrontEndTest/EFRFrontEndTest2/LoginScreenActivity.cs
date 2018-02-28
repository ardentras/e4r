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

using Acr.UserDialogs;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "EFRFrontEndTest2", MainLauncher = true)]
    public class LoginScreenActivity : Activity
    {
        //Main function, called on run
        protected override void OnCreate(Bundle savedInstanceState)
        {
            LocalArchive m_archive = new LocalArchive(this);
            CallDatabase m_database = new CallDatabase(this);
            //m_database.GetUserObject.Load(this);
           // Task.Run(async () => { await RenewSessionAsync(); });

            //Removes title bar
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPageScreen);

            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            EditText passBox = FindViewById<EditText>(Resource.Id.passwordBox);
            Button login = FindViewById<Button>(Resource.Id.loginButton);
            Button guestLogin = FindViewById<Button>(Resource.Id.guestLoginButton);
            TextView forgotPassword = FindViewById<TextView>(Resource.Id.ForgotPasswordButton);
            TextView createAccount = FindViewById<TextView>(Resource.Id.createAccountButton);
            bool clicked = false;

            //Made this async so while we wait for the server to reply, the main GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {
                if (!clicked)
                {
                    clicked = true;
                    // Fetch the login information asynchronously, parse the results, then update the screen.
                    Responce responce = await m_database.FetchLogin(userBox.Text, passBox.Text);

                    if (responce.m_responce == "Success")
                    {
                        m_database.GetUserObject.Save(this);
                        var intent = new Intent(this, typeof(UserDashboardActivity));
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
                    clicked = false;
                }
            };

            guestLogin.Click += (sender, e) =>
            {
                if (!clicked)
                {
                    clicked = true;
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Guest account active");
                    alert.SetMessage("Your progress will not be saved");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        var intent = new Intent(this, typeof(UserDashboardActivity));
                        StartActivity(intent);
                    });
                    alert.Show();
                    alert.DismissEvent += (sndr, eF) =>
                    {
                        var intent = new Intent(this, typeof(UserDashboardActivity));
                        StartActivity(intent);
                    };
                    clicked = false;
                }

            };

            forgotPassword.Click += (sender, e) => { ShowForgotPasswordScreen(); };
            //Calls new activity with transition animation. (Requires changing focus in axml so text isnt selected at the beginning)
            createAccount.Click += StartCreateAccountActivity;
        }

        private async Task<bool> RenewSessionAsync()
        {
            Responce responce = await m_database.RenewSession();
            if (responce.m_responce == "Success")
            {
                m_database.GetUserObject.Load(this);
                var intent = new Intent(this, typeof(UserDashboardActivity));
                StartActivity(intent);
            }

            return true;
        }

        private void StartCreateAccountActivity(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateAccountScreenActivity));
            StartActivity(intent);
        }

        void ShowForgotPasswordScreen()
        {
            //Inflate layout
            View view = LayoutInflater.Inflate(Resource.Layout.ForgotPasswordAlertDialogScreen, null);
            AlertDialog builder = new AlertDialog.Builder(this).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);
            EditText textUsername = view.FindViewById<EditText>(Resource.Id.textUsername);
            Button buttonSubmit = view.FindViewById<Button>(Resource.Id.buttonSubmit);
            Button buttonCancel = view.FindViewById<Button>(Resource.Id.buttonCancel);
            buttonCancel.Click += delegate {
                builder.Dismiss();
            };
            buttonSubmit.Click += delegate
            {
                if (textUsername.Text.Length > 0)
                {
                    SendAccountRecoveryEmail(textUsername.Text);
                    builder.Dismiss();
                    Toast.MakeText(this, "Email sent!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Please enter your username", ToastLength.Short).Show();
                }
            };
            builder.Show();
        }

//TODO: Implement once Shaun has created a password recovery API call
        private void SendAccountRecoveryEmail(string username)
        {

        }


        LocalArchive m_archive;
        CallDatabase m_database;
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
