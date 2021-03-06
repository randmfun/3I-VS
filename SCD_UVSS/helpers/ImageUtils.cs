﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using log4net;

namespace SCD_UVSS.helpers
{
    public static class ImageUtils
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public static BitmapImage ByteArrayToBitMapImage(byte[] array)
        {
            var bitMapImage = new BitmapImage();
            try
            {
                Logger.Info("Inside ByteArrayToBitMapImage");
                bitMapImage.BeginInit();
                bitMapImage.StreamSource = new System.IO.MemoryStream(array);
                bitMapImage.EndInit();
            }
            catch (Exception exception)
            {
                Logger.Error("Exception ByteArrayToBitMapImage", exception);
                Logger.Fatal(exception.Message, exception.InnerException);

                return null;
            }
            return bitMapImage;
        }

        public static BitmapImage FiletoBitmapImage(string filePath)
        {
            var bitmapImage = new BitmapImage();
            
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filePath);
            bitmapImage.EndInit();
            
            return bitmapImage;
        }

        /// <summary>
        /// var image = new Bitmap(10, 10);
        //  byte[] arr = image.ImageToByteArray(ImageFormat.Bmp);
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static System.Windows.Controls.Image BitMapImageToImage(BitmapImage bitmapImage)
        {
            return new System.Windows.Controls.Image {Source = bitmapImage};
        }


    }
}
