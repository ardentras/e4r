using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class AccountSettings : Android.Support.V4.App.Fragment
    {
        private UserObject uo = SingleUserObject.getObject();
        private CallDatabase m_database;
        private TextView charity;
        private EditText firstName;
        private EditText lastName;

        private View view = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            m_database = new CallDatabase();
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnResume()
        {
            base.OnResume();
            setBackgrounds();
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
            view = inflater.Inflate(Resource.Layout.AccountSettings, container, false);
            setBackgrounds();

            TextView username = view.FindViewById<TextView>(Resource.Id.accountname);
            charity = view.FindViewById<TextView>(Resource.Id.currentcharityname);
            firstName = view.FindViewById<EditText>(Resource.Id.accountfirstname); ;
            lastName = view.FindViewById<EditText>(Resource.Id.accountlastname);
            TextView pwresetbtn = view.FindViewById<TextView>(Resource.Id.pwresetbtn);
            TextView gameresetbtn = view.FindViewById<TextView>(Resource.Id.gameresetbtn);
            Button savebtn = view.FindViewById<Button>(Resource.Id.accountsavebtn);
            username.Text = uo.Username;
            firstName.Hint = uo.FirstName;
            lastName.Hint = uo.LastName;
            if (charity.Text == "")
                charity.Text = "(Default) Red Cross";
            else
                charity.Text = uo.CharityName;

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
                        alert.SetButton("OK", (c, ev) =>
                        {
                            alert.Dismiss();
                        });
                        m_database.UpdateUO().Wait();
                        if (m_database.responce.m_reason == "User object out of date. Retrieving from database.")
                        {
                            alert.SetMessage("Looks like you've been playing on the computer.\nHeres the updated information.");
                            firstName.Text = uo.FirstName;
                            lastName.Text = uo.LastName;
                            if (charity.Text == "")
                                charity.Text = "None";
                            else
                                charity.Text = uo.CharityName;
                        }
                        else
                            alert.SetMessage("Information updated.");

                        alert.Show();
                    }
                }
            };

            pwresetbtn.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Password reset");
                alert.SetMessage("Are you sure you want to reset your password?");
                alert.SetButton("OK", async (c, ev) =>
                {
                    await ResetPassword();
                });
                alert.Show();
            };

            gameresetbtn.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Game reset");
                alert.SetMessage("Are you sure you want to reset your account?\nThis will reset all of your data.");
                alert.SetButton("OK", async (c, ev) =>
                {
                    await ResetAccount();
                });
                alert.Show();
            };
            
            lastName.Click += delegate
            {
                // Used for after user resets application
                lastName.SetCursorVisible(true);
            };

            return view;
        }

        private async Task<int> ResetPassword()
        {
            await m_database.ResetPassword(uo.Username, uo.Email);
            Responce responce = m_database.responce;
            AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Unable to reset password");
            alert.SetButton("OK", (c, ev) =>
            {

            });
            switch (responce.m_code)
            {
                case 200:
                    {
                        Toast.MakeText(Context, "Email sent!", ToastLength.Long).Show();
                        break;
                    }
                case 100:
                    {
                        alert.SetMessage(responce.m_reason);
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
            }
            return 0;
        }

        private async Task<int> ResetAccount()
        {
            uo.BlocksRemaining = 0;
            uo.CharityName = "";
            uo.CompletedBlocks = new int[1];
            uo.Difficulty = 1;
            uo.FavoriteCharities = new string[1];
            uo.FirstName = "";
            uo.LastName = "";
            uo.Level = 0;
            uo.SubjectID = 1;
            uo.SubjectName = "Mathematics";
            uo.TotalDonated = 0;
            uo.TotalQuestions = 0;
            await m_database.UpdateUO();
            Responce responce = m_database.responce;

            AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Unable to reset account");
            alert.SetButton("OK", (c, ev) =>
            { });

            switch (responce.m_code)
            {
                case 200:
                    {
                        Toast.MakeText(Context, "Account reset!", ToastLength.Long).Show();
                        firstName.Hint = "";
                        firstName.Text = "";
                        lastName.Hint = "";
                        lastName.Text = "";
                        charity.Text = "";
                        lastName.SetCursorVisible(false);

                        break;
                    }
                case 100:
                    {
                        alert.SetMessage(responce.m_reason);
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
            }
            return 0;
        }

        protected void setBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.AccountBackground);
                background.Background = AppBackground.background;
            }
        }
    }
}