using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using SCD_UVSS.ImageProcessor;

namespace UnitTest
{
    public class TestImageSticher
    {
        public void DeleteFile(string absFilePath)
        {
            try
            {
                if(File.Exists(absFilePath))
                    File.Delete(absFilePath);
            }
            catch (Exception){
            }
        }

        [Test]
        public void TestSticher()
        {
            //
            var executiongDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tempFileName = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetTempFileName()), ".jpg");

            var fileList = new List<string> {"split_1.jpg", "split_2.jpg"};
            var absFileList = from fileName in fileList
                              select Path.Combine(executiongDir, "Resources", fileName);
            
            // Action
            ImageSticher.Sticher(absFileList, tempFileName);
            
            // Assert
            Assert.True(File.Exists(tempFileName));
        }
    }
}
