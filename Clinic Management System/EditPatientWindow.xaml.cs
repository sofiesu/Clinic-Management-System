using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
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
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;
        int PID = 0;
        string Patienttype = "";

        public EditPatientWindow()
        {
            InitializeComponent();
            btnUpdate.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // USP Add Patient
            if (txtPatientName.Text.Length > 0 && txtAge.Text.Length > 0 && txtGender.Text.Length > 0 && txtAddress.Text.Length > 0 && txtContactNumber.Text.Length > 0 && txtAddress.Text.Length > 0 &&
            txtType.Text.Length > 0 && txtDescription.Text.Length > 0)
            {
                db_con.uspAddPatient(txtPatientName.Text, int.Parse(txtAge.Text), txtGender.Text, txtType.Text, txtDescription.Text, txtContactNumber.Text, txtEmailAddress.Text
                    , txtAddress.Text);
                MessageBox.Show("You have successfully added a patient."
                           , "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                db_con.uspInsertLogs(ConstantValues.UID, "Added a patient");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // USP Update Patient
            if (txtPatientName.Text.Length > 0 && txtAge.Text.Length > 0 && txtGender.Text.Length > 0 && txtAddress.Text.Length > 0 && txtContactNumber.Text.Length > 0 && txtAddress.Text.Length > 0 &&
            txtType.Text.Length > 0 && txtDescription.Text.Length > 0)
            {
                db_con.uspUpdatePatient(PID, txtPatientName.Text, int.Parse(txtAge.Text), txtGender.Text, txtType.Text, txtDescription.Text, txtContactNumber.Text, txtEmailAddress.Text
                    , txtAddress.Text);

                //uspUpdateEmergencyContact

                //uspUpdateAdviserInfo

                MessageBox.Show("You have successfully updated a patient."
                           , "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
                db_con.uspInsertLogs(ConstantValues.UID, "Updated a patient");
            }
        }

        private void Clear()
        {
            txtPatientName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtType.Text = string.Empty;
            txtDescription.Text = string.Empty;
            
            txtEmailAddress.Text = string.Empty;
            txtEmergencyAddress.Text = string.Empty;
            txtEmergencyContactNum.Text = string.Empty;
            txtEmergencyEmailAdd.Text = string.Empty;
            txtEmergencyName.Text = string.Empty;
            txtRelationship.Text = string.Empty;
            
            txtAdvEmailAdd.Text = string.Empty;
            txtAdviserName.Text = string.Empty;
            txtAdvContactNum.Text= string.Empty;
            txtDept.Text = string.Empty;
        }

        private void Fill()
        {
            List<uspSelectAllPatientResult> PatientResults = new List<uspSelectAllPatientResult>();
            PatientResults = db_con.uspSelectAllPatient().ToList();

            for (int x = 0; x < PatientResults.Count; x++)
            {
                cbName.Items.Add(PatientResults[x].PatientName);
            }
        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdate.IsEnabled = true;
            btnAdd.IsEnabled = false;
            Clear();

            string PatientName = cbName.SelectedItem.ToString();

            List<uspSelectAllPatientResult> PatientResults = new List<uspSelectAllPatientResult>();
            PatientResults = db_con.uspSelectAllPatient().ToList();

            List<uspSelectAllEmergencyContactResult> EmergencyResults = new List<uspSelectAllEmergencyContactResult>();
            EmergencyResults = db_con.uspSelectAllEmergencyContact().ToList();

            List<uspSelectAllStudentAdviserResult> AdviserResults = new List<uspSelectAllStudentAdviserResult>();
            AdviserResults = db_con.uspSelectAllStudentAdviser().ToList();

            for (int x = 0; x < PatientResults.Count; x++)
            {
                if (PatientResults[x].PatientName == PatientName)
                {
                    txtPatientName.Text = PatientResults[x].PatientName;
                    txtAge.Text = PatientResults[x].PatientAge.ToString();
                    txtGender.Text = PatientResults[x].PatientGender;
                    txtAddress.Text = PatientResults[x].PatientAddress;
                    txtContactNumber.Text = PatientResults[x].PatientNum;
                    txtAddress.Text = PatientResults[x].PatientAddress;
                    txtType.Text = PatientResults[x].PatientType;
                    txtDescription.Text = PatientResults[x].PatientDesc;
                    txtEmailAddress.Text =  PatientResults[x].PatientEmail;

                    PID = (int)db_con.udfGetPatientID(PatientName);
                    Patienttype = txtType.Text;
                }
            }

            for (int x = 0; x < EmergencyResults.Count; x++)
            {
                if (EmergencyResults[x].PatientID == PID)
                {
                    txtEmergencyAddress.Text = EmergencyResults[x].EmgyContactAddress;
                    txtEmergencyContactNum.Text = EmergencyResults[x].EmgyContactNum;
                    txtEmergencyEmailAdd.Text = EmergencyResults[x].EmgyContactEmail;
                    txtEmergencyName.Text = EmergencyResults[x].EmgyContactName;
                    txtRelationship.Text = EmergencyResults[x].EmgyRelationship;
                }
            }

            if(Patienttype == "Student")
            {
                for (int x = 0; x < AdviserResults.Count; x++)
                {
                    if (AdviserResults[x].PatientID == PID)
                    {
                        txtAdvEmailAdd.Text = AdviserResults[x].AdviserEmail;
                        txtAdviserName.Text = AdviserResults[x].AdviserName;
                        txtAdvContactNum.Text = AdviserResults[x].AdviserNum;
                        txtDept.Text = AdviserResults[x].AdviserDept;
                    }
                }
            }

            DisableMode(Patienttype);
        }

        private void DisableMode(string type)
        {
            if(type == "Student")
            {
                txtAdvEmailAdd.IsEnabled = true;
                txtAdviserName.IsEnabled = true;
                txtAdvContactNum.IsEnabled = true;
                txtDept.IsEnabled = true;
            }
        }
    }
}
