using Android.Content;
using Android.Widget;

namespace EFRFrontEndTest2.Assets.Charities_Selection_Layout
{
    class CharityFavorite
    {
        bool _favorited = false;
        ImageButton _button = null;
        string charity_name;
        public CharityFavorite(Context ctx, string name)
        {
            charity_name = name;
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
            var uo = SingleUserObject.getObject();
            if (_favorited)
            {
                uo.RemoveFavorite(charity_name);
                updateButton(Resource.Drawable.unfavorited);
            }
            else
            {
                uo.AddFavorite(charity_name);
                updateButton(Resource.Drawable.favorited);
            }
        }
        private void CreateButton(Context ctx)
        {
            var uo = SingleUserObject.getObject();
            ImageButton newButton = new ImageButton(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(60, 60)
            {
                RightMargin = 20
            };
            newButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newButton.LayoutParameters = param;
            if(uo.HasFavorite(charity_name))
                newButton.SetBackgroundResource(Resource.Drawable.favorited);
            else
                newButton.SetBackgroundResource(Resource.Drawable.unfavorited);
            newButton.SetScaleType(ImageView.ScaleType.FitCenter);
            _button = newButton;
        }
    }
}