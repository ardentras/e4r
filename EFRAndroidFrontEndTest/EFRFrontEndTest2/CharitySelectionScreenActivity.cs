﻿using Android.App;
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

/***************************************************************************************************************************
 * Author: Kevin Xu - if you change anything, update this!!!
 * Class: CharitySelectionScreenActivity
 * Description: A class to handle all the logic for the charity selection page.
 * Required Classes:
 *      - Charity.cs - for charity information storage
 *      - CharitySelection.cs - for dynamic layout creation
****************************************************************************************************************************/

namespace EFRFrontEndTest2
{
    [Activity(Label = "CharitySelectionScreenActivity")]
    public class CharitySelectionScreenActivity : Activity
    {
        //current selected charity
        private Charity current = new Charity();
        //current list of available charities
        private List<Charity> charities = new List<Charity>();
        //creater to dynamically create the layout for each charity within the scroll view
        private CharitySelection creater;

        /***************************************************************************************************************************
         * 
         * Note: The Follwing variables are Test datas to be pushed into charities list.
         * Required: 
         *   - Make sure the first element in the array of charities is None.
         * Upcoming Changes:
         *   - Replace the datas below by filling the list with an array of charities from the api calls.
         * Side-note:
         *   - Charity is an object, whose constructor takes a string for name, string for description and a string for the url. 
         *   - Charity(string name, string description, string url)
        ****************************************************************************************************************************/
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
        //**************************************************************************************************************************
        /***************************************************************************************************************************
         * 
         * Function: CharitySelectionScreenActivity - Default Constructor
         * Purpose: To initialize the creater with the context of this.
         * 
        ****************************************************************************************************************************/
        public CharitySelectionScreenActivity()
        {
            creater = new CharitySelection(this);
        }
        /***************************************************************************************************************************
         * 
         * Function: OnCreate
         * Purpose: Pretty much same for all activities, override oncreate, runs when activity is loaded
         * 
        ****************************************************************************************************************************/
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Remove the title bar for comestic purposes
            RequestWindowFeature(WindowFeatures.NoTitle);
            //call base oncreate
            base.OnCreate(savedInstanceState);
            //Set the layout content view to charityselectionscreen.axml
            SetContentView(Resource.Layout.CharitySelectionScreen);
            //Initialize the views with their click handlers
            initButtonHandlers();
            //get the scrollview for displaying the list of charities.
            LinearLayout charities_list = FindViewById<LinearLayout>(Resource.Id.charity_list);
            //add the charities to the list.  (right now it's using static data, to be dynamic just loop through an array and add it to the list, but make sure the first element is None)
            charities.Add(None);
            charities.Add(directrelief);
            charities.Add(redcross);
            charities.Add(unitedway);
            //Loop through the charity list from index 1, since the first element is None
            for (int i = 1; i < charities.Count; ++i)
            {
                //Create the layout for that charity and add it to the root, which here, is charities_list(scroll view)
                creater.FillCharities(charities_list, charities[i], i);
            }

        }
        /***************************************************************************************************************************
         * 
         * Function: initButtonHandlers
         * Purpose: Initialize the views(buttons and texts) to their rightful on click handlers.
         * 
        ****************************************************************************************************************************/
        private void initButtonHandlers()
        {
            ImageButton backButton = FindViewById<ImageButton>(Resource.Id.back_arrow);
            ImageButton filterButton = FindViewById<ImageButton>(Resource.Id.filter_btn);
            TextView save_btn_ext = FindViewById<TextView>(Resource.Id.save_btn_ext);
            TextView info_btn = FindViewById<TextView>(Resource.Id.info_btn);
            LinearLayout saveBtn = FindViewById<LinearLayout>(Resource.Id.save_btn);
            filterButton.Click += HandleFilterBtn;
            info_btn.Click += HandleInfoBtn;
            save_btn_ext.Click += HandleSave;
            saveBtn.Click += HandleSave;
            backButton.Click += HandleBackBtn;
        }
        /***************************************************************************************************************************
         * 
         * Function: HandleFilterBtn
         * Purpose: handles the click event for the filter button.
         * 
        ****************************************************************************************************************************/
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
        /***************************************************************************************************************************
         * 
         * Function: HandleSave
         * Purpose: handles the click event for the save button.
         * Process:
         *      - Check if the current is null, just make sure.
         *      - check if current's name is the same as the charity you just selected
         *      - if the same charity is selected then don't change anything
         *      - else change the current selected charity
         * 
        ****************************************************************************************************************************/
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
        /***************************************************************************************************************************
         * 
         * Function: HandleInfoBtn
         * Purpose: handles the click event for the info button.
         * Process:
         *      - Display the current info/description for the selected charity
         * 
        ****************************************************************************************************************************/
        private void HandleInfoBtn(object sender, EventArgs e)
        {
            if (current != null && current.Name != "None")
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
        /***************************************************************************************************************************
         * 
         * Function: HandleBackBtn
         * Purpose: handles the click event for the back button.
         * Process:
         *      - pop the view from the navagation stack, going back to the previous page, which is userdashboard.
         * 
        ****************************************************************************************************************************/
        private void HandleBackBtn(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }
    }
}