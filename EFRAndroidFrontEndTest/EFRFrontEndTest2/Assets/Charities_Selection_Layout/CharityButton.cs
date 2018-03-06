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
    class CharityButton
    {
        ImageButton _button = null;
        public CharityButton(Context ctx, string charity_name)
        {
            CreateCharityInfoButton(ctx, IdentifyCharity(charity_name));
        }
        public ImageButton Button
        {
            get { return _button; }
            set { _button = value; }
        }
        private int IdentifyCharity(string charity_name)
        {
            int logo = 0;
            switch (charity_name)
            {
                case "Direct Relief":
                    logo = Resource.Drawable.charity_direct_relief;
                    break;
                case "American Red Cross":
                    logo = Resource.Drawable.charity_red_cross;
                    break;
                case "United Way":
                    logo = Resource.Drawable.charity_united_way;
                    break;
                default:
                    logo = Resource.Drawable.charity_direct_relief;
                    break;
            }
            return logo;
        }

        private void CreateCharityInfoButton(Context ctx, int logo)
        {
            ImageButton newButton = new ImageButton(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent, 1f)
            {
                LeftMargin = 30,
                RightMargin = 30
            };
            newButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newButton.LayoutParameters = param;
            newButton.SetBackgroundResource(logo);
            newButton.SetScaleType(ImageView.ScaleType.FitCenter);
            _button = newButton;
        }
    }
}