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
    class Charity
    {
        private string _name;
        private string _description;
        private string _url;
        public Charity()
        {
            _name = "None";
            _description = "None";
            _url = "None";
        }
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