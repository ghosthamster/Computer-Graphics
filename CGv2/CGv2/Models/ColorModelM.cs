using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace CGv2.Models
{
    public class ColorModelM
    {
        public Bitmap bmp { get; set; }
        public void convertImg()
        {
            string path = @"D:\projekt\Computer-Graphics\CGv2\CGv2\Resourses\images\rgb.jpg";
            bmp = (Bitmap)Bitmap.FromFile(path);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {

                        var pix = bmp.GetPixel(x, y);
                        int newr = pix.R;
                        int newg = pix.G;
                        int newb = pix.G;
                        RGBtoHLStoRGB(ref newr,ref newg,ref newb);
                        var newpix = Color.FromArgb(newr,newg,newb);
                        bmp.SetPixel(x, y, newpix);
                }
            }
            bmp.Save(@"D:\projekt\Computer-Graphics\CGv2\CGv2\Resourses\images\HSLColor.jpg");
        }
        public void changebrightnes(int brightnes,string color)
        {
            //colors: red, green, blue
            bmp = (Bitmap)Bitmap.FromFile(@"D:\projekt\Computer-Graphics\CGv2\CGv2\Resourses\images\rgb.jpg");
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bool pixel = false;
                    if(color == "red")
                    {
                        pixel = isRed(bmp.GetPixel(x, y));
                    }
                    else if (color == "green")
                    {
                        pixel = isGreen(bmp.GetPixel(x, y));
                    }
                           
                    else if (color == "blue")
                    {
                        pixel = isBlue(bmp.GetPixel(x, y));
                    }
                    if (pixel)
                    {
                        var pix = bmp.GetPixel(x, y);
                        int newr = pix.R;
                        int newg = pix.G;
                        int newb = pix.G;
                        RGBtoHLStoRGBWithBrightnes(ref newr, ref newg, ref newb,brightnes);
                        newr = newr < 0 ? 0 : newr;
                        newg = newg < 0 ? 0 : newg;
                        newb = newb < 0 ? 0 : newb;
                        newr = newr > 255 ? 255 : newr;
                        newg = newg > 255 ? 255 : newg;
                        newb = newb > 255 ? 255 : newb;
                        var newpix = Color.FromArgb(newr, newg, newb);
                        bmp.SetPixel(x, y, newpix);
                    }
                }
            }
            bmp.Save(@"D:\projekt\Computer-Graphics\CGv2\CGv2\Resourses\images\HSLColor.jpg");
        }
        public bool isRed(Color c)
        {
            if(c.GetHue() < 20 || c.GetHue() > 340)
            {
                if (c.GetSaturation() > 0.15)
                    return true;
            }
            return false;
        }
        public bool isGreen(Color c)
        {
            if (c.GetHue() < 140 || c.GetHue() > 100)
            {
                if (c.GetSaturation() > 0.15)
                    return true;
            }
            return false;
        }
        public bool isBlue(Color c)
        {
            if (c.GetHue() < 260 || c.GetHue() > 220)
            {
                if (c.GetSaturation() > 0.15)
                    return true;
            }
            return false;
        }
        public static void RGBtoHLStoRGB(ref int r,ref int g,ref int b)
        {
            double newh, newl, news;
            RgbToHls(r, g, b,out newh, out newl, out news);
            int newr, newg, newb;
            HlsToRgb(newh, newl, news,out newr,out newg,out newb);
            r = newr;
            g = newg;
            b = newb;
        }
        public static void RGBtoHLStoRGBWithBrightnes(ref int r, ref int g, ref int b,int brightnes)
        {
            double newh, newl, news;
            RgbToHls(r, g, b, out newh, out newl, out news);
            int newr, newg, newb;
            newl = 2*((double)brightnes/100)*newl;
            HlsToRgb(newh, newl, news, out newr, out newg, out newb);
            r = newr;
            g = newg;
            b = newb;
        }
        // Convert an RGB value into an HLS value.
        public static void RgbToHls(int r, int g, int b,
            out double h, out double l, out double s)
        {
            // Convert RGB to a 0.0 to 1.0 range.
            double double_r = r / 255.0;
            double double_g = g / 255.0;
            double double_b = b / 255.0;

            // Get the maximum and minimum RGB components.
            double max = double_r;
            if (max < double_g) max = double_g;
            if (max < double_b) max = double_b;

            double min = double_r;
            if (min > double_g) min = double_g;
            if (min > double_b) min = double_b;

            double diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                double r_dist = (max - double_r) / diff;
                double g_dist = (max - double_g) / diff;
                double b_dist = (max - double_b) / diff;

                if (double_r == max) h = b_dist - g_dist;
                else if (double_g == max) h = 2 + r_dist - b_dist;
                else h = 4 + g_dist - r_dist;

                h = h * 60;
                if (h < 0) h += 360;
            }
        }

        // Convert an HLS value into an RGB value.
        public static void HlsToRgb(double h, double l, double s,
            out int r, out int g, out int b)
        {
            double p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            double p1 = 2 * l - p2;
            double double_r, double_g, double_b;
            if (s == 0)
            {
                double_r = l;
                double_g = l;
                double_b = l;
            }
            else
            {
                double_r = QqhToRgb(p1, p2, h + 120);
                double_g = QqhToRgb(p1, p2, h);
                double_b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            r = (int)(double_r * 255.0);
            g = (int)(double_g * 255.0);
            b = (int)(double_b * 255.0);
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
    }
}