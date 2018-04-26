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

namespace 
    EFRFrontEndTest2.Assets
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
        public int Level { get { return m_Level; } set { m_Level = value; } }

        private JsonValue m_json;
        private string m_SessionID       = "guest";
        private string m_Timestamp       = "default";
        private int    m_BlocksRemaining = 0;
        private int[]  m_CompletedBlocks = new int[0];
        private int m_Difficulty         = 0;
        private int    m_SubjectID       = 0;
        private string m_SubjectName     = "Mathematics";
        private double m_TotalDonated    = 0.0;
        private int    m_TotalQuestions  = 0;
        private string m_CharityName     = "Red Cross";
        private string m_Email           = "guest@default.com";
        private string m_FirstName       = "Anon";
        private string m_LastName        = "Guest";
        private string m_Username        = "Slenderman";
        private string[] m_FavoriteCharities = new string[0];
        private int m_Level = 0;
        
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
            if (m_FavoriteCharities.Length != 1 && m_FavoriteCharities[0] != "")
            {
                for (int x = 0; x < m_FavoriteCharities.Length; x++)
                {
                    data += m_FavoriteCharities[x];
                    if (x < m_CompletedBlocks.Length - 1)
                        data += "\", \"";
                }
            }
            data += "\"] ";
            data += "},";
            data += "\"game_data\": {";
            data += "\"subject_name\": \""+ m_SubjectName +"\",";
            data += "\"subject_id\": "+ m_SubjectID.ToString() +",";
            data += "\"difficulty\": "+ m_Difficulty.ToString() + ",";
            data += "\"totalQuestions\": "+ m_TotalQuestions.ToString() + ",";
            data += "\"totalDonated\": " + m_TotalDonated.ToString() + ",";
            data += "\"blocksRemaining\": "+ m_BlocksRemaining.ToString() + ",";
            data += "\"completed_blocks\": [";
            for(int x =0;x < m_CompletedBlocks.Length;x++)
            {
                data += m_CompletedBlocks[x].ToString();
                if (x != m_CompletedBlocks.Length - 1)
                    data += ", ";
            }
            data += "] }, ";
            data += "\"timestamp\":\""+ m_Timestamp +"\"";
            data += "}";
            return data;
        }

        public int AddCompletedBlock(int value)
        {
            int length = CompletedBlocks.Length;
            int[] newCompletedBlocks;
            if (length != 0)
            {
                newCompletedBlocks = new int[length + 1];
                for (int x = 0; x < length; x++)
                {
                    newCompletedBlocks[x] = m_CompletedBlocks[x];
                }
            }
            else
                newCompletedBlocks = new int[1];

            newCompletedBlocks[length] = value;
            m_CompletedBlocks = newCompletedBlocks;
            return length + 1;
        }
    }
}