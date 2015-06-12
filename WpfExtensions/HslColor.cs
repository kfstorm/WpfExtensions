using System;
using System.Windows.Media;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// Represents a color with HSL color space
    /// </summary>
    public struct HslColor
    {
        /// <summary>
        /// The alpha channel
        /// </summary>
        public double A;

        /// <summary>
        /// The hue
        /// </summary>
        public double H;

        /// <summary>
        /// The saturation
        /// </summary>
        public double S;

        /// <summary>
        /// The lightness
        /// </summary>
        public double L;

        /// <summary>
        /// Initializes a new instance of the <see cref="HslColor"/> type.
        /// </summary>
        /// <param name="a">The alpha channel.</param>
        /// <param name="h">The hue.</param>
        /// <param name="s">The saturation.</param>
        /// <param name="l">The lightness.</param>
        public HslColor(double a, double h, double s, double l)
        {
            A = a;
            H = h;
            S = s;
            L = l;
        }

        /// <summary>
        /// Generate a new instance of the <see cref="HslColor"/> type from an instance of the <see cref="Color"/> type.
        /// </summary>
        /// <param name="argb">The instance of the <see cref="Color"/> type.</param>
        /// <returns>The new instance of the <see cref="HslColor"/> type.</returns>
        public static HslColor FromArgb(Color argb)
        {
            double r = argb.R;
            double g = argb.G;
            double b = argb.B;

            double h = 0;
            double s;

            var min = Math.Min(Math.Min(r, g), b);
            var l = Math.Max(Math.Max(r, g), b);
            var delta = l - min;

            if (AlmostEqual(l, 0))
            {
                s = 0;
            }
            else
                s = delta / l;

            if (AlmostEqual(s, 0))
                h = 0.0;

            else
            {
                if (AlmostEqual(r, l))
                    h = (g - b) / delta;
                else if (AlmostEqual(g, l))
                    h = 2 + (b - r) / delta;
                else if (AlmostEqual(b, l))
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;

            }

            return new HslColor { A = (double)argb.A / 255, H = h, S = s, L = l / 255 };
        }

        /// <summary>
        /// Converts to a new instance of the <see cref="Color"/> type.
        /// </summary>
        /// <returns>The new instance of the <see cref="Color"/> type.</returns>
        public Color ToArgb()
        {
            var h = H;
            var s = S;
            var l = L;

            double r, g, b;

            if (AlmostEqual(s, 0))
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                if (AlmostEqual(h, 360))
                    h = 0;
                else
                    h = h / 60;

                var i = (int)Math.Truncate(h);
                var f = h - i;

                var p = l * (1.0 - s);
                var q = l * (1.0 - (s * f));
                var t = l * (1.0 - (s * (1.0 - f)));

                switch (i)
                {
                    case 0:
                    {
                        r = l;
                        g = t;
                        b = p;
                        break;
                    }
                    case 1:
                    {
                        r = q;
                        g = l;
                        b = p;
                        break;
                    }
                    case 2:
                    {
                        r = p;
                        g = l;
                        b = t;
                        break;
                    }
                    case 3:
                    {
                        r = p;
                        g = q;
                        b = l;
                        break;
                    }
                    case 4:
                    {
                        r = t;
                        g = p;
                        b = l;
                        break;
                    }
                    default:
                    {
                        r = l;
                        g = p;
                        b = q;
                        break;
                    }
                }

            }

            return Color.FromArgb((byte)(A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        private static bool AlmostEqual(double left, double right)
        {
            return Math.Abs(left - right) < 1e-7;
        }
    }
}
