using System;
using System.Drawing;

namespace CP
{
    public abstract class ColorEx
    {
        public abstract Color ToColor();


        public struct RGB
        {
            //#================================================================ VARIABLES
            public readonly int Red; // [0-255]
            public readonly int Green; // [0-255]
            public readonly int Blue; // [0-255]

            //#================================================================ INITIALIZE
            public RGB(int r, int g, int b)
            {
                Red = Math.Min(Math.Max(r, 0), 255);
                Green = Math.Min(Math.Max(g, 0), 255);
                Blue = Math.Min(Math.Max(b, 0), 255);
            }
            public RGB(int ole)
            {
                Red = BitConverter.GetBytes(ole)[0];
                Green = BitConverter.GetBytes(ole)[1];
                Blue = BitConverter.GetBytes(ole)[2];
            }

            //#================================================================ FUNCTIONS
            public Color ToColor()
            {
                return Color.FromArgb(Red, Green, Blue);
            }
            public HSB ToHSB()
            {
                float m = Math.Min(Math.Min(Red, Green), Blue) / 255f;
                float chroma = Math.Max(Math.Max(Red, Green), Blue) / 255f - m;
                // finding saturation and brightness
                float b = m + chroma;
                float s = (b != 0 ? chroma / b : 0);
                return new HSB(ToColor().GetHue(), s * 100, b * 100);
            }
            public HSL ToHSL()
            {
                Color color = ToColor();
                return new HSL(color.GetHue(), color.GetSaturation() * 100, color.GetBrightness() * 100);
            }
            public int ToOLE()
            {
                return (Blue << 16 | Green << 8 | Red);
            }
        }


        public struct HSB
        {
            //#================================================================ VARIABLES
            public readonly float Hue; // [0-360]
            public readonly float Saturation; // [0-100]
            public readonly float Brightness; // [0-100]

            //#================================================================ INITIALIZE
            public HSB(float h, float s, float b)
            {
                Hue = Math.Min(Math.Max(h, 0), 360);
                Saturation = Math.Min(Math.Max(s, 0), 100);
                Brightness = Math.Min(Math.Max(b, 0), 100);
            }

            //#================================================================ FUNCTIONS
            public Color ToColor()
            {
                return ToRGB().ToColor();
            }
            public RGB ToRGB()
            {
                float h = Hue / 60f;
                float s = Saturation / 100f;
                float b = Brightness / 100f;
                float chroma = b * s;
                float x = chroma * (1 - Math.Abs(h % 2 - 1));
                float m = b - chroma;
                int d = (int)((chroma + m) * 255);
                int e = (int)((x + m) * 255);
                int f = (int)(m * 255);
                switch ((int)h)
                {
                    case 0: return new RGB(d, e, f);
                    case 1: return new RGB(e, d, f);
                    case 2: return new RGB(f, d, e);
                    case 3: return new RGB(f, e, d);
                    case 4: return new RGB(e, f, d);
                    case 5: return new RGB(d, f, e);
                    default: return new RGB();
                }
            }
        }


        public struct HSL
        {
            //#================================================================ VARIABLES
            public readonly float Hue; // [0-360]
            public readonly float Saturation; // [0-100]
            public readonly float Lightness; // [0-100]

            //#================================================================ INITIALIZE
            public HSL(float h, float s, float l)
            {
                Hue = Math.Min(Math.Max(h, 0), 360);
                Saturation = Math.Min(Math.Max(s, 0), 100);
                Lightness = Math.Min(Math.Max(l, 0), 100);
            }

            //#================================================================ FUNCTIONS
            public Color ToColor()
            {
                return ToRGB().ToColor();
            }
            public RGB ToRGB()
            {
                float h = Hue / 60f;
                float s = Saturation / 100f;
                float l = Lightness / 100f;
                float chroma = (1 - Math.Abs(2 * l - 1)) * s;
                float x = chroma * (1 - Math.Abs(h % 2 - 1));
                float m = l - chroma / 2f;
                int a = (int)((chroma + m) * 255);
                int b = (int)((x + m) * 255);
                int c = (int)(m * 255);
                switch ((int)h)
                {
                    case 0: return new RGB(a, b, c);
                    case 1: return new RGB(b, a, c);
                    case 2: return new RGB(c, a, b);
                    case 3: return new RGB(c, b, a);
                    case 4: return new RGB(b, c, a);
                    case 5: return new RGB(a, c, b);
                    default: return new RGB();
                }
            }
        }
    }
}
