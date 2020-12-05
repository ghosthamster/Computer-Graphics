using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CGv2.Models
{
    public class Fractal
    {
        public int c { get; set; }
        public int num { get; set; }
        public bool color { get; set; }
        public Fractal(int c, int num, double xmin, double xmax, double ymin, double ymax,bool color)
        {
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            this.c = c;
            this.num = num;
            this.color = color;
            draw();
        }
        double xmin, xmax, ymin, ymax;
        private Complex NewtonFractalFuntion(Complex CC)
        {
            Complex result = new Complex();
            result = Complex.Pow(CC, num) - c;
            return result;
        }


        private Complex DerivateNewtonFractalFuntion(Complex CC)
        {
            Complex Delta = new Complex(Math.Pow(10, -10), Math.Pow(10, -10));
            Complex result = new Complex();
            result = (NewtonFractalFuntion(CC + Delta) - NewtonFractalFuntion(CC)) / (Delta);
            return result;
        }
        public ImageBrush draw()
        {

            int a, b, kmax = new int();
            a = 400;
            b = 400;
            kmax = 50;

            int[,] image = new int[a, b];
            double x0, y0, dx, dy = new double();

            dx = (xmax - xmin) / (a - 1);
            dy = (ymax - ymin) / (b - 1);

            Complex Z, Z0 = new Complex();
            int k = 0;

            for (int nx = 0; nx < a; nx++)
            {
                for (int ny = 0; ny < b; ny++)
                {

                    k = 0;
                    //Find the current location
                    x0 = xmin + nx * dx;
                    y0 = ymin + ny * dy;

                    Z = new Complex(x0, y0);
                    bool test = new bool();
                    test = true;
                    while (k <= kmax && test)
                    {
                        Z0 = Z - (NewtonFractalFuntion(Z) / DerivateNewtonFractalFuntion(Z));
                        k += 1;
                        double xx = new double();
                        xx = (double)Complex.Abs(Z0 - Z);
                        if (xx < 0.00001)
                        {
                            k = (int)(Math.Truncate(Z0.Phase) + num);
                            test = false;
                        }
                        else
                        {
                            Z = Z0;
                        }

                    }
                    image[ny, nx] = k;
                }
            }
            ImageBrush img = new ImageBrush();
            img.ImageSource = CreateBitmap(image, a, b, color);
            return (img);
        }
        List<Color> UniqueColorList = new List<Color>();
        List<Color> SmoothColorList = new List<Color>();
        private void FillInColorList()
        {
            SmoothColorGenerator Temp = new SmoothColorGenerator();
            SmoothColorList = Temp.ColorList;

            ColourGenerator generator = new ColourGenerator();
            UniqueColorList.Add(Color.FromRgb(0, 0, 0));
            UniqueColorList.Add(Color.FromRgb(255, 255, 255));
            for (int i = 0; i <= 895; i++)
            {
                Color c = new Color();
                c = (Color)ColorConverter.ConvertFromString("#" + generator.NextColour());
                UniqueColorList.Add(c);
            }
        }
        private BitmapSource CreateBitmap(int[,] IntegerArray, int Width, int Height, bool NotDistinctColor)
        {
            List<Color> LstColor = new List<Color>();
            FillInColorList();
            if (NotDistinctColor)
            {
                LstColor = SmoothColorList;
            }
            else
            {
                LstColor = UniqueColorList;
            }
            List<byte> buffer = new List<byte>();

            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    int k = IntegerArray[i, j];

                    buffer.Add(LstColor[k].R);

                    buffer.Add(LstColor[k].G);
                    buffer.Add(LstColor[k].B);
                    buffer.Add(255);

                }
            }

            double dpiX = 96;
            double dpiY = 96;
            PixelFormat pixelFormat = PixelFormats.Pbgra32;
            int bytesPerPixel = (int)Math.Truncate((double)((pixelFormat.BitsPerPixel + 7) / 8));
            int stride = (int)(bytesPerPixel * Width);

            return BitmapSource.Create(Width, Height, dpiX, dpiY, pixelFormat, null, buffer.ToArray(), stride);
            //
        }
        public class SmoothColorGenerator
        {


            public SmoothColorGenerator()
            {
                GradientStop.Add(Brushes.Red);
                GradientStop.Add(Brushes.Yellow);
                GradientStop.Add(Brushes.Green);
                GradientStop.Add(Brushes.Magenta);
                GradientStop.Add(Brushes.Blue);
                GradientStop.Add(Brushes.Purple);
                GradientStop.Add(Brushes.Gold);
                for (int i = 0; i <= GradientStop.Count - 2; i++)
                {
                    GetGradients(GradientStop[i].Color, GradientStop[i + 1].Color, 35);
                }

            }

            private List<SolidColorBrush> pGradientStop = new List<SolidColorBrush>();
            public List<SolidColorBrush> GradientStop
            {
                get { return pGradientStop; }
                set { pGradientStop = value; }
            }

            private List<Color> pColorlist = new List<Color>();
            public List<Color> ColorList
            {
                get { return pColorlist; }
                set { pColorlist = value; }
            }

            public void GetGradients(Color starts, Color ends, int steps)
            {
                int stepA = (int)Math.Truncate((Convert.ToDouble(ends.A) - Convert.ToDouble(starts.A)) / (Convert.ToDouble(steps) - 1));
                int stepR = (int)Math.Truncate((Convert.ToDouble(ends.R) - Convert.ToDouble(starts.R)) / (Convert.ToDouble(steps) - 1));
                int stepG = (int)Math.Truncate((Convert.ToDouble(ends.G) - Convert.ToDouble(starts.G)) / (Convert.ToDouble(steps) - 1));
                int stepB = (int)Math.Truncate((Convert.ToDouble(ends.B) - Convert.ToDouble(starts.B)) / (Convert.ToDouble(steps) - 1));


                for (int i = 0; i <= steps - 1; i++)
                {
                    ColorList.Add(Color.FromArgb(Convert.ToByte(Convert.ToInt32(starts.A) + Convert.ToInt32(stepA) * Convert.ToInt32(i)), Convert.ToByte(Convert.ToInt32(starts.R) + Convert.ToInt32(stepR) * Convert.ToInt32(i)), Convert.ToByte(Convert.ToInt32(starts.G) + Convert.ToInt32(stepG) * Convert.ToInt32(i)), Convert.ToByte(Convert.ToInt32(starts.B) + Convert.ToInt32(stepB) * Convert.ToInt32(i))));
                }

            }
        }

        public class ColourGenerator
        {

            private int index = 0;

            private IntensityGenerator intensityGenerator = new IntensityGenerator();
            public string NextColour()
            {
                string colour = string.Format(PatternGenerator.NextPattern(index), intensityGenerator.NextIntensity(index));
                index += 1;
                return colour;
            }
        }

        public class PatternGenerator
        {
            public static string NextPattern(int index)
            {
                switch (index % 7)
                {
                    case 0:
                        return "{0}0000";
                    case 1:
                        return "00{0}00";
                    case 2:
                        return "0000{0}";
                    case 3:
                        return "{0}{0}00";
                    case 4:
                        return "{0}00{0}";
                    case 5:
                        return "00{0}{0}";
                    case 6:
                        return "{0}{0}{0}";
                    default:
                        throw new Exception("Math error");
                }
            }
        }

        public class IntensityGenerator
        {
            private IntensityValueWalker walker;

            private int current;
            public string NextIntensity(int index)
            {
                if (index == 0)
                {
                    current = 255;
                }
                else if (index % 7 == 0)
                {
                    if (walker == null)
                    {
                        walker = new IntensityValueWalker();
                    }
                    else
                    {
                        walker.MoveNext();
                    }
                    current = walker.Current.Value;
                }
                string currentText = current.ToString("X");
                if (currentText.Length == 1)
                {
                    currentText = "0" + currentText;
                }
                return currentText;
            }
        }

        public class IntensityValue
        {

            private IntensityValue mChildA;

            private IntensityValue mChildB;
            public IntensityValue(IntensityValue parent__1, int value__2, int level__3)
            {
                if (level__3 > 7)
                {
                    throw new Exception("There are no more colours left");
                }
                Value = value__2;
                Parent = parent__1;
                Level = level__3;
            }

            public int Level
            {
                get { return m_Level; }
                set { m_Level = value; }
            }
            private int m_Level;
            public int Value
            {
                get { return m_Value; }
                set { m_Value = value; }
            }
            private int m_Value;
            public IntensityValue Parent
            {
                get { return m_Parent; }
                set { m_Parent = value; }
            }

            private IntensityValue m_Parent;
            public IntensityValue ChildA
            {
                get { return mChildA ?? (InlineAssignHelper(ref mChildA, new IntensityValue(this, this.Value - (1 << (7 - Level)), Level + 1))); }
            }

            public IntensityValue ChildB
            {
                get { return mChildB ?? (InlineAssignHelper(ref mChildB, new IntensityValue(this, Value + (1 << (7 - Level)), Level + 1))); }
            }
            private static T InlineAssignHelper<T>(ref T target, T value)
            {
                target = value;
                return value;
            }
        }

        public class IntensityValueWalker
        {

            public IntensityValueWalker()
            {
                Current = new IntensityValue(null, 1 << 7, 1);
            }

            public IntensityValue Current
            {
                get { return m_Current; }
                set { m_Current = value; }
            }

            private IntensityValue m_Current;
            public void MoveNext()
            {
                if (Current.Parent == null)
                {
                    Current = Current.ChildA;
                }
                else if (object.ReferenceEquals(Current.Parent.ChildA, Current))
                {
                    Current = Current.Parent.ChildB;
                }
                else
                {
                    int levelsUp = 1;
                    Current = Current.Parent;
                    while (Current.Parent != null && object.ReferenceEquals(Current, Current.Parent.ChildB))
                    {
                        Current = Current.Parent;
                        levelsUp += 1;
                    }
                    if (Current.Parent != null)
                    {
                        Current = Current.Parent.ChildB;
                    }
                    else
                    {
                        levelsUp += 1;
                    }
                    for (int i = 0; i <= levelsUp - 1; i++)
                    {
                        Current = Current.ChildA;

                    }
                }
            }
        }
    }
}