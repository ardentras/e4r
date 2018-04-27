using Android.Content;
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
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            newBox.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newBox.LayoutParameters = param;
            _button = newBox;
        }
    }
}