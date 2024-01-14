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
    /// Interaction logic for ClinicVisits.xaml
    /// </summary>
    public partial class ClinicVisits : Window
    {
        ClinicDatabaseDataContext clinicDB = ConstantValues.DBConnectionString;
        List<uspSelectAllPatientwAdviserResult> patients = new List<uspSelectAllPatientwAdviserResult>();
        List<uspSelectNursesMedProResult> users = new List<uspSelectNursesMedProResult>();
        int pID = 0;
        int uID = 0;
        public ClinicVisits()
        {
            InitializeComponent();

            #region combobox
            string pName = "";
            string gender = "";
            int age = 0;
            string pType = "";
            string pDesc = "";
            string pNum = "";
            string pEmail = "";
            string pAddress = "";
            string aName = "";
            string aEmail = "";
            string aNum = "";
            string aDept = "";

            patients = clinicDB.uspSelectAllPatientwAdviser().ToList();
            foreach (var p in patients)
            {
                pName = p.PatientName;
                gender = p.PatientGender;
                age = p.PatientAge;
                pType = p.PatientType;
                pDesc = p.PatientDesc;
                pNum = p.PatientNum;
                pEmail = p.PatientEmail;
                pAddress = p.PatientAddress;
                aName = p.AdviserName;
                aEmail = p.AdviserEmail;
                aNum = p.AdviserNum;
                aDept = p.AdviserDept;

                cbPatients.Items.Add(pName);
            }

            int uID = 0;
            string uName = "";
            string userName = "";
            string pw = "";
            string uEmail = "";
            string uNum = "";
            string uType = "";

            users = clinicDB.uspSelectNursesMedPro().ToList();
            foreach (var u in users)
            {
                uID = u.UserID;
                uName = u.UserFullName;
                userName = u.UserName;
                uEmail = u.UserEmail;
                pw = u.UserPassword;
                uNum = u.UserContact;
                uType = u.UserType;
                cbNurses.Items.Add(uName);
            } 
            #endregion
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void timeinbtn_Click(object sender, RoutedEventArgs e)
        {
            string pName = (string)cbPatients.SelectedItem;
            pID = (int)clinicDB.udfGetPatientID(pName);
            string uName = (string)cbNurses.SelectedItem;
            uID = (int)clinicDB.udfGetUserID(uName);

            clinicDB.uspAddClinicVisitInfo(pID, uID);
            clinicDB.uspInsertLogs(uID, "A patient has been checked in");

            MessageBox.Show("Checked in patient " + pName);
        }

        private void cbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            patientDeetsLB.Items.Clear();
            string patient = (string)cbPatients.SelectedItem;

            string pName = "";
            string gender = "";
            int age = 0;
            string pType = "";
            string pDesc = "";
            string pNum = "";
            string pEmail = "";
            string pAddress = "";
            string aName = "";
            string aEmail = "";
            string aNum = "";
            string aDept = "";

            patients = clinicDB.uspSelectAllPatientwAdviser().ToList();
            foreach (var p in patients)
            {
                pName = p.PatientName;
                gender = p.PatientGender;
                age = p.PatientAge;
                pType = p.PatientType;
                pDesc = p.PatientDesc;
                pNum = p.PatientNum;
                pEmail = p.PatientEmail;
                pAddress = p.PatientAddress;
                aName = p.AdviserName;
                aEmail = p.AdviserEmail;
                aNum = p.AdviserNum;
                aDept = p.AdviserDept;

                if (patient == pName)
                {
                    patientDeetsLB.Items.Add("Patient Name: " + pName + "\nGender: " + gender + "\nAge: " + age
                        + "\nPatient Type: " + pType + "\nDescription: " + pDesc + "\nContact Number: " + pNum + "\nEmail Address: " + pEmail
                        + "\nHome Address: " + pAddress + "\n\nStudent Adviser Details:\nAdviser Name: " + aName + "\nAdviser Email: " + aEmail
                        + "\nAdviser Number: " + aNum + "\nDepartment: " + aDept);
                }
            }
        }
    }
}
