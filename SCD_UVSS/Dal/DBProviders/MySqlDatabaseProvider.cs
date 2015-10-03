using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using log4net;
using MySql.Data.MySqlClient;
using SCD_UVSS.helpers;
using SCD_UVSS.Model;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.Dal.DBProviders
{
    public class MySqlDatabaseProvider : IDatabaseProvider
    {
        MySqlConnection connection;
        //MySqlDataAdapter adapter;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(GateSetupViewModel));

        //string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false",server.Text, userid.Text, password.Text );

        //SERVER=localhost;DATABASE=scduvss;UID=root;PASSWORD=Welcome@123;
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
            const string insertQueryFormat =
                "INSERT INTO vehicle_blacklist(vehiclenumber,entrydatetime) VALUES(@number, @time)";
            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText = insertQueryFormat;
                    mySqlCommand.Parameters.AddWithValue("@number", blackListItem.VehicleNumber);
                    mySqlCommand.Parameters.AddWithValue("@time", DateTime.Now.ConvertToMySqlFormat());

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
                const string query = "select * from vehicle_blacklist";
                
                var adapter = new MySqlDataAdapter(query, connection);

                var dataTable = new DataTable();

                //get query results in dataset
                adapter.Fill(dataTable);
                
                blackListitems.AddRange(from DataRow dr in dataTable.Rows select new BlackListItem(dr["vehiclenumber"].ToString()));
            }

            return blackListitems;
        }

        public bool AddVehicleEntryBasicInfo(VehicleBasicInfoModel vehicleBasicInfoModel)
        {
            const string insertQueryFormat =
                "INSERT INTO vehicle_entry_info(id,number, entrytime) VALUES(@id, @vehiclenumber, @time)";
            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText = insertQueryFormat;
                    mySqlCommand.Parameters.AddWithValue("@id", vehicleBasicInfoModel.UniqueEntryId);
                    mySqlCommand.Parameters.AddWithValue("@vehiclenumber",  vehicleBasicInfoModel.Number);
                    mySqlCommand.Parameters.AddWithValue("@time", vehicleBasicInfoModel.DateTime.ConvertToMySqlFormat());

                    mySqlCommand.ExecuteNonQuery();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Add AddVehicleEntryBasicInfo Item Failed..", ex);
                return false;
            }
        }

        public bool AddVehicleEntryImges(VehicleImagesModel vehicleImagesModel)
        {
            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText =
                        "INSERT INTO vehicle_entry_images(id,chasis_image, driver_image, car_full_image) VALUES(@id, @chasisImage, @driverImage, @overallImage)";
                    mySqlCommand.Parameters.AddWithValue("@id", vehicleImagesModel.ForeignKeyId);
                    mySqlCommand.Parameters.AddWithValue("@chasisImage", vehicleImagesModel.ChaisisImage);
                    mySqlCommand.Parameters.AddWithValue("@driverImage", vehicleImagesModel.DriverImage);
                    mySqlCommand.Parameters.AddWithValue("@overallImage", vehicleImagesModel.VehicleOverallImage);

                    mySqlCommand.ExecuteNonQuery();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("Add AddVehicleEntryImges Item Failed..", ex);
                return false;
            }
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
            /*
                SELECT *
                FROM scduvss.vehicle_entry_info
                WHERE number = "TN 07 1234"
                AND entrytime >= '2015-10-01' AND entrytime < '2015-11-01'
                AND DATE_FORMAT(entrytime, '%H:%i:s') >= '10:00:00' AND DATE_FORMAT(entrytime, '%H:%i:s') > '22:00:00' 
             */
            var dbSearchResults = new List<DbSearchResultModel>();
            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText =
                        "SELECT * FROM vehicle_entry_info " +
                        "WHERE number = @vehicleNumber " +
                        "AND entrytime >= @entryDateGreaterThan AND entrytime < @entryDateLessThan " +
                        "AND DATE_FORMAT(entrytime, '%H:%i:s') >= @entryTimeGreaterThan AND DATE_FORMAT(entrytime, '%H:%i:s') < @entryTimeLessThan";

                    mySqlCommand.Parameters.AddWithValue("@vehicleNumber", dbSearchRequestModel.VehicleNumber);

                    mySqlCommand.Parameters.AddWithValue("@entryDateGreaterThan",
                        dbSearchRequestModel.StaDateTime.GetDatePortion());
                    mySqlCommand.Parameters.AddWithValue("@entryDateLessThan",
                        dbSearchRequestModel.EnDateTime.GetDatePortion());

                    mySqlCommand.Parameters.AddWithValue("@entryTimeGreaterThan",
                        dbSearchRequestModel.StaDateTime.GetTimePortion());
                    mySqlCommand.Parameters.AddWithValue("@entryTimeLessThan",
                        dbSearchRequestModel.EnDateTime.GetTimePortion());

                    var dataTable = new DataTable();
                    
                    var adapter = new MySqlDataAdapter(mySqlCommand);
                    adapter.Fill(dataTable);

                    dbSearchResults.AddRange(
                            from DataRow dr in dataTable.Rows
                            select new DbSearchResultModel
                            {
                                UniqueId = dr["id"].ToString(),
                                EntryDateTime = (DateTime) dr["entrytime"],
                                VehicleNumber = dr["number"].ToString()
                            }
                        );
                }
                return dbSearchResults;
            }
            catch (Exception ex)
            {
                Logger.Error("Search Failed..", ex);
                return null;
            }
        }

        public DbImageResult GetImageResult(string uniqueId)
        {
            var dbImageResult = new DbImageResult();

            try
            {
                if (this.Open())
                {
                    var mySqlCommand = this.connection.CreateCommand();

                    mySqlCommand.CommandText =
                        "SELECT * FROM vehicle_entry_images " +
                        "WHERE id = @uniqueId ";

                    mySqlCommand.Parameters.AddWithValue("@uniqueId", uniqueId);
                    
                    var dataTable = new DataTable();

                    var adapter = new MySqlDataAdapter(mySqlCommand);
                    adapter.Fill(dataTable);

                    var firstRow = dataTable.Rows[0];
                    dbImageResult.CarFullImage = firstRow["car_full_image"] as byte[];
                    dbImageResult.ChasisImage = firstRow["chasis_image"] as byte[];
                    dbImageResult.DriverImage = firstRow["driver_image"] as byte[];

                }
                return dbImageResult;
            }
            catch (Exception ex)
            {
                Logger.Error("Search Failed..", ex);
                return null;
            }
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


/* ALTER TABLE `scduvss`.`vehicle_entry_images` 
DROP FOREIGN KEY `vehicle_entry_id`;
ALTER TABLE `scduvss`.`vehicle_entry_images` 
ADD CONSTRAINT `vehicle_entry_id`
  FOREIGN KEY (`id`)
  REFERENCES `scduvss`.`vehicle_entry_info` (`id`)
  ON DELETE CASCADE
  ON UPDATE NO ACTION;
 */