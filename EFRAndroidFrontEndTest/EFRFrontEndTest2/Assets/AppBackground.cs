using Android.Graphics.Drawables;

namespace EFRFrontEndTest2.Assets
{
    class AppBackground
    {
        private static Drawable m_background;
        public static Drawable background { get { return m_background; } set { m_background = value; } }
    }
}