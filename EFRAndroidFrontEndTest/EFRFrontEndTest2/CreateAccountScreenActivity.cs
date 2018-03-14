using Android.App;
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
    [Activity(Label = "CreateAccountScreenActivity")]
    public class CreateAccountScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Removes title bar
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccountScreen);
            CallDatabase database = new CallDatabase(this);

            EditText emailBox = FindViewById<EditText>(Resource.Id.EmailBox);
            EditText usernameBox = FindViewById<EditText>(Resource.Id.UsernameBox);
            EditText passwordBoxOne = FindViewById<EditText>(Resource.Id.PasswordBoxOne);
            EditText passwordBoxTwo = FindViewById<EditText>(Resource.Id.PasswordBoxTwo);

            Button createAccountButton = FindViewById<Button>(Resource.Id.CreateButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.CancelButton);

            //Used for notifying user when a field entry is not permitted
            TextView emailErrorBox = FindViewById<TextView>(Resource.Id.EmailErrorBox);
            TextView usernameErrorBox = FindViewById<TextView>(Resource.Id.UsernameErrorBox);
            TextView passwordErrorBox = FindViewById<TextView>(Resource.Id.PasswordErrorBox);
            TextView finalErrorBox = FindViewById<TextView>(Resource.Id.FinalErrorBox);


            //Ensures the boxes do not expand from their original size when too many characters are entered
            //Keeps this dynamic for screens with different aspect ratios
            emailBox.SetMaxWidth(emailBox.Width);
            usernameBox.SetMaxWidth(usernameBox.Width);
            passwordBoxOne.SetMaxWidth(passwordBoxOne.Width);
            passwordBoxTwo.SetMaxWidth(passwordBoxTwo.Width);

            //For readability so the enum doesnt need to be accessed every time
            //Also acts as a bool to check if data/format is correct
            ViewStates visible = ViewStates.Visible;
            ViewStates invisible = ViewStates.Invisible;

            emailBox.FocusChange += (sender, e) =>
            {
                if (!emailformat(emailBox.Text) && emailBox.Text.Length > 0)
                    emailErrorBox.Visibility = visible;
                else
                    emailErrorBox.Visibility = invisible;
            };
            
            usernameBox.FocusChange += async (sender, e) =>
            {
                if (usernameBox.Text.Length > 0)
                {
                    //Pings the server to check if the username already exists
                    Responce responce = await database.CheckUsername(usernameBox.Text, emailBox.Text);

                    //Parses JSON responce to see if (User not found) returns, if so then username is acceptable
                    if (responce.m_responce == "Failed")
                        usernameErrorBox.Visibility = invisible;
                    else
                        usernameErrorBox.Visibility = visible;
                    ;
                }
            };

            //Error appears if the passwords arent the same but both password fields have text entered into them
            passwordBoxOne.FocusChange += (sender, e) =>
            {
                if (passwordBoxOne.Text != passwordBoxTwo.Text && passwordBoxTwo.Text.Length > 0 && passwordBoxOne.Text.Length > 0)
                    passwordErrorBox.Visibility = visible;
                else
                    passwordErrorBox.Visibility = invisible;
            };

            //Error appears if the passwords arent the same but both password fields have text entered into them
            passwordBoxTwo.FocusChange += (sender, e) =>
            {
                if (passwordBoxOne.Text != passwordBoxTwo.Text && passwordBoxTwo.Text.Length > 0 && passwordBoxOne.Text.Length > 0)
                    passwordErrorBox.Visibility = visible;
                else
                    passwordErrorBox.Visibility = invisible;
            };

            createAccountButton.Click += async (sender, e) =>
            {
                //Final check on password cases, this doesnt care if a field is blank
                if (passwordBoxOne.Text != passwordBoxTwo.Text)
                    passwordErrorBox.Visibility = visible;

                //Checks if all information is correct, if not then a final error is shown
                //if so, then the account is created
                if (emailErrorBox.Visibility == visible
                || usernameErrorBox.Visibility == visible
                || passwordErrorBox.Visibility == visible)
                    finalErrorBox.Visibility = visible;
                else
                {
                    finalErrorBox.Visibility = invisible;
                    Responce responce = await database.CreateAccount(usernameBox.Text, emailBox.Text, passwordBoxOne.Text);
                    if (responce.m_responce == "Succeed")
                    {
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("");
                        alert.SetMessage("Account Created!\nPlease check your email to confirm your account.");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            Finish();
                        });
                        alert.Show();
                    }
                    else //This should never be called if it does, tell Jacob
                    {
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("");
                        alert.SetMessage("username or email unavalable");
                        alert.SetButton("OK", (c, ev) =>
                        {
                        });
                        alert.Show();
                    }
                }
            };

            cancelButton.Click += (sender, e) =>
            {
                //Closes the current view
                Finish();
            };
        }

        //Checks if the string is [something][@][provider][.][extention] format
        private bool emailformat(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}



//look up data context for windows (updated data in real time)