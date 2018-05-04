using System;

namespace EFRFrontEndTest2.Assets
{
    class SingleUserObject
    {
        //to use this class you can either instantiate the singleton or
        // simply type UserObject var = SingleUserObject.getObject();
        // and you should get the user object.
        public SingleUserObject()
        {
            if(singleObj == null)
            {
                singleObj = new UserObject();
            }

        }

        public static ref UserObject getObject()
        {
            if(singleObj == null)
            {
                singleObj = new UserObject();
            }
            return ref singleObj;
        }
        private static UserObject singleObj;
    }
}