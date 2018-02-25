using System;
using System.Collections.Generic;
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
        public UserObject() { m_CompletedBlocks[0] = 0; }

        public string SessionID { get { return m_SessionID; } set { m_SessionID = value; } }
        public string Timestamp { get { return m_Timestamp; } set { m_Timestamp = value; } }
        public int BlocksRemaining { get { return m_BlocksRemaining; } set { m_BlocksRemaining = value; } }
        public int[] CompletedBlocks { get { return m_CompletedBlocks; } set { m_CompletedBlocks = value; } }
        public string Difficulty { get { return m_Difficulty; } set { m_Difficulty = value; } }
        public int SubjectID { get { return m_SubjectID; } set { m_SubjectID = value; } }
        public string SubjectName { get { return m_SubjectName; } set { m_SubjectName = value; } }
        public double TotalDonated { get { return m_TotalDonated; } set { m_TotalDonated = value; } }
        public int TotalQuestions { get { return m_TotalQuestions; } set { m_TotalQuestions = value; } }
        public string CharityName { get { return m_CharityName; } set { m_CharityName = value; } }
        public string Email { get { return m_Email; } set { m_Email = value; } }
        public string FirstName { get { return m_FirstName; } set { m_FirstName = value; } }
        public string LastName { get { return m_LastName; } set { m_LastName = value; } }
        public string Username { get { return m_Username; } set { m_Username = value; } }

        private string m_SessionID = "guest";
        private string m_Timestamp = "default";
        private int m_BlocksRemaining = 0;
        private int[] m_CompletedBlocks = new int[1];
        private string m_Difficulty = "easy";
        private int m_SubjectID = 0;
        private string m_SubjectName = "Math";
        private double m_TotalDonated = 0.0;
        private int m_TotalQuestions = 0;
        private string m_CharityName = "Red Cross";
        private string m_Email = "guest@default.com";
        private string m_FirstName = "Anon";
        private string m_LastName = "Guest";
        private string m_Username = "Slenderman";
        
        public string GetObjectString()
        {
            string objectString = m_SessionID + ",";
            objectString += m_CompletedBlocks + ",";
            objectString += m_Difficulty + ",";
            objectString += m_SubjectID.ToString() + ",";
            objectString += m_Timestamp + ",";
           // objectString += m_Charity + ",";
            objectString += m_FirstName + ",";
            objectString += m_LastName + ",";
            objectString += m_Username + ",";
          //  objectString += m_MoneyEarned + ",";
          //  objectString += m_QuestionsAnswered + ",";

            return objectString;
        }
        public bool SetObjectString(string objectString)
        {
            bool done = false;
            string[] list = objectString.Split(',');
            if (list.Length == 9)
            {
                try { m_SubjectID = Int32.Parse(list[3]); } //Ensures a corrupt string will not corrupt the object
                catch (Exception) { return false; }
                m_SessionID = list[0];
                //m_CompletedBlocks = list[1];
                m_Difficulty = list[1];
                m_Timestamp = list[3];
           //     m_Charity = list[4];
                m_FirstName = list[5];
                m_LastName = list[6];
                m_Username = list[7];
             //   m_MoneyEarned = Convert.ToInt32(list[8]);
             //   m_QuestionsAnswered = Convert.ToInt32(list[9]);

                done = true;
            }

            return done; //True if object is updated successfully
        }

        public void Save()
        {

        }

        public void Load()
        {

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