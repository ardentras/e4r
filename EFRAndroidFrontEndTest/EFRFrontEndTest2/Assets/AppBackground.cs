using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EFRFrontEndTest2.Assets
{
    class AppBackground
    {
        private static Drawable m_background;
        public static Drawable background { get { return m_background; } set { m_background = value; } }
    }
}