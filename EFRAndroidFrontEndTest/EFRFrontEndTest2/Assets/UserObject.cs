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
        public UserObject() { }

        //public Array CompletedBlocks { get { return m_CompletedBlocks; } set { m_CompletedBlocks = value; } } //TODO: Make array once server functionality is implemented
        public string Difficulty { get { return m_Difficulty; } set { m_Difficulty = value; } }
        public int SubjectID { get { return m_SubjectID; } set { m_SubjectID = value; } }
        public string Timestamp { get { return m_Timestamp; } set { m_Timestamp = value; } }
        public string Charity { get { return m_Charity; } set { m_Charity = value; } }
        public string FirstName { get { return m_FirstName; } set { m_FirstName = value; } }
        public double MoneyEarned { get { return m_MoneyEarned; } set { m_MoneyEarned = value; } }
        public int QuestionsAnswered { get { return m_QuestionsAnswered; } set { m_QuestionsAnswered = value; } }
        public string LastName { get { return m_LastName; } set { m_LastName = value; } }
        public string Username { get { return m_Username; } set { m_Username = value; } }       //These should only be changed when loading a user object (I used this implementation for readability and consistancy)
        public string SessionID { get { return m_SessionID; } set { m_SessionID = value; } }    //These should only be changed when loading a user object
        public int[] CompletedBlocks { get { return m_CompletedBlocks; } set { m_CompletedBlocks = value; } }

        private string m_SessionID;
        //private Array m_CompletedBlocks;
        private string m_Difficulty;
        private int m_SubjectID;
        private string m_Timestamp;
        private string m_Charity;
        private string m_FirstName;
        private string m_LastName;
        private string m_Username;
        private double m_MoneyEarned;
        private int m_QuestionsAnswered;
        private int[] m_CompletedBlocks;

        public string GetObjectString()
        {
            string objectString = m_SessionID + ",";
            //objectString += m_CompletedBlocks + ",";
            objectString += m_Difficulty + ",";
            objectString += m_SubjectID.ToString() + ",";
            objectString += m_Timestamp + ",";
            objectString += m_Charity + ",";
            objectString += m_FirstName + ",";
            objectString += m_LastName + ",";
            objectString += m_Username + ",";
            objectString += m_MoneyEarned + ",";
            objectString += m_QuestionsAnswered + ",";
//TODO: If someone has time, replace with a more effecient process

            return objectString;
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

        public bool SetObjectString(string objectString)
        {
            bool done = false;
            string[] list = objectString.Split(',');
            if (list.Length == 9)
            {
                try { m_SubjectID = Int32.Parse(list[3]); } //Ensures a corrupt string will not corrupt the object
                catch (Exception e) { return false; }
                m_SessionID = list[0];
               // m_CompletedBlocks = list[1];
                m_Difficulty = list[1];
                m_Timestamp = list[3];
                m_Charity = list[4];
                m_FirstName = list[5];
                m_LastName = list[6];
                m_Username = list[7];
                m_MoneyEarned = Convert.ToInt32(list[8]);
                m_QuestionsAnswered = Convert.ToInt32(list[9]);

                done = true;
            }

            return done; //True if object is updated successfully
        }
    }
}