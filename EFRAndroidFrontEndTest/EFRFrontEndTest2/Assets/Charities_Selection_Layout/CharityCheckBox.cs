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

namespace EFRFrontEndTest2.Assets.Charities_Selection_Layout
{
    public class CharityCheckBox
    {
        private CheckBox _button = null;
        public CharityCheckBox(Context ctx)
        {
            CreateButton(ctx);
        }
        public CheckBox Button
        {
            get { return _button; }
            set { _button = value; }
        }
        private void CreateButton(Context ctx)
        {
            CheckBox newBox = new CheckBox(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(50, 50)
            {
                LeftMargin = 20
            };
            newBox.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newBox.LayoutParameters = param;
            _button = newBox;
        }
    }
}