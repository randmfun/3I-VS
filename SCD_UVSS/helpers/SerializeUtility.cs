using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using log4net;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.helpers
{
    public static class SerializeUtility
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        public static bool Save(object gate, string saveFileName)
        {
            try
            {
                //serialize
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    var bformatter = new BinaryFormatter();
                    bformatter.Serialize(stream, gate);
                }
                return true;
            }
            catch (Exception exception)
            {
                Logger.Error("Save binary information failed", exception);
                return false;
            }
        }

        public static T Read<T>(string saveFileName)
        {
            T savedObject = default(T);
            try
            {
                //deserialize
                using (Stream stream = File.Open(saveFileName, FileMode.Open))
                {
                    var bformatter = new BinaryFormatter();
                    savedObject = (T) bformatter.Deserialize(stream);
                }
            }
            catch (Exception exception)
            {
                Logger.Error("Read binary information failed", exception);
            }

            return savedObject;
        }
    }
}
