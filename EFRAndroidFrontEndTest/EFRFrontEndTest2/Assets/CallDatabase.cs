using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EFRFrontEndTest2.Assets
{
    public struct Responce
    {
        public Responce(string responce, int code, string reason, JsonValue json)
        {
            m_responce = responce;
            m_reason = reason;
            m_code = code;
            m_json = json; // Holds the object
        }

        public string m_responce;
        public string m_reason;
        public int m_code;
        public JsonValue m_json;
    }

    class CallDatabase
    {
        public CallDatabase()
        {
            GetUserObject = SingleUserObject.getObject();
        }

        public async Task<Responce> RetreaveQuestionBlock()
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{\"user\": { \"session\": \"" + GetUserObject.SessionID + "\", " + GetUserObject.UserObjectForm() + " }}");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("PUT", "/q/request_block", bytestream);
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> CreateAccount(string username, string email, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\": { \"username\": \"" + username + "\", \"email\": \"" + email + "\", \"password\": \"" + password + "\"} }");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("POST", "/signup", bytestream);
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> FetchLogin(string username, string password)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\":{ \"username\":\"" + username + "\",\"password\":\"" + password + "\"} }");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("POST", "/login", bytestream, true);
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> UpdateUO()
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{\"user\": { \"session\": \"" + GetUserObject.SessionID + "\", " + GetUserObject.UserObjectForm() + " }}");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("PUT", "/update_uo", bytestream, true);
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> RenewSession()
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\": { \"session\": \"{" + GetUserObject.SessionID + "}\"} }");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("PUT", "/renew", bytestream, true); //True because session ID is in the UO and needs to be updated to be saved
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> ResetPassword(string username, string email)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes("{ \"user\" : { \"username\": \"" + username + "\", \"email\": \"" + email + "\" } }");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("POST", "/reset_password", bytestream, true); //True because session ID is in the UO and needs to be updated to be saved
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task<Responce> CheckUsername(string username, string email)
        {
            byte[] bytestream = Encoding.ASCII.GetBytes(@"{ ""user"": { ""username"": """ + username + @""", ""email"": """ + email + @""" } }");
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = APICall("POST", "/check_username", bytestream);
            await Task.WhenAny(task, Task.Delay(2000, cts.Token));
            CheckTask(task);

            return LastResponce;
        }

        public async Task APICall(string method, string uri, byte[] bytestream, bool need_UO = false)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://34.216.143.255:3002/api" + uri));
            request.ContentType = "application/json";
            request.Method = method;
            request.Timeout = 2000;
            request.GetRequestStream().Write(bytestream, 0, bytestream.Length); // Can cause an exception if phone is in airplane mode
            try
            {
                using (WebResponse response = request.GetResponse())
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
                    }
                }
            }
            catch (Exception e) // Exception is created here for debugging
            {
                int i = 0;
                i++;
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

        private void CheckTask(Task task)
        {
            if (task.Status == TaskStatus.WaitingForActivation)
            {
                LastResponce.m_responce = "Failure";
                LastResponce.m_reason = "This app is no longer supported.";
                LastResponce.m_code = 504;
            }
            else if (!task.IsCompleted || task.Status == TaskStatus.Faulted)
            {
                LastResponce.m_responce = "Failure";
                switch (task.Exception.InnerException.Message)
                {
                    case "Error: ConnectFailure (Network is unreachable)":
                        LastResponce.m_reason = "Unable to connect with the server. Check your internet connection and try again.";
                        LastResponce.m_code = 503; // Airplane mode or other similar issues
                        break;
                    case "The request timed out":
                        LastResponce.m_reason = "This app is no longer supported.";
                        LastResponce.m_code = 504; // Timeout error code
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateUserObject(JsonValue json)
        {
            try
            { // UO updating doesn't come with a session ID
                GetUserObject.SessionID = json["session_id"];
            }
            catch { }

            GetUserObject.Json = json;

            JsonValue user = json["userobject"];

            GetUserObject.Timestamp = user["timestamp"];

            JsonValue game = user["game_data"];
            GetUserObject.BlocksRemaining = game["blocksRemaining"];
            //m_userObject.CompletedBlocks =
            //JsonArray stuff = new JsonArray(game["completed_blocks"]);
            JsonArray array = (JsonArray)game["completed_blocks"];
            int[] numbers = new int[array.Count];

            // Extract numbers from JSON array.
            for (int i = 0; i < array.Count; ++i)
            {
                numbers[i] = array[i];
            }
            GetUserObject.CompletedBlocks = numbers;


            GetUserObject.Difficulty = game["difficulty"];
            GetUserObject.SubjectID = game["subject_id"];
            GetUserObject.SubjectName = game["subject_name"];
            GetUserObject.TotalDonated = game["totalDonated"];
            GetUserObject.TotalQuestions = game["totalQuestions"];

            user = user["user_data"];
            GetUserObject.CharityName = user["selected_charity"];
            GetUserObject.Email = user["email"];
            GetUserObject.FirstName = user["first_name"];
            GetUserObject.LastName = user["last_name"];
            GetUserObject.Username = user["username"];
            JsonArray array2 = (JsonArray)user["favorite_charities"];
            string[] strings = new string[array2.Count];

            for (int i = 0; i < array2.Count; ++i)
            {
                strings[i] = array2[i];
            }
            GetUserObject.FavoriteCharities = strings;
        }

        public UserObject GetUserObject { get; }
        public Responce responce { get { return LastResponce; } }

        private Responce LastResponce;
    }
}