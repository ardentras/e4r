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
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Assets
{
    class CharitySelection
    {
        private int selected;
        private Context ctx;
        private List<CheckBox> checkboxes;
        public CharitySelection(Context _ctx)
        {
            ctx = _ctx;
            checkboxes = new List<CheckBox>();
        }
        public int Selected {
            get { return selected; }
            set { selected = value; }
        }
    public void FillCharities(LinearLayout root, Charity charity, int index)
        {
            root.AddView(CreateLayout(charity, index));
        }
        private void HandleCheckClicks(object sender, EventArgs e)
        {
            foreach (CheckBox item in checkboxes)
            {
                selected = ((CheckBox)sender).Id;
                if (item != (CheckBox)sender)
                {
                    item.Checked = false;
                }
                else if (item.Checked == false)
                {
                    item.Checked = true;
                }
            }
        }
        private void HandleCharityDescription(object sender, EventArgs e, Charity charity)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(ctx);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(charity.Name);
            alert.SetMessage(charity.Description);
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.SetButton2("Learn More", (c, ev) =>
            {
                var uri = Android.Net.Uri.Parse(charity.Url);
                var intent = new Intent(Intent.ActionView, uri);
                ctx.StartActivity(intent);
            });
            alert.Show();
        }
        private void HandleFavoriteClicks(object sender, EventArgs e)
        {
            //unfinished, only checks, doesn't uncheck
            ImageButton temp = (ImageButton)sender;
            temp.SetBackgroundResource(Resource.Drawable.favorited);
        }
        private ImageButton CreateFavoriteBtn()
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
            newButton.Click += HandleFavoriteClicks;
            return newButton;
        }
        private ImageButton CreateCharityImage(int img, Charity charity)
        {
            ImageButton newButton = new ImageButton(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(100, LinearLayout.LayoutParams.MatchParent, 1f)
            {
                LeftMargin = 80,
                RightMargin = 80
            };
            newButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newButton.LayoutParameters = param;
            newButton.SetBackgroundResource(img);
            newButton.SetScaleType(ImageView.ScaleType.FitCenter);
            newButton.Click += (s, e) =>
            {
                HandleCharityDescription(s, e, charity);
            };
            return newButton;
        }
        private CheckBox CreateCheckBox(int index)
        {
            CheckBox newBox = new CheckBox(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(50, 50)
            {
                LeftMargin = 20
            };
            newBox.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newBox.LayoutParameters = param;
            newBox.Click += HandleCheckClicks;
            newBox.Id = index;
            return newBox;
        }
        private LinearLayout CreateLayout(Charity charity, int index)
        {
            LinearLayout newlayout = new LinearLayout(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 150)
            {
                BottomMargin = 10,
                TopMargin = 0
            };
            int logo = 0;
            CheckBox checkBox = CreateCheckBox(index);
            checkboxes.Add(checkBox);

            switch(charity.Name)
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
                    return null;
            }
            newlayout.SetPadding(0, 20, 0, 20);
            newlayout.LayoutParameters = param;
            newlayout.SetGravity(GravityFlags.Center);
            newlayout.Orientation = Orientation.Horizontal;
            newlayout.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            newlayout.AddView(checkBox);
            newlayout.AddView(CreateCharityImage(logo, charity));
            newlayout.AddView(CreateFavoriteBtn());
            return newlayout;
        }
    }
}