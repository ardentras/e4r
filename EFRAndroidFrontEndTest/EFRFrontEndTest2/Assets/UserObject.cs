using System;
using System.Json;

namespace 
    EFRFrontEndTest2.Assets
{
    public class UserObject
    {
        public JsonValue Json { get; set; }
        public string SessionID { get; set; } = "guest";

        public string Timestamp { get; set; } = "default";

        public int BlocksRemaining { get; set; } = 0;

        public int[] CompletedBlocks { get; set; } = new int[0];

        public int Difficulty { get; set; } = 0;

        public int SubjectID { get; set; } = 0;

        public string SubjectName { get; set; } = "Math";

        public double TotalDonated { get { return m_TotalDonated; } set { m_TotalDonated = value; UpdateLevel(); } }
        public int TotalQuestions { get { return m_TotalQuestions; } set { m_TotalQuestions = value; UpdateLevel(); } }
        public string CharityName { get; set; } = "American Red Cross";

        public string Email { get; set; } = "guest@default.com";

        public string FirstName { get; set; } = "Anon";

        public string LastName { get; set; } = "Guest";

        public string Username { get; set; } = "Slenderman";

        public string[] FavoriteCharities { get; set; } = new string[0];

        public int Level { get; set; } = 1;

        private double m_TotalDonated    = 0.0;
        private int    m_TotalQuestions  = 0;

        public string UserObjectForm()
        {
            string data = "";
            data += "\"userobject\": {";
            data += "\"user_data\": {";
            data += "\"username\": \"" + Username + "\",";
            data += "\"email\": \"" + Email + "\",";
            data += "\"first_name\": \""+ FirstName +"\",";
            data += "\"last_name\": \""+ LastName +"\",";
            data += "\"selected_charity\": \""+ CharityName +"\", ";
            data += "\"favorite_charities\": [\"";
            try
            {
                if (FavoriteCharities.Length > 0 && FavoriteCharities[0] != "")
                {
                    for (int x = 0; x < FavoriteCharities.Length; x++)
                    {
                        data += FavoriteCharities[x];
                        if (x < CompletedBlocks.Length - 1)
                            data += "\", \"";
                    }
                }
            }
            catch (Exception e)
            {
                int i = 0;
                i++;
            }
            data += "\"] ";
            data += "},";
            data += "\"game_data\": {";
            data += "\"subject_name\": \""+ SubjectName +"\",";
            data += "\"subject_id\": "+ SubjectID.ToString() + ",";
            data += "\"difficulty\": "+ Difficulty.ToString() + ",";
            data += "\"totalQuestions\": "+ m_TotalQuestions.ToString() + ",";
            data += "\"totalDonated\": " + m_TotalDonated.ToString() + ",";
            data += "\"blocksRemaining\": "+ BlocksRemaining.ToString() + ",";
            data += "\"completed_blocks\": [";
            for(int x =0;x < CompletedBlocks.Length;x++)
            {
                data += CompletedBlocks[x].ToString();
                if (x != CompletedBlocks.Length - 1)
                    data += ", ";
            }
            data += "] }, ";
            data += "\"timestamp\":\""+ Timestamp +"\"";
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
                    newCompletedBlocks[x] = CompletedBlocks[x];
                }
            }
            else
                newCompletedBlocks = new int[1];

            newCompletedBlocks[length] = value;
            CompletedBlocks = newCompletedBlocks;
            return length + 1;
        }

        private void UpdateLevel()
        {
            Level = (int)(Math.Sqrt(TotalQuestions) + TotalDonated);
            if (Level <= 0)
                Level = 1;
        }
    }
}