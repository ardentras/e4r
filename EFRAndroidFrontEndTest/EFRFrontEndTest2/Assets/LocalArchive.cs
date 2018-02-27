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

using Java.IO;

namespace EFRFrontEndTest2.Assets
{
    public class LocalArchive
    {
        public LocalArchive(Activity activity, string filename = "UserData.txt")
        {
            m_activity = activity;
            m_filename = filename;
        }

        //Should only be called by UserObject
        public void SaveUserData(string data)
        {
            using (var fos = m_activity.OpenFileOutput(m_filename, FileCreationMode.Private))
            {
                //Get the byte array
                byte[] bytes = Encoding.ASCII.GetBytes(data);
                fos.Write(bytes, 0, bytes.Length);
            }
        }

        //Should only be called by UserObject
        public string LoadUserData()
        {
            StringBuilder builder = new StringBuilder();
            using (var input = m_activity.OpenFileInput(m_filename))
            {
                int buffer = input.ReadByte();

                while (buffer != -1)
                {
                    builder.Append((char)buffer);
                    buffer = input.ReadByte();
                }
            }
            return builder.ToString();
        }

        //When a session renewal has failed, the user needs to log back in
        public void ClearUserData()
        {
            using (var fos = m_activity.OpenFileOutput(m_filename, FileCreationMode.Private))
                fos.Write(null, 0, 0);
        }

        private Activity m_activity;
        private string m_filename;
    }
}