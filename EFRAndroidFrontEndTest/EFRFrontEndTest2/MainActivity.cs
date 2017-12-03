using System;
using System.Json;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using Android.Views;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;

namespace EFRFrontEndTest2
{
    [Activity(Label = "EFRFrontEndTest2", MainLauncher = true)]
    public class MainActivity : Activity
    {

        /**/


        //Pops up the keyboard and bind it to the currently selected object.
        //For manual calls
        public void ShowKeyboard(View pView)
        {
            pView.RequestFocus();
            InputMethodManager inputMethodManager = Application.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(pView, ShowFlags.Forced);
            inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
        }

        //Makes the keyboard disappear. Only needs to be called on an object that uses the ShowKeyboard() function.
        public void HideKeyboard(View pView)
        {
            InputMethodManager inputMethodManager = Application.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(pView.WindowToken, HideSoftInputFlags.None);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            EditText passBox = FindViewById<EditText>(Resource.Id.passwordBox);
            EditText userCover = FindViewById<EditText>(Resource.Id.usernameCover);
            EditText passCover = FindViewById<EditText>(Resource.Id.passwordCover);
            Button login = FindViewById<Button>(Resource.Id.loginButton);
            Button creatAccount = FindViewById<Button>(Resource.Id.createAccountButton);

            //Sets up an overlay over username/password and makes it disappear when clicked.
//TODO: Needs to be redone to keep the functionality but make it less complicated.
            ViewStates visible = ViewStates.Visible;
            ViewStates invisible = ViewStates.Invisible;

            userBox.Visibility = invisible;
            passBox.Visibility = invisible;
            userCover.Click += (sender, e) =>
            {
                if (userBox.Visibility == invisible)
                {
                    userBox.Visibility = visible;
                    userCover.Visibility = invisible;
                    userBox.RequestFocus();
                    ShowKeyboard(userBox);
                }
            };
            passCover.Click += (sender, e) =>
            {
                if (passBox.Visibility == invisible)
                {
                    passBox.Visibility = visible;
                    passCover.Visibility = invisible;
                    passBox.RequestFocus();
                    ShowKeyboard(passBox);
                }
            };

            //Made this async so while we wait for the server to reply, the GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {
                string url = "http://34.208.210.218:3002/api/login";
                // Fetch the login information asynchronously, 
                // parse the results, then update the screen:
                JsonValue json = await FetchLoginAsync(url, userBox.Text, passBox.Text);
                var stuff = json.ToString().Split(',');
                // ParseAndDisplay (json);
            };

            creatAccount.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateAccountScreenActivity));
                StartActivity(intent);
            };
        }

        //An override that is supposed to be called when the app goes into the background.
        //Meant to hide the keyboard if ShowKeyboard() function is called and the user presses
        //the home button or opens a different app.
        //Not working as of 12/2/17
        protected void onPause()
        {
            this.onPause();

            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            if (userBox.HasFocus)
                HideKeyboard(FindViewById<EditText>(Resource.Id.usernameBox));
        }

        private async Task<JsonValue> FetchLoginAsync(string url, string username, string password)
        {
            //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            //byte[] temp = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            byte[] temp = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"shaunrasmusen\",\"password\":\"defaultpass\"} }");
            request.GetRequestStream().Write(temp, 0, temp.Length);

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
    }
}

