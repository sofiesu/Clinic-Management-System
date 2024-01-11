using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ClinicEditInventoryWindow.xaml
    /// </summary>
    public partial class ClinicEditInventoryWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;
        int MID = 0;

        public ClinicEditInventoryWindow()
        {
            InitializeComponent();
            btnUpdateMedicine.IsEnabled = false;
            Fill();
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            // USP Add Medicine
            if(txtMedName.Text.Length > 0 && txtGenericName.Text.Length > 0 && txtManufacturer.Text.Length > 0 && txtExpirationDate.Text.Length > 0 && txtQuantity.Text.Length > 0)
            {
                db_con.uspAddMedicine(txtMedName.Text, txtGenericName.Text, DateTime.Parse(txtExpirationDate.Text), txtManufacturer.Text, int.Parse(txtQuantity.Text));
                MessageBox.Show("You have successfully added a medicine."
                           , "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                db_con.uspInsertLogs(ConstantValues.UID, "Added a medicine");

                MessageBox.Show("Medicine ID: " + db_con.udfGetMedicineID(txtMedName.Text), "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK); 
            }
        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            // USP Update Medicine
            if (txtMedName.Text.Length > 0 && txtGenericName.Text.Length > 0 && txtManufacturer.Text.Length > 0 && txtExpirationDate.Text.Length > 0 && txtQuantity.Text.Length > 0)
            {
                db_con.uspUpdateMedicine(MID, txtMedName.Text, txtGenericName.Text, DateTime.Parse(txtExpirationDate.Text), txtManufacturer.Text, int.Parse(txtQuantity.Text));
                MessageBox.Show("You have successfully updated a medicine."
                           , "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                db_con.uspInsertLogs(ConstantValues.UID, "Updated a medicine");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdateMedicine.IsEnabled = true;
            btnAddMedicine.IsEnabled = false;
            Clear();

            string MedName = cbName.SelectedItem.ToString();

            List<uspSelectAllMedicineResult> MedicineResults = new List<uspSelectAllMedicineResult>();
            MedicineResults = db_con.uspSelectAllMedicine().ToList();

            for (int x = 0; x < MedicineResults.Count; x++)
            {
                if(MedicineResults[x].MedicineName == MedName)
                {
                    txtQuantity.Text = MedicineResults[x].MedQTY.ToString();
                    txtManufacturer.Text = MedicineResults[x].MedManufacturer;
                    txtMedName.Text = MedicineResults[x].MedicineName;
                    txtExpirationDate.Text = MedicineResults[x].MedExpDate.ToString();
                    txtGenericName.Text = MedicineResults[x].MedicineType.ToString();
                    MID = (int)db_con.udfGetMedicineID(MedName);
                }
            }
        }

        private void Clear()
        {
            txtQuantity.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtMedName.Text = string.Empty;
            txtExpirationDate.Text = string.Empty;
            txtGenericName.Text = string.Empty;
        }

        private void Fill()
        {
            List<uspSelectAllMedicineResult> MedicineResults = new List<uspSelectAllMedicineResult>();
            MedicineResults = db_con.uspSelectAllMedicine().ToList();
            
            for (int x = 0; x < MedicineResults.Count; x++)
            {
                cbName.Items.Add(MedicineResults[x].MedicineName);
            }
        }
    }
}
