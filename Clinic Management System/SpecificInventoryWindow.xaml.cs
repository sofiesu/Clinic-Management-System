using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clinic_Management_System
{
    /// <summary>
    /// Interaction logic for SpecificInventoryWindow.xaml
    /// </summary>
    public partial class SpecificInventoryWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public SpecificInventoryWindow()
        {
            InitializeComponent();

            txtExpDate.IsEnabled = false;
            txtGenericName.IsEnabled = false;
            txtName.IsEnabled = false;
            txtLastUpd.IsEnabled = false;
            txtManufacturer.IsEnabled = false;
            txtQuantity.IsEnabled = false;  
            txtStatus.IsEnabled = false;

            Fill();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void Fill()
        {
            List<uspSelectAllMedicineResult> MedicineResults = new List<uspSelectAllMedicineResult>();
            MedicineResults = db_con.uspSelectAllMedicine().ToList();

            for (int x = 0; x < MedicineResults.Count; x++)
            {
                string MedName = MedicineResults[x].MedicineName;
                int MID = (int)db_con.udfGetMedicineID(MedName);

                if (MID == ConstantValues.MID)
                {
                    txtExpDate.Text = MedicineResults[x].MedExpDate.ToString();
                    txtGenericName.Text = MedicineResults[x].MedicineType;
                    txtName.Text = MedicineResults[x].MedicineName;
                    txtLastUpd.Text = MedicineResults[x].MedLastUpdated.ToString();
                    txtManufacturer.Text = MedicineResults[x].MedManufacturer;
                    txtQuantity.Text = MedicineResults[x].MedQTY.ToString();
                    txtStatus.Text = MedicineResults[x].MedStatus.ToString();
                    lbl_ID.Content = MID;
                }
            }
        }
    }
}
