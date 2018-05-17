/***************************************************************************************************************************
 * Author: Kevin Xu - if you change anything, update this!!!
 * Class: Charity
 * Description: A class to handle all the charity information logic.
****************************************************************************************************************************/

namespace EFRFrontEndTest2.Assets.Charities_Selection_Layout
{
    public class Charity
    {
        private string _name = "None";
        private string _description = "None";
        private string _url = "None";
        public Charity(string name, string description, string url)
        {
            _name = name;
            _description = description;
            _url = url;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}