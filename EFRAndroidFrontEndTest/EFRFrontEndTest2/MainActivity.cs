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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.NoTitle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Template);
            EditText userBox = FindViewById<EditText>(Resource.Id.usernameBox);
            EditText passBox = FindViewById<EditText>(Resource.Id.passwordBox);
            Button login = FindViewById<Button>(Resource.Id.loginButton);
            TextView createAccount = FindViewById<TextView>(Resource.Id.createAccountButton);
            

            //Made this async so while we wait for the server to reply, the GUI thread doesn't freeze up.
            login.Click += async (sender, e) =>
            {
                string url = "http://34.208.210.218:3002/api/login";
                // Fetch the login information asynchronously, parse the results, then update the screen.
                JsonValue json = await FetchLoginAsync(url, userBox.Text, passBox.Text);
                var stuff = json.ToString().Split(',');
            };

            //Calls new activity with transition animation. (Requires changing focus in axml so text isnt selected at the beginning)
            createAccount.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateAccountScreenActivity));
                StartActivity(intent);
            };
        }

        private async Task<JsonValue> FetchLoginAsync(string url, string username, string password)
        {
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


//Great reference for calling event function out of main. Though transition is immediate and not an animated transition.

/*createAccount.Click += OnTapGestureRecognizerTapped;
private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
{
    SetContentView(Resource.Layout.CreateAccountScreen);
}*/
