namespace EFRFrontEndTest2.Assets.DynamicSize
{
    public class DynamicSize
    {
        private Android.Util.DisplayMetrics _metrics = null;
        private Android.Content.Res.Resources _resources = null;

        public DynamicSize(Android.Content.Res.Resources resources)
        {
            _resources = resources;
            _metrics = resources.DisplayMetrics;
        }
        public int PxWidth(double ratio)
        {
            return (int)(_metrics.WidthPixels*ratio);
        }
        public int PxHeight(double ratio)
        {
            return (int)(_metrics.HeightPixels*ratio);
        }
        public int DpHeight(double ratio) 
        {
            return (int)(ConvertPixelsToDp(_metrics.HeightPixels)*ratio);
        }
        public int DpWidth(double ratio)
        {
            return (int)(ConvertPixelsToDp(_metrics.WidthPixels)*ratio);
        }
        private int ConvertPixelsToDp(float pixel) {
            int dp = (int)((pixel) / _metrics.Density);
            return dp;
        }
    }
}
