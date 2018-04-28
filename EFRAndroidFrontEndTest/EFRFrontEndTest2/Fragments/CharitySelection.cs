using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EFRFrontEndTest2.Assets.Charities_Selection_Layout;
using EFRFrontEndTest2.Assets;

namespace EFRFrontEndTest2.Fragments
{
    public class CharitySelection : Android.Support.V4.App.Fragment
    {
        private CharityLayout _current = null;
        private CharityLayout _selected = null;
        private Context _ctx = null;
        //current list of available charities
        private List<Charity> _favorites = new List<Charity>();
        private List<Charity> _charities = new List<Charity>();
        private string[] colorCodes = new string[] { "#FF7D61", "#5EFDFF", "#5787FF", "#FFD04A" };
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
        public static CharitySelection NewInstance(Context ctx)
        {
            CharitySelection temp = new CharitySelection(ctx);
            return temp;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.CharitySlection, container, false);
            LinearLayout charities = view.FindViewById<LinearLayout>(Resource.Id.charities);
            for (int i = 0; i < _charities.Count; ++i, ++colorCodes_index)
            {
                if (colorCodes_index >= 4)
                {
                    colorCodes_index = 0;
                }
                charities.AddView((new CharityLayout(this, _ctx, _charities[i], colorCodes[colorCodes_index], Resources)).Container);
            }
            return view;
        }
    }
}