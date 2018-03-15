using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Net;
using EFRFrontEndTest2.Assets.Charities_Selection_Layout;
using EFRFrontEndTest2.Assets.DynamicSize;


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
        private LinearLayout _layout = null;
        private LinearLayout _seperator = null;
        private LinearLayout _container = null;
        private Charity _charity = null;

        private CharityCheckBox _checkBox = null;
        private CharityFavorite _favoriteBtn = null;
        private CharityButton _logoBtn = null;
        private DynamicSize.DynamicSize _Dsize = null;

        private CharitySelectionScreenActivity _main = null;

        public CharityLayout(CharitySelectionScreenActivity main, Context ctx, Charity charity, string seperator_color, Android.Content.Res.Resources Re)
        {
            _charity = charity;
            _ctx = ctx;
            _main = main;
            _Dsize = new DynamicSize.DynamicSize(Re);
            CreateContainer();
            CreateSeperator(seperator_color);
            CreateComponent();
            CreateChildComponents();
            MergeLayouts();
        }
        public LinearLayout Layout
        {
            get { return _layout; }
            set { _layout = value; }
        }
        public LinearLayout Container
        {
            get { return _container; }
            set { _container = value; }
        }
        public CharityCheckBox CheckBox
        {
            get { return _checkBox; }
            set { _checkBox = value; }
        }
        public Charity Charity
        {
            get { return _charity; }
            set { _charity = value; }
        }
        private void initClickhandlers()
        {
            _favoriteBtn.Button.Click += delegate
            {
                if (_favoriteBtn.Favorited)
                {
                    _main.Favorites.Remove(_charity);
                }
                else
                {
                    _main.Favorites.Add(_charity);
                }
                _favoriteBtn.HandleFavoriteClicks();
            };
            _checkBox.Button.Click += delegate
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
                alert.SetTitle(_charity.Name);
                alert.SetMessage(_charity.Description);
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.SetButton2("Learn More", (c, ev) =>
                {
                    var uri = Android.Net.Uri.Parse(_charity.Url);
                    var intent = new Intent(Intent.ActionView, uri);
                    _ctx.StartActivity(intent);
                });
                alert.Show();
            };
        }
        private void MergeLayouts()
        {
            _layout.AddView(_checkBox.Button);
            _layout.AddView(_logoBtn.Button);
            _layout.AddView(_favoriteBtn.Button);
            _container.AddView(_seperator);
            _container.AddView(_layout);
        }
        private void CreateChildComponents()
        {
            _favoriteBtn = new CharityFavorite(_ctx);
            _checkBox = new CharityCheckBox(_ctx);
            _logoBtn = new CharityButton(_ctx, _charity.Name);
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
            _container = newlayout;
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
            _layout = newlayout;
        }
    }
}