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


//TODO: Make this implement a singleton pattern
namespace EFRFrontEndTest2.Assets 
{
    public struct Responce
    {
        public Responce(string responce, int code, string reason)
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
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString()); //For debugging
                    SaveLastResponce(jsonDoc);
                    if (LastResponce.m_code == 200)
                        CreateUserObject(jsonDoc);

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
                    SaveLastResponce(jsonDoc);

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
                    SaveLastResponce(jsonDoc);

                    // Return the JSON document:
                    return LastResponce;
                }
            }
        }

        private void SaveLastResponce(JsonValue json)
        {
            string response = json["response"];
            int code = json["code"];
            string reason = "None";
// TODO: Ask Shaun if he can link code to the existance of reason. Or at lease have a reason in every response.
            try { reason = json["reason"]; } // Reason isnt linked to error code so must be checked every time.
            catch (Exception) { }

            LastResponce = new Responce(response, code, reason);
        }
        private void CreateUserObject(JsonValue json)
        {
            JsonValue user = json["user_object"];
            JsonValue game = user["game_data"];
//TODO: Fix once implemented
           // m_userObject.CompletedBlocks = game["completed_blocks"];
            m_userObject.Difficulty = game["difficulty"];
            m_userObject.SubjectID = game["subject_id"];
            m_userObject.Timestamp = user["timestamp"];
            user = user["user_data"];
            m_userObject.Charity = user["charity_name"];
            m_userObject.FirstName = user["first_name"];
            m_userObject.LastName = user["last_name"];
            m_userObject.Username = user["username"];
        }

        public UserObject GetUserObject { get { return m_userObject; } }
        public Responce responce { get { return LastResponce; } }

        private Activity m_activity;
        private UserObject m_userObject;
        private Responce LastResponce;
    }
}


/* "{
 *      "action": "LOGIN",
 *      "code": 200,
 *      "response": "Success",
 *      "session_id": "77b96593-1516-481e-8479-e944c83ff587",
 *      "type": "GET",
 *          "user_object": 
 *          {
 *              "game_data": 
 *              {
 *                  "completed_blocks": [],
 *                  "difficulty": "0",
 *                  "subject_id": "1",
 *                  "subject_name": ""
 *              },
 *          "timestamp": "",
 *          "user_data": 
 *          {
 *              "charity_name": "",
 *              "first_name": "",
 *              "last_name": "",
 *              "username": "abc"
 *          }
 *      }
 * }"
 */

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
