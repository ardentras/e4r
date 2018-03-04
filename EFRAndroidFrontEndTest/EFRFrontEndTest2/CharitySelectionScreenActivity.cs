using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2
{
    [Activity(Label = "CharitySelectionScreenActivity")]
    public class CharitySelectionScreenActivity : Activity
    {
        private Charity current = new Charity();
        private List<Charity> charities = new List<Charity>();
        private CharitySelection creater;

        //test data
        private Charity None = new Charity("None",
        "None",
        "None");
        private Charity directrelief = new Charity("Direct Relief",
                "a nonprofit, nonpartisan organization with a stated mission to “improve the health and lives of people affected by poverty or emergency situations by mobilizing and providing essential medical resources needed for their care.",
                "https://www.directrelief.org/");
        private Charity redcross = new Charity("American Red Cross",
                "a humanitarian organization that provides emergency assistance, disaster relief and education in the United States.",
                "http://www.redcross.org/");
        private Charity unitedway = new Charity("United Way",
                "is the leadership and support organization for the network of nearly 1,800 community-based United Ways in 45 countries and territories.",
                "https://www.unitedway.org/");

        public CharitySelectionScreenActivity()
        {
            creater = new CharitySelection(this);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            charities.Add(None);
            charities.Add(directrelief);
            charities.Add(redcross);
            charities.Add(unitedway);

            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CharitySelectionScreen);

            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.back_arrow);
            ImageButton filterButton = FindViewById<ImageButton>(Resource.Id.filter_btn);
            TextView save_btn_ext = FindViewById<TextView>(Resource.Id.save_btn_ext);
            TextView info_btn = FindViewById<TextView>(Resource.Id.info_btn);
            LinearLayout charities_list = FindViewById<LinearLayout>(Resource.Id.charity_list);
            LinearLayout saveBtn = FindViewById<LinearLayout>(Resource.Id.save_btn);
            for(int i = 1; i < charities.Count; ++i)
            {
                creater.FillCharities(charities_list, charities[i], i);
            }
            filterButton.Click += HandleFilterBtn;
            info_btn.Click += HandleInfoBtn;
            save_btn_ext.Click += HandleSave;
            saveBtn.Click += HandleSave;
            backButton.Click += HandleBackBtn;
        }
        private void HandleFilterBtn(object sender, EventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Upcoming");
            alert.SetMessage("Coming soon...");
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }
        private void HandleSave(object sender, EventArgs e)
        {
            TextView selected_charity = FindViewById<TextView>(Resource.Id.selected_charitiy_name);
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            if (current != null)
            {
                if (current.Name != charities[creater.Selected].Name)
                {
                    selected_charity.Text = charities[creater.Selected].Name;
                    current = charities[creater.Selected];
                    alert.SetTitle("Saved");
                    alert.SetMessage("Your selected charity has been updated.");
                }
                else
                {
                    alert.SetTitle("Unchanged");
                    alert.SetMessage("You didn't select a new charity.");
                }
            }
            else
            {
                alert.SetTitle("Unchanged");
                alert.SetMessage("You didn't select a new charity.");
            }
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }
        private void HandleInfoBtn(object sender, EventArgs e)
        {
            if (current != null)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle(current.Name);
                alert.SetMessage(current.Description);
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.SetButton2("Learn More", (c, ev) =>
                {
                    var uri = Android.Net.Uri.Parse(current.Url);
                    var intent = new Intent(Intent.ActionView, uri);
                    this.StartActivity(intent);
                });
                alert.Show();
            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("No Charity has Been Selected");
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }
        }
        private void HandleBackBtn(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }
    }
}