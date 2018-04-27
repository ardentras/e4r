using Android.Content;
using Android.Widget;

namespace EFRFrontEndTest2.Assets.Charities_Selection_Layout
{
    class CharityButton
    {
        ImageButton _button = null;
        public CharityButton(Context ctx, string charity_name)
        {
            CreateCharityInfoButton(ctx, IdentifyCharity(charity_name));
        }
        public ImageButton Button
        {
            get { return _button; }
            set { _button = value; }
        }
        private int IdentifyCharity(string charity_name)
        {
            int logo = 0;
            switch (charity_name)
            {
                case "Alzheimer's Association":
                    logo = Resource.Drawable.charity_alzheimers_association;
                    break;
                case "American Cancer Society":
                    logo = Resource.Drawable.charity_american_cancer_society;
                    break;
                case "American Heart Association":
                    logo = Resource.Drawable.charity_american_heart_association;
                    break;
                case "American Red Cross":
                    logo = Resource.Drawable.charity_red_cross;
                    break;
                case "ASPCA":
                    logo = Resource.Drawable.charity_ASPCA;
                    break;
                case "Boy's and Girl's Club's of America":
                    logo = Resource.Drawable.charity_boys_and_girls_club;
                    break;
                case "Compassion":
                    logo = Resource.Drawable.charity_compassion;
                    break;
                case "Direct Relief":
                    logo = Resource.Drawable.charity_direct_relief;
                    break;
                case "Feeding America":
                    logo = Resource.Drawable.charity_feeding_america;
                    break;
                case "Make-A-Wish":
                    logo = Resource.Drawable.charity_make_a_wish;
                    break;
                case "St. Jude Childrens Research Hospital":
                    logo = Resource.Drawable.charity_st_jude_childrens_research;
                    break;
                case "Susan G Komen":
                    logo = Resource.Drawable.charity_susan_g_komen;
                    break;
                case "Task Force Global Health":
                    logo = Resource.Drawable.charity_task_force_for_global_health;
                    break;
                case "The Humane Society":
                    logo = Resource.Drawable.charity_the_humane_society;
                    break;
                case "Toys-For-Tots":
                    logo = Resource.Drawable.charity_toys_for_tots;
                    break;
                case "United Way":
                    logo = Resource.Drawable.charity_united_way;
                    break;
                case "World Wildlife Fund":
                    logo = Resource.Drawable.charity_world_wildlife_foundation;
                    break;
                case "Wounded Warrior Project":
                    logo = Resource.Drawable.charity_wounded_warrior_project;
                    break;
                default:
                    logo = Resource.Drawable.charity_direct_relief;
                    break;
            }
            return logo;
        }

        private void CreateCharityInfoButton(Context ctx, int logo)
        {
            ImageButton newButton = new ImageButton(ctx);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent, 1f)
            {
                LeftMargin = 30,
                RightMargin = 30
            };
            newButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            newButton.LayoutParameters = param;
            newButton.SetBackgroundResource(logo);
            newButton.SetScaleType(ImageView.ScaleType.FitCenter);
            _button = newButton;
        }
    }
}