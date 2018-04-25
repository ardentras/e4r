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
            string stream = "{\"user\": { \"session\": \"" + m_userObject.SessionID + "\", " + m_userObject.UserObjectForm() + " }}";
            byte[] bytestream = Encoding.ASCII.GetBytes(stream);
            return await APICall("PUT", "/q/request_block", bytestream);
        }

        public async Task<Responce> CreateAccount(string username, string email, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\": { \"username\": \"" + username + "\", \"email\": \"" + email + "\", \"password\": \"" + password + "\"} }");
            return await APICall("POST", "/signup", bytestream);
        }

        public async Task<Responce> FetchLogin(string username, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            return await APICall("POST", "/login", bytestream, true);
        }

        public async Task<Responce> RenewSession()
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("P \"user\": { \"session\": \"{" + m_userObject.SessionID + "}\"} }");
            return await APICall("PUT", "/renew", bytestream, true); //True because session ID is in the UO and needs to be updated to be saved
        }

        // TODO: Update to username/email request when API allows for individual checking
        public async Task<Responce> CheckUsername(string username, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            return await APICall("POST", "/login", bytestream);
        }


        public async Task<Responce> APICall(string method, string uri, byte[] bytestream, bool need_UO = false)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://34.216.143.255:3002/api" + uri));
            request.ContentType = "application/json";
            request.Method = method;
            try
            {
                request.Timeout = 2000; // Two second timeout. Timeout it in milliseconds
                                       // Send the request to the server and wait for the response:
                request.GetRequestStream().Write(bytestream, 0, bytestream.Length); // Can cause an exception if phone is in airplane mode
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
            catch (Exception e)
            {
                LastResponce.m_responce = "Failure";
                switch (e.Message)
                {
                    case "Error: ConnectFailure (Network is unreachable)":
                        LastResponce.m_reason = "Unable to connect to network";
                        LastResponce.m_code = 503; // Airplane mode or other similar issues
                        break;
                    case "The request timed out":
                        LastResponce.m_reason = "HTTP Request Timeout";
                        LastResponce.m_code = 504; // Timeout error code
                        break;
                    default:
                        break;
                }
                return LastResponce;
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

            LastResponce = new Responce(response, code, reason, json);
        }

        private void CreateUserObject(JsonValue json)
        {
            m_userObject.SessionID = json["session_id"];
            m_userObject.Json = json;
            JsonValue user = json["user_object"];
            m_userObject.Timestamp = user["timestamp"];

            JsonValue game = user["game_data"];
            m_userObject.BlocksRemaining = game["blocksRemaining"];
            //m_userObject.CompletedBlocks =
            //JsonArray stuff = new JsonArray(game["completed_blocks"]);
            JsonArray array = (JsonArray)game["completed_blocks"];
            int[] numbers = new int[array.Count];

            // Extract numbers from JSON array.
            for (int i = 0; i < array.Count; ++i)
            {
                numbers[i] = array[i];
            }
            m_userObject.CompletedBlocks = numbers;


            m_userObject.Difficulty = game["difficulty"];
            m_userObject.SubjectID = game["subject_id"];
            m_userObject.SubjectName = game["subject_name"];
            m_userObject.TotalDonated = game["totalDonated"];
            m_userObject.TotalQuestions = game["totalQuestions"];

            user = user["user_data"];
            m_userObject.CharityName = user["selected_charity"];
            m_userObject.Email = user["email"];
            m_userObject.FirstName = user["first_name"];
            m_userObject.LastName = user["last_name"];
            m_userObject.Username = user["username"];
            JsonArray array2 = (JsonArray)user["favorite_charities"];
            string[] strings = new string[array2.Count];

            for (int i = 0; i < array2.Count; ++i)
            {
                strings[i] = array2[i];
            }
            m_userObject.FavoriteCharities = strings;
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
