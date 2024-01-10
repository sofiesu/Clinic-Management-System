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
    /// Interaction logic for SpecificPatientWindow.xaml
    /// </summary>
    public partial class SpecificPatientWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public SpecificPatientWindow()
        {
            InitializeComponent();

            txtType.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtAge.IsEnabled = false;
            txtGender.IsEnabled = false;
            txtName.IsEnabled = false;
            txtContactNum.IsEnabled = false;
            txtDescription.IsEnabled = false;
            txtEmailAddress.IsEnabled = false;
            txtPatientID.IsEnabled = false;

            txtEAddress.IsEnabled = false;
            txtRelationship.IsEnabled = false;
            txtEEmailAddress.IsEnabled = false;
            txtEContactNumber.IsEnabled = false;
            txtEName.IsEnabled = false;

            txtDept.IsEnabled = false;
            txtAdvName.IsEnabled = false;
            AdvContactNum.IsEnabled = false;
            AdvEmailAddress.IsEnabled = false;

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
            List<uspSelectAllPatientResult> PatientResults = new List<uspSelectAllPatientResult>();
            PatientResults = db_con.uspSelectAllPatient().ToList();

            List<uspSelectAllEmergencyContactResult> EmergencyResults = new List<uspSelectAllEmergencyContactResult>();
            EmergencyResults = db_con.uspSelectAllEmergencyContact().ToList();

            List<uspSelectAllStudentAdviserResult> AdviserResults = new List<uspSelectAllStudentAdviserResult>();
            AdviserResults = db_con.uspSelectAllStudentAdviser().ToList();

            string PatientName = "";
            int PID = 0;

            for (int x = 0; x < PatientResults.Count; x++)
            {
                PatientName = PatientResults[x].PatientName;
                PID = (int)db_con.udfGetPatientID(PatientName);

                if (PID == ConstantValues.PID)
                {
                    txtType.Text = PatientResults[x].PatientType;
                    txtAddress.Text = PatientResults[x].PatientAddress;
                    txtAge.Text = PatientResults[x].PatientAge.ToString();
                    txtGender.Text = PatientResults[x].PatientGender;
                    txtName.Text = PatientResults[x].PatientName;
                    txtContactNum.Text = PatientResults[x].PatientNum;
                    txtDescription.Text = PatientResults[x].PatientDesc;
                    txtEmailAddress.Text = PatientResults[x].PatientEmail;
                    txtPatientID.Text = PID.ToString();
                }
            }

            for (int x = 0; x < EmergencyResults.Count; x++)
            {
                if (EmergencyResults[x].PatientID == PID)
                {
                    txtEAddress.Text = EmergencyResults[x].EmgyContactAddress;
                    txtRelationship.Text = EmergencyResults[x].EmgyRelationship;
                    txtEEmailAddress.Text = EmergencyResults[x].EmgyContactEmail;
                    txtEContactNumber.Text = EmergencyResults[x].EmgyContactNum;
                    txtEName.Text = EmergencyResults[x].EmgyContactName;
                }
            }

            for (int x = 0; x < AdviserResults.Count; x++)
            {
                if (AdviserResults[x].PatientID == PID)
                {
                    txtDept.Text = AdviserResults[x].AdviserDept;
                    txtAdvName.Text = AdviserResults[x].AdviserName;
                    AdvContactNum.Text = AdviserResults[x].AdviserNum;
                    AdvEmailAddress.Text = AdviserResults[x].AdviserEmail;
                }
            }
        }

    }
}
