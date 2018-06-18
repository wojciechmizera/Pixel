using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel
{
    public static class DearVisualStudio
    {
        public static string GiveMeLetterForThisColumn(int index)
        {
            string letter = "";
            int number = index;
            do
            {
                letter += (char)(65 + number % 26);
                number /= 26;
            }
            while (number-- > 0);

            char[] t = letter.ToCharArray();
            Array.Reverse(t);
            return new string(t);
        }


        public static bool IsThePictureSmall(Bitmap bitmap)
        {
            return bitmap.Height > Program.form.CenterPicture.Height || bitmap.Width > Program.form.Width;
        }


        public static Bitmap ResizeThePicture(Bitmap bitmap, int size)
        {
            double imageSize = bitmap.Size.Width * bitmap.Size.Height;
            double scale = Math.Sqrt((double)size / imageSize * 4);
            return new Bitmap(bitmap, (int)(bitmap.Width * scale / 4), (int)(bitmap.Height * scale));
        }


    }
}
