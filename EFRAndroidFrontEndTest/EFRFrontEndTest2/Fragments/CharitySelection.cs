using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets.Charities_Selection_Layout;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class CharitySelection : Android.Support.V4.App.Fragment
    {
        private UserObject uo = SingleUserObject.getObject();
        private Context _ctx = null;
        View view = null;
        private List<Charity> _charities = new List<Charity>();
        private string[] colorCodes = { "#FF7D61", "#5EFDFF", "#5787FF", "#FFD04A" };
        private int colorCodes_index = 0;
        private Charity[] charityNames = {
            new Charity("Alzheimer's Association", 
                        "is an association that works on a global, national and local level to provide care and support for all those affected by Alzheimer’s and other dementias.",
                        "https://www.alz.org/"),
            new Charity("American Cancer Society",
                        "is a nationwide, community-based voluntary health organization dedicated to eliminating cancer as a major health problem.",
                        "https://www.cancer.org/"),
            new Charity("American Heart Association",
                        "is the nation’s oldest and largest voluntary organization dedicated to fighting heart disease and stroke. Founded by six cardiologists in 1924, our organization now includes more than 22.5 million volunteers and supporters.",
                        "https://www.compassion.com/"),
            new Charity("American Red Cross",
                        "is a humanitarian organization that provides emergency assistance, disaster relief and education in the United States.",
                        "http://www.redcross.org/"),
            new Charity("ASPCA",
                        "is founded on the belief that animals are entitled to kind and respectful treatment at the hands of humans and must be protected under the law.",
                        "https://www.aspca.org/"),
            new Charity("Boy's and Girl's Club's of America",
                        "is providing a world-class Club Experience that assures success is within reach of every young person who enters our doors.",
                        "https://www.bgca.org/"),
            new Charity("Compassion",
                        "is a child sponsorship ministry that connects one child with one sponsor to help the child achieve his or her God given potential.",
                        "https://www.compassion.com/"),
            new Charity("Direct Relief",
                        "a nonprofit, nonpartisan organization with a stated mission to “improve the health and lives of people affected by poverty or emergency situations by mobilizing and providing essential medical resources needed for their care.",
                        "http://www.feedingamerica.org/"),
            new Charity("Make-A-Wish",
                        "is a collection of tens of thousands of volunteers, donors and supporters who advance the Make-A-Wish® vision to grant the wish of every child diagnosed with a critical illness.",
                        "http://wish.org"),
            new Charity("St. Jude Childrens Research Hospital",
                        "is leading the way the world understands, treats and defeats childhood cancer and other life-threatening diseases.",
                        "https://www.stjude.org/"),
            new Charity("Susan G Komen",
                        "is a sisters promise to end breast cancer forever.",
                        "https://ww5.komen.org/"),
            new Charity("Task Force Global Health",
                        "is an independent, nongovernmental organization based in Decatur, GA, USA. They focus on controlling and eliminating debilitating diseases and building durable systems that protect and promote health.",
                        "https://www.taskforce.org/"),
            new Charity("The Humane Society",
                        "is the nation's most effective animal protection organization. With hands-on care and services to more than 100,000 animals each year.",
                        "http://www.humanesociety.org"),
            new Charity("Toys-For-Tots",
                        "is a mission to collect new, unwrapped toys during October, November and December each year, and distribute those toys as Christmas gifts to less fortunate children in the community in which the campaign is conducted.",
                        "https://www.toysfortots.org/"),
            new Charity("United Way",
                        "is the leadership and support organization for the network of nearly 1,800 community-based United Ways in 45 countries and territories.",
                        "https://www.unitedway.org/"),
            new Charity("World Wildlife Fund",
                        "is a mission to conserve nature and reduce the most pressing threats to the diversity of life on Earth.",
                        "https://www.worldwildlife.org/"),
            new Charity("Wounded Warrior Project",
                        "is an organization that serves veterans and service members who incurred a physical or mental injury, illness, or wound, co-incident to their military service on or after September 11, 2001 and their families.",
                        "https://www.woundedwarriorproject.org/")
        };
        public CharityLayout Current { get; set; } = null;

        public CharityLayout Selected { get; set; } = null;

        public List<Charity> Favorites { get; set; } = new List<Charity>();

        public CharitySelection(Context ctx)
        {
            _ctx = ctx;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            foreach (Charity c in charityNames)
            {
                _charities.Add(c);
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            setBackgrounds();
        }

        public static CharitySelection NewInstance(Context ctx)
        {
            CharitySelection temp = new CharitySelection(ctx);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            view = inflater.Inflate(Resource.Layout.CharitySlection, container, false);
            setBackgrounds();

            LinearLayout charities = view.FindViewById<LinearLayout>(Resource.Id.charities);
            TextView currentCharity = view.FindViewById<TextView>(Resource.Id.current);
            TextView currentInfo = view.FindViewById<TextView>(Resource.Id.info);
            Button savebutton = view.FindViewById<Button>(Resource.Id.savebutton);
            bool found = false;

            if (uo.CharityName != "")
            {
                currentCharity.Text = uo.CharityName;
            }
            currentInfo.Click += delegate
            {
                if (Current != null && Current.Charity.Name != "None")
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle(Current.Charity.Name);
                    alert.SetMessage(Current.Charity.Description);
                    alert.SetButton("OK", (c, ev) =>
                    {
                        alert.Dismiss();
                    });
                    alert.SetButton2("Learn More", (c, ev) =>
                    {
                        var uri = Android.Net.Uri.Parse(Current.Charity.Url);
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);
                    });
                    alert.Show();
                }
                else
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(Context);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("No Charity has Been Selected");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        alert.Dismiss();
                    });
                    alert.Show();
                }
            };

            savebutton.Click += async delegate
            {
                if (Selected != null && Current != Selected)
                {
                    Current = Selected;
                    currentCharity.Text = Selected.Charity.Name;
                    uo.CharityName = Selected.Charity.Name;
                    CallDatabase db = new CallDatabase();
                    await db.UpdateUO();
                }
            };

            for (int i = 0; i < _charities.Count; ++i, ++colorCodes_index)
            {
                if (colorCodes_index >= 4)
                {
                    colorCodes_index = 0;
                }
                CharityLayout temp = new CharityLayout(this, _ctx, _charities[i], colorCodes[colorCodes_index], Resources);
                if (!found && uo.CharityName != "")
                {
                    if (_charities[i].Name == uo.CharityName)
                    {
                        Current = temp;
                        found = true;
                    }
                }
                charities.AddView(temp.Container);
            }

            return view;
        }

        protected void setBackgrounds()
        {
            if (AppBackground.background != null)
            {
                LinearLayout background = view.FindViewById<LinearLayout>(Resource.Id.charity_screen);
                background.Background = AppBackground.background;
            }
        }
    }
}