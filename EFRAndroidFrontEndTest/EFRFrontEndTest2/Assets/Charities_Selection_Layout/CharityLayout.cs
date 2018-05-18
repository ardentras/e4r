using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Fragments;


/***************************************************************************************************************************
 * Author: Kevin Xu - if you change anything, update this!!!
 * Class: CharitySelection
 * Description: A class to handle all the logic for dynamic rendering of the charity selection page.
****************************************************************************************************************************/

namespace EFRFrontEndTest2.Assets.Charities_Selection_Layout
{
    public class CharityLayout
    {
        private Context _ctx = null;
        private LinearLayout _seperator = null;
        private CharityFavorite _favoriteBtn = null;
        private CharityButton _logoBtn = null;
        private DynamicSize.DynamicSize _Dsize = null;

        private CharitySelection _main = null;

        public CharityLayout(CharitySelection main, Context ctx, Charity charity, string seperator_color, Android.Content.Res.Resources Re)
        {
            Charity = charity;
            _ctx = ctx;
            _main = main;
            _Dsize = new DynamicSize.DynamicSize(Re);
            CreateContainer();
            CreateSeperator(seperator_color);
            CreateComponent();
            CreateChildComponents();
            MergeLayouts();
        }
        public LinearLayout Layout { get; set; } = null;

        public LinearLayout Container { get; set; } = null;

        public CharityCheckBox CheckBox { get; set; } = null;
        public Charity Charity { get; set; } = null;

        private void initClickhandlers()
        {
            _favoriteBtn.Button.Click += delegate
            {
                if (_favoriteBtn.Favorited)
                {
                    _main.Favorites.Remove(Charity);
                }
                else
                {
                    _main.Favorites.Add(Charity);
                }
                _favoriteBtn.HandleFavoriteClicks();
            };
            CheckBox.Button.Click += delegate
            {
                if (_main.Selected != this)
                {
                    if (_main.Selected != null)
                    {
                        _main.Selected.CheckBox.Button.Checked = false;
                        _main.Selected.CheckBox.Button.Enabled = true;
                    }
                    _main.Selected = this;
                    _main.Selected.CheckBox.Button.Enabled = false;
                }
            };
            _logoBtn.Button.Click += delegate
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(_ctx);
                AlertDialog alert = dialog.Create();
                alert.SetTitle(Charity.Name);
                alert.SetMessage(Charity.Description);
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.SetButton2("Learn More", (c, ev) =>
                {
                    var uri = Android.Net.Uri.Parse(Charity.Url);
                    var intent = new Intent(Intent.ActionView, uri);
                    _ctx.StartActivity(intent);
                });
                alert.Show();
            };
        }
        private void MergeLayouts()
        {
            Layout.AddView(CheckBox.Button);
            Layout.AddView(_logoBtn.Button);
            Layout.AddView(_favoriteBtn.Button);
            Container.AddView(_seperator);
            Container.AddView(Layout);
        }
        private void CreateChildComponents()
        {
            _favoriteBtn = new CharityFavorite(_ctx, Charity.Name);
            CheckBox = new CharityCheckBox(_ctx);
            _logoBtn = new CharityButton(_ctx, Charity.Name);
            initClickhandlers();
        }
        private void CreateSeperator(string color)
        {
            LinearLayout newlayout = new LinearLayout(_ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(5, ViewGroup.LayoutParams.MatchParent)
            {
                LeftMargin = 8,
                RightMargin = 8

            };
            newlayout.LayoutParameters = param;
            newlayout.Orientation = Orientation.Vertical;
            newlayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            _seperator = newlayout;
        }
        private void CreateContainer()
        {
            LinearLayout newlayout = new LinearLayout(_ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent)
            {
                TopMargin = 0,
                BottomMargin = 10
            };
            newlayout.LayoutParameters = param;
            newlayout.Orientation = Orientation.Horizontal;
            Container = newlayout;
        }
        /***************************************************************************************************************************
         * 
         * Function: CreateLayout
         * Purpose: Create the container for the checkbox, image and favorite button, so it all belongs to that single charity.
         * 
        ****************************************************************************************************************************/
        private void CreateComponent()
        {
            LinearLayout newlayout = new LinearLayout(_ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, _Dsize.PxHeight(0.2), 1f);
            newlayout.SetPadding(0, 20, 0, 20);
            newlayout.LayoutParameters = param;
            newlayout.SetGravity(GravityFlags.Center);
            newlayout.Orientation = Orientation.Horizontal;
            newlayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            Layout = newlayout;
        }
    }
}