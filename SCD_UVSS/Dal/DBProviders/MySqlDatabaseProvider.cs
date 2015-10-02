using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using log4net;
using MySql.Data.MySqlClient;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.Dal.DBProviders
{
    public class MySqlDatabaseProvider : IDatabaseProvider
    {
        MySqlConnection connection;
        //MySqlDataAdapter adapter;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        private string _connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set { this._connectionString = value; }
        }

        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public bool Open()
        {
            try
            {
                this.connection = new MySqlConnection(this.ConnectionString);
                this.connection.Open();
                return true;
            }
            catch (Exception exception)
            {
                Logger.Error("MySQL Open connetion failed:", exception);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                Logger.Error("My SQL Close connection failed", exception);
                return false;
            }
        }

        public bool AddBlackListItem(BlackListItem blackListItem)
        {
            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText =
                        "INSERT INTO vehicle_blacklist(vehiclenumber,entrydatetime) VALUES(@number, @time)";
                    mySqlCommand.Parameters.AddWithValue("@number", blackListItem.VehicleNumber);
                    mySqlCommand.Parameters.AddWithValue("@time", this.ConvertDateTimeToMySqlFormat(DateTime.Now));

                    mySqlCommand.ExecuteNonQuery();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Add Blacklist Item Failed..", ex);
                return false;
            }
        }

        public List<BlackListItem> GetAllBlackListItem()
        {
            var blackListitems =  new List<BlackListItem>();
            if (this.Open())
            {

                //prepare query to get all records from items table
                string query = "select * from vehicle_blacklist";
                //prepare adapter to run query
                var adapter = new MySqlDataAdapter(query, connection);

                DataSet DS = new DataSet();

                //get query results in dataset
                adapter.Fill(DS);

                var table = DS.Tables[0];

                foreach (DataRow dr in table.Rows)
                {
                    blackListitems.Add(new BlackListItem(dr["vehiclenumber"].ToString()));
                }
            }

            return blackListitems;
        }

        public bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel)
        {
            throw new NotImplementedException();
        }

        public bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel)
        {
            throw new NotImplementedException();
        }

        public bool AddGateInfo(Gate gate)
        {
            throw new NotImplementedException();
        }

        public Gate ReadGateInfo()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbSearchResultModel> Search(DbSearchRequestModel dbSearchRequestModel)
        {
            throw new NotImplementedException();
        }

        private string ConvertDateTimeToMySqlFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd H:mm:ss");
        }

    }
}


/*
 
 * CREATE TABLE `scduvss`.`vehicle_blacklist` (
  `VehicleNumber` VARCHAR(20) NOT NULL COMMENT '',
  `EntryDateTime` DATETIME NOT NULL COMMENT '',
  PRIMARY KEY (`VehicleNumber`)  COMMENT '',
  UNIQUE INDEX `VehicleNumber_UNIQUE` (`VehicleNumber` ASC)  COMMENT '')
COMMENT = 'Black listed vehicle number';
 * 
 * 
 */