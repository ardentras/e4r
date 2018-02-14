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

    public class UserObject
    {
        public UserObject() { }

        public string CompletedBlocks { get { return m_CompletedBlocks; } set { m_CompletedBlocks = value; } } //TODO: Make array once server functionality is implemented
        public string Difficulty { get { return m_Difficulty; } set { m_Difficulty = value; } }
        public int SubjectID { get { return m_SubjectID; } set { m_SubjectID = value; } }
        public string Timestamp { get { return m_Timestamp; } set { m_Timestamp = value; } }
        public string Charity { get { return m_Charity; } set { m_Charity = value; } }
        public string FirstName { get { return m_FirstName; } set { m_FirstName = value; } }
        public string LastName { get { return m_LastName; } set { m_LastName = value; } }
        public string Username { get { return m_Username; } set { m_Username = value; } }       //These should only be changed when loading a user object (I used this implementation for readability and consistancy)
        public string SessionID { get { return m_SessionID; } set { m_SessionID = value; } }    //These should only be changed when loading a user object

        private string m_SessionID;
        private string m_CompletedBlocks;
        private string m_Difficulty;
        private int m_SubjectID;
        private string m_Timestamp;
        private string m_Charity;
        private string m_FirstName;
        private string m_LastName;
        private string m_Username;

        public string GetObjectString()
        {
            string objectString = m_SessionID + ",";
            objectString += m_CompletedBlocks + ",";
            objectString += m_Difficulty + ",";
            objectString += m_SubjectID.ToString() + ",";
            objectString += m_Timestamp + ",";
            objectString += m_Charity + ",";
            objectString += m_FirstName + ",";
            objectString += m_LastName + ",";
            objectString += m_Username;
//TODO: If someone has time, replace with a more effecient process

            return objectString;
        }

        public bool SetObjectString(string objectString)
        {
            bool done = false;
            string[] list = objectString.Split(',');
            if (list.Length == 9)
            {
                try { m_SubjectID = Int32.Parse(list[3]); } //Ensures a corrupt string will not corrupt the object
                catch (Exception e) { return false; }
                m_SessionID = list[0];
                m_CompletedBlocks = list[1];
                m_Difficulty = list[2];
                m_Timestamp = list[4];
                m_Charity = list[5];
                m_FirstName = list[6];
                m_LastName = list[7];
                m_Username = list[8];
                done = true;
            }

            return done; //True if object is updated successfully
        }
    }


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
