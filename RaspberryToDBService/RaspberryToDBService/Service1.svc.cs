using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RaspberryToDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString =
           "Server=tcp:lyssensor-server.database.windows.net,1433;Initial Catalog=LysSensorDB;Persist Security Info=False;User ID=gruppe6;Password=Hejmeddig1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public int InsertMeasurement(int lightvalue)
        {
            const string insertData = "insert into Lightsensor(lightvalue) values (@lightvalue)";

            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertData, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@lightvalue", lightvalue);
                    
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
