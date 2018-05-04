using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Threading.Tasks;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "E4R")]
    public class LoginScreenActivity : Activity
    {
        private CallDatabase m_database;
        //Main function, called on run
        protected override void OnCreate(Bundle savedInstanceState)
        {
            m_database = new CallDatabase();

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
            //Used as a switch to keep multiple async calls to a new activity
            bool clicked = false;

            //Made this async so while we wait for the server to reply, the main GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {
                if (!clicked)
                {
                    clicked = true;
                    // Fetch the login information asynchronously, parse the results, then update the screen.
                    Responce responce = await m_database.FetchLogin(userBox.Text, passBox.Text);
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("You couldn't log in");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        passBox.Text = "";
                    });
                    switch (responce.m_code)
                    {
                        case 200:
                            {
                                //m_database.GetUserObject.Save(this);
                                //changed into dashboard activity for new userdashboard, only test
                                var intent = new Intent(this, typeof(BottomMenuTest));
                                StartActivity(intent);
                                //finish will destory this page
                                Finish();
                                break;
                            }
                        case 401:
                            {
                                alert.SetMessage("Invalid username/password");
                                alert.Show();
                                break;
                            }
                        case 428:
                            {
                                alert.SetMessage("Check your email to verify your account");
                                alert.Show();
                                break;
                            }
                        case 503: // Network issues
                        case 504:
                            {

                                alert.SetMessage(responce.m_reason);
                                alert.Show();
                                break;
                            }
                        default:
                            {
                                alert.SetMessage("Unknown Error");
                                alert.Show();
                                break;
                            }
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
                        //changed into dashboard activity for new userdashboard, only test
                        var intent = new Intent(this, typeof(BottomMenuTest));
                        StartActivity(intent);
                        //finish will destory this page
                        Finish();
                    });
                    alert.Show();
                    alert.DismissEvent += (sndr, eF) =>
                    {
                        //changed into dashboard activity for new userdashboard, only test
                        var intent = new Intent(this, typeof(BottomMenuTest));
                        StartActivity(intent);
                        //finish will destory this page
                        Finish();
                    };
                    clicked = false;
                }
            };

            forgotPassword.Click += (sender, e) => { ShowForgotPasswordScreen(); };
            //Calls new activity with transition animation. (Requires changing focus in axml so text isnt selected at the beginning)
            createAccount.Click += StartCreateAccountActivity;
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

        /***************************************************************************************************************************
         * Author: Kevin Xu - if you change anything, update this!!!
         * Function: OnBackPressed
         * Purpose: Prevent exiting the app by clicking the back button.(should not happen!)
        ****************************************************************************************************************************/
        public override void OnBackPressed()
        {
            return;
        }

        //TODO: Implement once Shaun has created a password recovery API call
        private void SendAccountRecoveryEmail(string username)
        {

        }
    }
}
