using System;

namespace EFRFrontEndTest2.Assets
{
    class SingleUserObject
    {
        // To use this class you can either instantiate the singleton or
        // simply type UserObject var = SingleUserObject.getObject();
        // and you should get the user object.
        public SingleUserObject()
        {
            if(singleObj == null)
            {
                singleObj = new UserObject();
                singleObj.Level = (int)(Math.Sqrt(singleObj.TotalQuestions / 10) + singleObj.TotalDonated / 10 + 1);
            }
        }

        public static ref UserObject getObject()
        {
            if(singleObj == null)
            {
                singleObj = new UserObject();
                singleObj.Level = (int)(Math.Sqrt(singleObj.TotalQuestions / 10) + singleObj.TotalDonated / 10 + 1);
            }
            return ref singleObj;
        }

        private static UserObject singleObj;
    }
}