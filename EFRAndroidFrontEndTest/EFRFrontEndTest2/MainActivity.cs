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
            SetContentView(Resource.Layout.Template);
            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            EditText passBox = FindViewById<EditText>(Resource.Id.passwordBox);
            Button login = FindViewById<Button>(Resource.Id.loginButton);
            TextView createAccount = FindViewById<TextView>(Resource.Id.createAccountButton);
            

            //Made this async so while we wait for the server to reply, the main GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {
                // Fetch the login information asynchronously, parse the results, then update the screen.
                JsonValue json = await FetchLoginAsync(userBox.Text, passBox.Text);
                if (json.ToString().Contains("Success"))
                {
                    var intent = new Intent(this, typeof(HomeScreenActivity));
                    StartActivity(intent);
                }
                else
                {

                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
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

        private async Task<JsonValue> FetchLoginAsync(string username, string password)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://34.163.221.182:3002/api/login"));
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] JsonString = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            //byte[] JsonString = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"shaunrasmusen\",\"password\":\"defaultpass\"} }");
            request.GetRequestStream().Write(JsonString, 0, JsonString.Length);

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


//Great reference for calling event function out of main.
//SetContentView doesnt give a transition animation

/*createAccount.Click += OnTapGestureRecognizerTapped;
private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
{
    SetContentView(Resource.Layout.CreateAccountScreen);
}*/
