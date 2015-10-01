﻿using System;
using System.Collections.Generic;
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
            try
            {
                var imageArray = from fileName in fileList
                                 select new Image<Bgr, byte>(fileName);

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
    }
}
