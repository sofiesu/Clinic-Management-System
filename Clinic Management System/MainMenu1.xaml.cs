using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainMenu1.xaml
    /// </summary>
    public partial class MainMenu1 : Window
    {
        ConstantValues ConstantValues = new ConstantValues();
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public MainMenu1()
        {
            InitializeComponent();
            MedFN.Content = ConstantValues.FullName;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            db_con.uspInsertLogs(ConstantValues.UID, "Logged Out");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnClinicOverview_Click(object sender, RoutedEventArgs e)
        {
            ClinicInventoryWindow clinicInventoryWindow = new ClinicInventoryWindow();
            clinicInventoryWindow.Show();
            this.Close();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            ClinicEditInventoryWindow clinicEditInventoryWindow = new ClinicEditInventoryWindow();
            clinicEditInventoryWindow.Show();
            this.Close();
        }

        private void btnViewPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow patientWindow = new PatientWindow();
            patientWindow.Show();
            this.Close();
        }

        private void btnRegisterPatient_Click(object sender, RoutedEventArgs e)
        {
            EditPatientWindow editPatientWindow = new EditPatientWindow();
            editPatientWindow.Show();
            this.Close();
        }

        private void btnClinicVisits_Click(object sender, RoutedEventArgs e)
        {
            ClinicVisits clinicVisits = new ClinicVisits();
            clinicVisits.Show();
            this.Close();
        }
    }
}
