using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Emgu.CV;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using log4net;

namespace SCD_UVSS.ImageProcessor
{
    public class ImageSticher
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ImageSticher));

        public static bool Sticher(IEnumerable<string> fileList, string saveFileLocation)
        {
            var imageArray = from fileName in fileList
                             select new Image<Bgr, byte>(fileName);

            try
            {

                using (var stitcher = new Stitcher(false))
                {
                    using (var vm = new VectorOfMat())
                    {
                        var result = new Mat();
                        vm.Push(imageArray.ToArray());
                        stitcher.Stitch(vm, result);
                        result.Save(saveFileLocation);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to stich !!", ex);
                return false;
            }
            finally
            {
                foreach (Image<Bgr, Byte> img in imageArray)
                {
                    img.Dispose();
                }
            }
        }

        public static byte[] Sticher(IEnumerable<byte[]> images)
        {
            try
            {
                var fileNames = new List<string>();
                foreach (var image in images)
                {
                    var tempFileName = Path.GetTempFileName();
                    tempFileName = Path.ChangeExtension(tempFileName, "jpg");

                    File.WriteAllBytes(tempFileName, image);
                    fileNames.Add(tempFileName);
                }

                var resultFile = Path.GetTempFileName();
                resultFile = Path.ChangeExtension(resultFile, "jpg");

                Sticher(fileNames, resultFile);

                return File.ReadAllBytes(resultFile);
            }
            catch (Exception exception)
            {
                Logger.Error("Failed Sticher byte array", exception);
                return new byte[1];
            }
        }

        /*
        private void CropImage2()
        {
            Rectangle cropRect = new Rectangle(new Point(0, 0), new Size(100, 200));
            Bitmap src = Image.FromFile("fileName") as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                    cropRect,
                    GraphicsUnit.Pixel);
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public void MergeMe(object sender, EventArgs e)
        {
            string[] files = new string[2];
            files[0] = @"D:\personal\code\vs-jac\UnitTest\Resources\inward\ProperFrontPointerInwardDirection.png";
            files[1] = @"D:\personal\code\vs-jac\UnitTest\Resources\inward\ProperBackPointerInwardDirection.png";
            
            Bitmap myinage = CombineBitmap(files);
            myinage.Save(@"D:\personal\code\vs-jac\UnitTest\Resources\actualFinalImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        
        public static Bitmap CombineBitmap(string[] files)
        {
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(image);

                    //update the size of the final bitmap
                    width += bitmap.Width;
                    height = bitmap.Height > height ? bitmap.Height : height;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (System.Drawing.Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new System.Drawing.Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }
        */
    }
}
