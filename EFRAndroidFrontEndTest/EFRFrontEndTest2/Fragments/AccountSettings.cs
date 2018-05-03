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
    public class AccountSettings : Android.Support.V4.App.Fragment
    {
        private UserObject uo = SingleUserObject.getObject();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public static AccountSettings NewInstance()
        {
            AccountSettings temp = new AccountSettings();
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.AccountSettings, container, false);
            TextView username = view.FindViewById<TextView>(Resource.Id.accountname);
            EditText firstName = view.FindViewById<EditText>(Resource.Id.accountfirstname);
            EditText lastName = view.FindViewById<EditText>(Resource.Id.accountlastname);
            TextView charity = view.FindViewById<TextView>(Resource.Id.currentcharityname);
            TextView pwresetbtn = view.FindViewById<TextView>(Resource.Id.pwresetbtn);
            TextView gameresetbtn = view.FindViewById<TextView>(Resource.Id.gameresetbtn);
            Button savebtn = view.FindViewById<Button>(Resource.Id.accountsavebtn);
            username.Text = uo.Username;
            firstName.Hint = uo.FirstName;
            lastName.Hint = uo.LastName;
            if (charity.Text == "")
            {
                charity.Text = "None";
            }
            else
            {
                charity.Text = uo.CharityName;
            }
            savebtn.Click += delegate
            {
                if (firstName.Text != "" || lastName.Text != "")
                {
                    if (firstName.Text != uo.FirstName || lastName.Text != uo.LastName)
                    {
                        uo.FirstName = firstName.Text;
                        uo.LastName = lastName.Text;
                        AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Name change");
                        alert.SetMessage("Information updated.");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            alert.Dismiss();
                        });
                        alert.Show();
                    }
                }
            };
            pwresetbtn.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Password reset");
                alert.SetMessage("are you sure?");
                alert.SetButton("OK", (c, ev) =>
                {
                    //put password reset functionality here
                });
                alert.Show();
            };
            gameresetbtn.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Game reset");
                alert.SetMessage("are you sure?");
                alert.SetButton("OK", (c, ev) =>
                {
                    //put game reset functionality here
                });
                alert.Show();
            };
            return view;
        }
    }
}