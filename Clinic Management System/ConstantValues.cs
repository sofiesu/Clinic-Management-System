using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_System
{
    internal class ConstantValues
    {
        //Edit the connection string here
        public static ClinicDatabaseDataContext DBConnectionString = new ClinicDatabaseDataContext
            (Properties.Settings.Default.ClinicManagementSystem_Group3ConnectionString);

        public static string FullName = "";
        public static int UID = 0;
        public static int MID = 0;
        public static int PID = 0;
        public static int AID = 0;
        public static string type = "";

    }
}
