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
    class CharityFavorite
    {
        bool _favorited = false;
        ImageButton _button = null;

        public CharityFavorite(Context ctx) {
            CreateButton(ctx);
        }
        public bool Favorited {
            get { return _favorited; }
            set { _favorited = value; }
        }
        public ImageButton Button{
            get { return _button; }
            set { _button = value; }
        }
        private void updateButton(int img)
        {
            _button.SetBackgroundResource(img);
            _favorited = !_favorited;
        }
        public void HandleFavoriteClicks()
        {
            if (_favorited)
            {
                updateButton(Resource.Drawable.unfavorited);
            }
            else
            {
                updateButton(Resource.Drawable.favorited);
            }
        }
        private void CreateButton(Context ctx)
        {
            ImageButton newButton = new ImageButton(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(60, 60)
            {
                RightMargin = 20
            };
            newButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newButton.LayoutParameters = param;
            newButton.SetBackgroundResource(Resource.Drawable.unfavorited);
            newButton.SetScaleType(ImageView.ScaleType.FitCenter);
            _button = newButton;
        }
    }
}