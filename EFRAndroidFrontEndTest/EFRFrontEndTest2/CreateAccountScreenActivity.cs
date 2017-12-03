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

namespace EFRFrontEndTest2
{
    [Activity(Label = "CreateAccountScreenActivity")]
    public class CreateAccountScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccountScreen);

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

            //For readability so the enum doesnt need to be accessed every time
            //Also acts as a bool to check if data/format is correct
            ViewStates visible = ViewStates.Visible;
            ViewStates invisible = ViewStates.Invisible;

            emailBox.FocusChange += (sender, e) =>
            {
                if (!emailformat(emailBox.Text))
                    emailErrorBox.Visibility = visible;
                else
                    emailErrorBox.Visibility = invisible;
            };

//TODO: Check to see if username is already made in database. Does Shaun have API functionality for this set up?
            usernameBox.FocusChange += (sender, e) =>
            {

            };

            passwordBoxOne.FocusChange += (sender, e) =>
            {
                if (passwordBoxOne.Text != passwordBoxTwo.Text)
                    passwordErrorBox.Visibility = visible;
                else
                    passwordErrorBox.Visibility = invisible;
            };

            passwordBoxTwo.FocusChange += (sender, e) =>
            {
                if (passwordBoxOne.Text != passwordBoxTwo.Text)
                    passwordErrorBox.Visibility = visible;
                else
                    passwordErrorBox.Visibility = invisible;
            };

            createAccountButton.Click += (sender, e) =>
            {
                //Checks if all information is correct, if not then a final error is shown
                //if so, then the account is created
                if (emailErrorBox.Visibility == visible
                || usernameErrorBox.Visibility == visible
                || passwordErrorBox.Visibility == visible)
                    finalErrorBox.Visibility = visible;
                else
                {
                    finalErrorBox.Visibility = invisible;
//TODO: Need to write create account functionality
                }
            };

            cancelButton.Click += (sender, e) =>
            {
                //Closes the current view
                Finish();
            };
        }

//TODO
        //Checks if the string is [something][@][provider][.][extention] format
        private bool emailformat(string email)
        {

            return false;
        }
    }
}