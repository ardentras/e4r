using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EFRFrontEndTest2.Assets
{
    public struct Responce
    {
        public Responce(string responce, int code, string reason = "None")
        {
            m_responce = responce;
            m_reason = reason;
            m_code = code;
        }

        public string m_responce;
        public string m_reason;
        public int m_code;
    }

    class CallDatabase
    {
        public CallDatabase(Activity activity)
        {
            m_activity = activity;
            m_userObject = new UserObject();
        }

        public async Task<Responce> FetchLogin(string username, string password)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://35.163.221.182:3002/api/login"));
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
                    string [] list = ParseJson(jsonDoc.ToString());
                    if (list.Any("game_data".Contains))
                        CreateUserObject(list);

                    return LastResponce;
                }
            }
        }

        public async Task<Responce> CheckUsername(string username, string password)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://35.163.221.182:3002/api/login"));
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
                    ParseJson(jsonDoc.ToString());

                    // Return the JSON document:
                    return LastResponce;
                }
            }
        }

        public async Task<Responce> CreateAccount(string username, string email, string password)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://35.163.221.182:3002/api/signup"));
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] JsonString = Encoding.ASCII.GetBytes("{ \"user\": { \"username\": \"" + username + "\", \"email\": \"" + email + "\", \"password\": \"" + password + "\"} }");
            request.GetRequestStream().Write(JsonString, 0, JsonString.Length);
            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = JsonObject.Load(stream);
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    ParseJson(jsonDoc.ToString());

                    // Return the JSON document:
                    return LastResponce;
                }
            }
        }

        private string [] ParseJson(string json)
        {
            json = json.Replace("\"", string.Empty);
            json = json.Trim(new char[] { '{', '}'});
            string[] checks = new[] { "action: ", ", code: ", ", response: ", ", session_id: ", ", type: ", ", user_object: ", ", timestamp: ", ", user_data: " };
            string[] list = json.Split(checks, StringSplitOptions.None);

            LastResponce.m_reason = list[1];
            Int32.TryParse(list[2], out LastResponce.m_code);
            LastResponce.m_responce = list[3];

            return list;
        }

        private void CreateUserObject(string [] list)
        {
            list[6] = list[6].Trim(new char[] { '{', '}' });
            string[] checks = new[] { "game_data: {completed_blocks: ", ", difficulty: ", ", subject_id: ", ", subject_name: " };
            string[] game_data = list[6].Split(checks, StringSplitOptions.None);
            checks = new[] { "{charity_name: ", ", first_name: ", ", last_name: ", ", username: " };
            string[] user_data = list[8].Split(checks, StringSplitOptions.None);

            int ID;
            Int32.TryParse(game_data[2], out ID);
            m_userObject.SubjectID = ID; //TODO: Talk to Shaun about removing subject_name, we will have it saved locally

            m_userObject.CompletedBlocks = game_data[0];
            m_userObject.Difficulty = game_data[1];
            m_userObject.Timestamp = list[7];
            m_userObject.Charity = user_data[0]; //TODO: Talk to Shaun about using an ID instead of a name
            m_userObject.FirstName = user_data[1];
            m_userObject.LastName = user_data[2];
            m_userObject.Username = user_data[3];
            m_userObject.SessionID = list[4];
        }

        public UserObject GetUserObject { get { return m_userObject; } }
        public Responce responce { get { return LastResponce; } }

        private Activity m_activity;
        private UserObject m_userObject;
        private Responce LastResponce;
    }
}

//"{\"action\": \"LOGIN\", \"code\": 200, \"response\": \"Success\", \"session_id\": \"855ce8c1-8577-4a84-9199-9b8efaebe8b3\", \"type\": \"GET\", 
//\"user_object\": {\"game_data\": {\"completed_blocks\": [], \"difficulty\": \"0\", \"subject_id\": \"1\", \"subject_name\": \"\"}, \"timestamp\": \"\", \"user_data\": {\"charity_name\": \"\", \"first_name\": \"\", \"last_name\": \"\", \"username\": \"abc\"}}}"


/*
list
{string[9]}
    [0]: ""
    [1]: "LOGIN"
    [2]: "200"
    [3]: "Success"
    [4]: "1bffb7a4-4a1d-4695-b9a5-1d7685d1bfbc"
    [5]: "GET"
    [6]: "{game_data: {completed_blocks: [], difficulty: 0, subject_id: 1, subject_name: }"
    [7]: ""
    [8]: "{charity_name: , first_name: , last_name: , username: abc"
*/
