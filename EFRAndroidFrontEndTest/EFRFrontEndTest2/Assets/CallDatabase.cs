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
        public Responce(string responce, int code, string reason, JsonValue json)
        {
            m_responce = responce;
            m_reason = reason;
            m_code = code;
            m_json = json;//holds the object
            
        }

        public string m_responce;
        public string m_reason;
        public int m_code;
        public JsonValue m_json;
    }

    class CallDatabase
    {
        public CallDatabase(Activity activity)
        {
            m_activity = activity;
            m_userObject = SingleUserObject.getObject();
        }

        public async Task<Responce> RetreaveQuestionBlock()
        {
            UserObject QuestionsBlock = SingleUserObject.getObject();
            byte[] bytestream = Encoding.ASCII.GetBytes("{\"user\": { \"session\":\"{"+ QuestionsBlock.SessionID+ "}\",\"userobject\": { "+QuestionsBlock.GetObjectString()+"}}}");
            return await APICall("PUT", "http://35.163.221.182:3002/api/q/request_block", bytestream);
        }

        public async Task<Responce> CreateAccount(string username, string email, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\": { \"username\": \"" + username + "\", \"email\": \"" + email + "\", \"password\": \"" + password + "\"} }");
            return await APICall("POST", "http://35.163.221.182:3002/api/signup", bytestream);
        }

        public async Task<Responce> FetchLogin(string username, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            return await APICall("POST", "http://35.163.221.182:3002/api/login", bytestream, true);
        }

        public async Task<Responce> RenewSession()
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("P \"user\": { \"session\": \"{" + m_userObject.SessionID + "}\"} }");
            return await APICall("PUT", "http://35.163.221.182:3002/api/renew", bytestream, true); //True because session ID is in the UO and needs to be updated to be saved
        }

// TODO: Update to username/email request when API allows for individual checking
        public async Task<Responce> CheckUsername(string username, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            return await APICall("POST", "http://35.163.221.182:3002/api/login", bytestream);
        }


        public async Task<Responce> APICall(string method, string Uri, byte [] bytestream, bool need_UO = false)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Uri));
            request.ContentType = "application/json";
            request.Method = method;
            request.GetRequestStream().Write(bytestream, 0, bytestream.Length);
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

                    if (LastResponce.m_code == 200 && need_UO == true)
                        CreateUserObject(jsonDoc);

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

            LastResponce = new Responce(response, code, reason,json);
        }

        private void CreateUserObject(JsonValue json)
        {
            m_userObject.SessionID = json["session_id"];

            JsonValue user = json["user_object"];
            m_userObject.Timestamp = user["timestamp"];

            JsonValue game = user["game_data"];
            m_userObject.BlocksRemaining = game["blocksRemaining"];
            //m_userObject.CompletedBlocks =
            //JsonArray stuff = new JsonArray(game["completed_blocks"]);
            m_userObject.Difficulty = game["difficulty"];
            m_userObject.SubjectID = game["subject_id"];
            m_userObject.SubjectName = game["subject_name"];
            m_userObject.TotalDonated = game["totalDonated"];
            m_userObject.TotalQuestions = game["totalQuestions"];

            user = user["user_data"];
            m_userObject.CharityName = user["charity_name"];
            m_userObject.Email = user["email"];
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



// User Object example (depricated)
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
 *                  "completed_blocks": [1, 7, 29],
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