using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EFRFrontEndTest2.Assets
{
    public class UserObject
    {
        public JsonValue Json { get { return m_json; } set { m_json = value; } }
        public string SessionID { get { return m_SessionID; } set { m_SessionID = value; } }
        public string Timestamp { get { return m_Timestamp; } set { m_Timestamp = value; } }
        public int BlocksRemaining { get { return m_BlocksRemaining; } set { m_BlocksRemaining = value; } }
        public int[] CompletedBlocks { get { return m_CompletedBlocks; } set { m_CompletedBlocks = value; } }
        public int Difficulty { get { return m_Difficulty; } set { m_Difficulty = value; } }
        public int SubjectID { get { return m_SubjectID; } set { m_SubjectID = value; } }
        public string SubjectName { get { return m_SubjectName; } set { m_SubjectName = value; } }
        public double TotalDonated { get { return m_TotalDonated; } set { m_TotalDonated = value; } }
        public int TotalQuestions { get { return m_TotalQuestions; } set { m_TotalQuestions = value; } }
        public string CharityName { get { return m_CharityName; } set { m_CharityName = value; } }
        public string Email { get { return m_Email; } set { m_Email = value; } }
        public string FirstName { get { return m_FirstName; } set { m_FirstName = value; } }
        public string LastName { get { return m_LastName; } set { m_LastName = value; } }
        public string Username { get { return m_Username; } set { m_Username = value; } }
        public string[] FavoriteCharities { get { return m_FavoriteCharities; } set { m_FavoriteCharities = value; } }

        private JsonValue m_json;
        private string m_SessionID       = "guest";
        private string m_Timestamp       = "default";
        private int    m_BlocksRemaining = 0;
        private int[]  m_CompletedBlocks = new int[0];
        private int m_Difficulty         = 0;
        private int    m_SubjectID       = 0;
        private string m_SubjectName     = "Math";
        private double m_TotalDonated    = 0.0;
        private int    m_TotalQuestions  = 0;
        private string m_CharityName     = "Red Cross";
        private string m_Email           = "guest@default.com";
        private string m_FirstName       = "Anon";
        private string m_LastName        = "Guest";
        private string m_Username        = "Slenderman";
        private string[] m_FavoriteCharities = new string[0];


        public string GetObjectString()
        {
            string data = "";

            data += m_SessionID + ",";
            data += m_Timestamp + ",";
            data += m_BlocksRemaining.ToString() + ",";
            //data += m_CompletedBlocks + ",";
            data += m_Difficulty + ",";
            data += m_SubjectID.ToString() + ",";
            data += m_SubjectName + ",";
            data += m_TotalDonated.ToString() + ",";
            data += m_TotalQuestions.ToString() + ",";
            data += m_CharityName + ",";
            data += m_Email + ",";
            data += m_FirstName + ",";
            data += m_LastName + ",";
            data += m_Username;

            return data;
        }
        public string UserObjectForm()
        {
            string data = "";
            data += "\"userobject\": {";
            data += "\"user_data\": {";
            data += "\"username\": \"" + m_Username + "\",";
            data += "\"email\": \"" + m_Email + "\",";
            data += "\"first_name\": \""+ m_FirstName +"\",";
            data += "\"last_name\": \""+ m_LastName +"\",";
            data += "\"selected_charity\": \""+ m_CharityName +"\", ";
            data += "\"favorite_charities\": [\"";
            for (int x = 0; x < m_FavoriteCharities.Length; x++)
            {
                data += m_FavoriteCharities[x];
                if (x != m_CompletedBlocks.Length - 1)
                    data += "\" \"";
            }
            data += "\"] ";
            data += "},";
            data += "\"game_data\": {";
            data += "\"subject_name\": \""+ m_SubjectName +"\",";
            data += "\"subject_id\": "+ Convert.ToString(m_SubjectID) +",";
            data += "\"difficulty\": "+ Convert.ToString(m_Difficulty) +",";
            data += "\"totalQuestions\": "+ Convert.ToString(m_TotalQuestions) + ",";
            data += "\"totalDonated\": " + Convert.ToString(m_TotalDonated) +",";
            data += "\"blocksRemaining\": "+ Convert.ToString(m_BlocksRemaining) +",";
            data += "\"completed_blocks\": [";
            for(int x =0;x < m_CompletedBlocks.Length;x++)
            {
                data += Convert.ToString(m_CompletedBlocks[x]);
                if (x != m_CompletedBlocks.Length - 1)
                    data += " ";
            }
            data += "] }, ";
            data += "\"timestamp\":\""+ m_Timestamp +"\"";
            data += "}";
            return data;
        }

        //Requires an activity to pass to LocalArchive as UserObject is an asset and not an activity
        // so LocalArchive would be unable to link the protected file to the app.
        public void Save(Activity activity)
        {
            LocalArchive archive = new LocalArchive(activity);
            string data = GetObjectString();

            archive.SaveUserData(data);
        }

        //Requires an activity to pass to LocalArchive as UserObject is an asset and not an activity
        // so LocalArchive would be unable to link the protected file to the app.
        public void Load(Activity activity)
        {
            LocalArchive archive = new LocalArchive(activity);
            string[] data = archive.LoadUserData().Split(',');
            m_SessionID = data[0];
            m_Timestamp = data[1];
            m_BlocksRemaining = Convert.ToInt32(data[2]);
            //m_CompletedBlocks = data[3];
            m_Difficulty = Convert.ToInt32(data[3]);
            m_SubjectID = Convert.ToInt32(data[4]);
            m_SubjectName = data[5];
            m_TotalDonated = Convert.ToInt32(data[6]);
            m_TotalQuestions = Convert.ToInt32(data[7]);
            m_CharityName = data[8];
            m_Email = data[9];
            m_FirstName = data[10];
            m_LastName = data[11];
            m_Username = data[12];
        }

        public int AddCompletedBlock(int value)
        {
            CompletedBlocks = new int[m_CompletedBlocks.Length+1];
            for(int x =0; x < m_CompletedBlocks.Length; x++)
            {
                CompletedBlocks[x] = m_CompletedBlocks[x];
            }
            CompletedBlocks[m_CompletedBlocks.Length] = value;
            m_CompletedBlocks = CompletedBlocks;
            return m_CompletedBlocks.Length;
        }
    }
}