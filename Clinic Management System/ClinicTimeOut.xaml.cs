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
    /// Interaction logic for ClinicTimeOut.xaml
    /// </summary>
    public partial class ClinicTimeOut : Window
    {
        ClinicDatabaseDataContext clinicDB = ConstantValues.DBConnectionString;
        List<uspSelectAllPatientResult> patients = new List<uspSelectAllPatientResult>();
        List<uspSelectPatientTimeInResult> timeins = new List<uspSelectPatientTimeInResult>();
        List<uspSelectAvailMedsResult> availMeds = new List<uspSelectAvailMedsResult>();
        List<uspSelectAvailSuppliesResult> availSupps = new List<uspSelectAvailSuppliesResult>();

        List<DateTime> timeins1 = new List<DateTime>();
        public ClinicTimeOut()
        {
            InitializeComponent();

            string pName = "";
            string medName = "";
            string suppName = "";

            patients = clinicDB.uspSelectAllPatient().ToList();
            foreach (var p in patients)
            {
                pName = p.PatientName;
                cbPName.Items.Add(pName);
            }

            availMeds = clinicDB.uspSelectAvailMeds().ToList();
            foreach(var m in availMeds)
            {
                medName = m.MedicineName;
                cbMeds.Items.Add(medName);
            }

            availSupps = clinicDB.uspSelectAvailSupplies().ToList();
            foreach (var s in availSupps)
            {
                suppName = s.SupplyName;
                cbSupplies.Items.Add(suppName);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void cbPName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbTimeIn.Items.Clear();
            string timein = "";
            string pName = "";
            string patient = (string)cbPName.SelectedItem;

            timeins = clinicDB.uspSelectPatientTimeIn(patient).ToList();

            patients = clinicDB.uspSelectAllPatient().ToList();
            foreach (var p in patients)
            {
                pName = p.PatientName;

                foreach (var t in timeins)
                {
                    timein = t.PatientTimeIn.ToString();
                    if (patient == pName)
                    {
                        cbTimeIn.Items.Add(timein);
                        timeins1.Add(t.PatientTimeIn);
                    }
                }
            }
        }

        private void btnAddMed_Click(object sender, RoutedEventArgs e)
        {
            int qty = int.Parse(qtyMedTB.Text);
            string med = (string)cbMeds.SelectedItem;
            

            MessageBoxResult res = MessageBox.Show("Confirming... Medicine: " + med + " Qty: " + qty + ". Are you sure with this?"
                , "Confirmation Message"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                string patient = (string)cbPName.SelectedItem;
                int pID = (int)clinicDB.udfGetPatientID(patient);
                DateTime timein = timeins1.ElementAt(cbTimeIn.SelectedIndex);

                int mID = (int)clinicDB.udfGetMedicineID(med);
                int vID = (int)clinicDB.udfGetVisitID(pID, timein);

                clinicDB.uspInsertGivenMeds(vID, mID, qty);
                clinicDB.uspInsertLogs(ConstantValues.UID, "Recorded administered medicine(s) to Patient " + patient);
                MessageBox.Show("You have successfully recorded the administered medicine to Patient " + patient);
            }
            else
            {
                cbMeds.Text = "";
                qtyMedTB.Text = "";
            }

            cbMeds.Text = "";
            qtyMedTB.Text = "";
        }

        private void btnAddSupply_Click(object sender, RoutedEventArgs e)
        {
            int qty = int.Parse(qtySuppTB.Text);
            string supply = (string)cbSupplies.SelectedItem;

            MessageBoxResult res = MessageBox.Show("Confirming... Supply: " + supply + " Qty: " + qty + ". Are you sure with this?"
                , "Confirmation Message"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                string patient = (string)cbPName.SelectedItem;
                int pID = (int)clinicDB.udfGetPatientID(patient);

                DateTime timein = timeins1.ElementAt(cbTimeIn.SelectedIndex);

                int sID = (int)clinicDB.udfGetSupplyID(supply);
                int vID = (int)clinicDB.udfGetVisitID(pID, timein);

                clinicDB.uspInsertGivenSupplies(vID, sID, qty);
                clinicDB.uspInsertLogs(ConstantValues.UID, "Recorded given medical supply(s) to Patient " + patient);
                MessageBox.Show("You have successfully recorded the given medical supply to Patient " + patient);
            }
            else
            {
                cbSupplies.Text = "";
                qtySuppTB.Text = "";
            }
            cbSupplies.Text = "";
            qtySuppTB.Text = "";
        }

        private void btnTimeOut_Click(object sender, RoutedEventArgs e)
        {
            string patient = (string)cbPName.SelectedItem;
            int pID = (int)clinicDB.udfGetPatientID(patient);
            DateTime timein = timeins1.ElementAt(cbTimeIn.SelectedIndex);

            int vID = (int)clinicDB.udfGetVisitID(pID, timein);

            string notes = notesTB.Text;

            clinicDB.uspPatientTimeOut(vID, notes);
            clinicDB.uspInsertLogs(ConstantValues.UID, "Patient has checked out");

            MessageBox.Show("Checked out Patient " + patient);

            cbPName.Text = "";
            cbTimeIn.Text = "";
            notesTB.Text = "";
        }
    }
}
