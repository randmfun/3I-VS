using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;


namespace SCD_UVSS.SystemInput
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static byte[] ToByteArray(this Stream sourceStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /*
         * //Bitmap bmp = new Bitmap(stream);
                    //var bytes = bmp.ToByteArray(ImageFormat.Bmp);

                    Bitmap bmp2;
                    using (var ms = new MemoryStream(bytes))
                    {
                        bmp2 = new Bitmap(ms);
                        bmp2.Save("D:\\new_me.bmp");
                        bmp2.Dispose();
                    }
                    //bmp.Dispose();
         */

    }

}
