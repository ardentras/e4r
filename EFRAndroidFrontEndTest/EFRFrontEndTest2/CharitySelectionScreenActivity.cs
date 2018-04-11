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
using EFRFrontEndTest2.Assets.Charities_Selection_Layout;
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
        private CharityLayout _current = null;
        private CharityLayout _selected = null;
        //current list of available charities
        private List<Charity> _favorites = new List<Charity>();
        private List<Charity> _charities = new List<Charity>();
        private string[] colorCodes = new string[] { "#FF7D61", "#5EFDFF", "#5787FF", "#FFD04A" };
        private int colorCodes_index = 0;

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
        private Charity alzheimersassociation = new Charity("Alzheimer's Association",
                "is an association that works on a global, national and local level to provide care and support for all those affected by Alzheimer’s and other dementias.",
                "https://www.alz.org/");
        private Charity americancancersociety = new Charity("American Cancer Society",
                "is a nationwide, community-based voluntary health organization dedicated to eliminating cancer as a major health problem.",
                "https://www.cancer.org/");
        private Charity americanheartassociation = new Charity("American Heart Association",
                "is the nation’s oldest and largest voluntary organization dedicated to fighting heart disease and stroke. Founded by six cardiologists in 1924, our organization now includes more than 22.5 million volunteers and supporters.",
                "https://www.compassion.com/");
        private Charity redcross = new Charity("American Red Cross",
                "is a humanitarian organization that provides emergency assistance, disaster relief and education in the United States.",
                "http://www.redcross.org/");
        private Charity aspca = new Charity("ASPCA",
                "is founded on the belief that animals are entitled to kind and respectful treatment at the hands of humans and must be protected under the law.",
                "https://www.aspca.org/");
        private Charity boysandgirlsclub = new Charity("Boy's and Girl's Club's of America",
                "is providing a world-class Club Experience that assures success is within reach of every young person who enters our doors.",
                "https://www.bgca.org/");
        private Charity compassion = new Charity("Compassion",
                "is a child sponsorship ministry that connects one child with one sponsor to help the child achieve his or her God given potential.",
                "https://www.compassion.com/");
        private Charity directrelief = new Charity("Direct Relief",
                "a nonprofit, nonpartisan organization with a stated mission to “improve the health and lives of people affected by poverty or emergency situations by mobilizing and providing essential medical resources needed for their care.",
                "https://www.directrelief.org/");
        private Charity feedingamerica = new Charity("Feeding America",
                "is the nation’s largest domestic hunger-relief organization. Together with individuals, charities, businesses and government we can end hunger.",
                "http://www.feedingamerica.org/");
        private Charity makeawish = new Charity("Make-A-Wish",
                "is a collection of tens of thousands of volunteers, donors and supporters who advance the Make-A-Wish® vision to grant the wish of every child diagnosed with a critical illness.",
                "http://wish.org");
        private Charity stjudechildrensresearch = new Charity("St. Jude Childrens Research Hospital",
                "is leading the way the world understands, treats and defeats childhood cancer and other life-threatening diseases.",
                "https://www.stjude.org/");
        private Charity susangkomen = new Charity("Susan G Komen",
                "is a sisters promise to end breast cancer forever.",
                "https://ww5.komen.org/");
        private Charity taskforceglobalhealth = new Charity("Task Force Global Health",
                "is an independent, nongovernmental organization based in Decatur, GA, USA. They focus on controlling and eliminating debilitating diseases and building durable systems that protect and promote health.",
                "https://www.taskforce.org/");
        private Charity humanesociety = new Charity("The Humane Society",
                "is the nation's most effective animal protection organization. With hands-on care and services to more than 100,000 animals each year.",
                "http://www.humanesociety.org");
        private Charity toysfortots = new Charity("Toys-For-Tots",
                "is a mission to collect new, unwrapped toys during October, November and December each year, and distribute those toys as Christmas gifts to less fortunate children in the community in which the campaign is conducted.",
                "https://www.toysfortots.org/");
        private Charity unitedway = new Charity("United Way",
                "is the leadership and support organization for the network of nearly 1,800 community-based United Ways in 45 countries and territories.",
                "https://www.unitedway.org/");
        private Charity worldwildlifefunt = new Charity("World Wildlife Fund",
                "is a mission to conserve nature and reduce the most pressing threats to the diversity of life on Earth.",
                "https://www.worldwildlife.org/");
        private Charity woundedwarriorproject = new Charity("Wounded Warrior Project",
                "is an organization that serves veterans and service members who incurred a physical or mental injury, illness, or wound, co-incident to their military service on or after September 11, 2001 and their families.",
                "https://www.woundedwarriorproject.org/");
        //**************************************************************************************************************************
        /***************************************************************************************************************************
         * 
         * Function: CharitySelectionScreenActivity - Default Constructor
         * Purpose: To initialize the creater with the context of this.
         * 
        ****************************************************************************************************************************/
        public CharityLayout Current
        {
            get { return _current; }
            set { _current = value; }
        }
        public CharityLayout Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public List<Charity> Favorites
        {
            get { return _favorites; }
            set { _favorites = value; }
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
            //set background to dynamic choice
            setBackground();
            //get the scrollview for displaying the list of charities.
            LinearLayout charities_list = FindViewById<LinearLayout>(Resource.Id.charity_list);
            //add the charities to the list.  (right now it's using static data, to be dynamic just loop through an array and add it to the list, but make sure the first element is None)
            TextView selected_charity = FindViewById<TextView>(Resource.Id.selected_charitiy_name);
            selected_charity.Text = SingleUserObject.getObject().CharityName;
            _charities.Add(None);
            _charities.Add(alzheimersassociation);
            _charities.Add(americancancersociety);
            _charities.Add(americanheartassociation);
            _charities.Add(redcross);
            _charities.Add(aspca);
            _charities.Add(boysandgirlsclub);
            _charities.Add(compassion);
            _charities.Add(directrelief);
            _charities.Add(feedingamerica);
            _charities.Add(makeawish);
            _charities.Add(stjudechildrensresearch);
            _charities.Add(susangkomen);
            _charities.Add(taskforceglobalhealth);
            _charities.Add(humanesociety);
            _charities.Add(toysfortots);
            _charities.Add(unitedway);
            _charities.Add(worldwildlifefunt);
            _charities.Add(woundedwarriorproject);
            //Loop through the charity list from index 1, since the first element is None
            for (int i = 1; i < _charities.Count; ++i, ++colorCodes_index)
            {
                if (colorCodes_index > 4)
                {
                    colorCodes_index = 0;
                }
                //Create the layout for that charity and add it to the root, which here, is charities_list(scroll view)
                charities_list.AddView((new CharityLayout(this, this, _charities[i], colorCodes[colorCodes_index], Resources)).Container);
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
            if (_selected != null)
            {
                if (_current != _selected)
                {
                    //Add in Update userobject call here
                    //The charity name has already been set, just need to update it in the server.
                    _current = _selected;
                    selected_charity.Text = _current.Charity.Name;
                    SingleUserObject.getObject().CharityName = _current.Charity.Name;
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
            if (_current != null && _current.Charity.Name != "None")
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle(_current.Charity.Name);
                alert.SetMessage(_current.Charity.Description);
                alert.SetButton("OK", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.SetButton2("Learn More", (c, ev) =>
                {
                    var uri = Android.Net.Uri.Parse(_current.Charity.Url);
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
        protected void setBackground()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = FindViewById<LinearLayout>(Resource.Id.charity_selection);
                background.Background = AppBackground.background;
            }
        }
    }
}