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
using System.Xml.Linq;

namespace Clinic_Management_System
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public PatientWindow()
        {
            InitializeComponent();
            Fill();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void lbResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string PatientName = lbPatientResults.SelectedItem.ToString();
            int PID = (int)db_con.udfGetPatientID(PatientName);

            ConstantValues.PID = PID;

            SpecificPatientWindow specificPatientWindow = new SpecificPatientWindow();
            specificPatientWindow.Show();
            this.Close();
        }

        private void btnRegisterPatient_Click(object sender, RoutedEventArgs e)
        {
            EditPatientWindow editPatientWindow = new EditPatientWindow();
            editPatientWindow.Show();
            this.Close();
        }

        private void Fill()
        {
            List<uspSelectAllPatientResult> PatientResults = new List<uspSelectAllPatientResult>();
            PatientResults = db_con.uspSelectAllPatient().ToList();

            for (int x = 0; x < PatientResults.Count; x++)
            {
                int age = PatientResults[x].PatientAge;
                string gender = PatientResults[x].PatientGender;
                string type = PatientResults[x].PatientType;
                string fullname = PatientResults[x].PatientName;

                if (!cbAge.Items.Contains(age))
                {
                    cbAge.Items.Add(PatientResults[x].PatientAge);
                }

                if (!cbGender.Items.Contains(gender))
                {
                    cbGender.Items.Add(PatientResults[x].PatientGender);
                }

                if (!cbType.Items.Contains(type))
                {
                    cbType.Items.Add(PatientResults[x].PatientType);
                }

                if (!lbPatientResults.Items.Contains(fullname))
                {
                    lbPatientResults.Items.Add(PatientResults[x].PatientName);
                }
            }
        }

        private void cbAge_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Age
        {
            if(cbAge.SelectedIndex != -1)
            {
                string age = cbAge.SelectedItem.ToString();

                lbPatientResults.Items.Clear();
                txtPatientName.Text = "";

                List<uspSelectPatientByAgeResult> Age = new List<uspSelectPatientByAgeResult>();
                Age = db_con.uspSelectPatientByAge(int.Parse(age)).ToList();

                if (cbGender.SelectedIndex == -1 && cbType.SelectedIndex == -1)
                {
                    foreach (var a in Age)
                    {
                        lbPatientResults.Items.Add(a.PatientName);
                    }
                }

                if (cbGender.SelectedIndex != -1 && cbType.SelectedIndex == -1)
                {
                    foreach (var a in Age)
                    {
                        if (a.PatientGender == cbGender.SelectedItem.ToString())
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbGender.SelectedIndex == -1 && cbType.SelectedIndex != -1)
                {
                    foreach (var a in Age)
                    {
                        if (a.PatientType == cbType.SelectedItem.ToString())
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbGender.SelectedIndex != -1 && cbType.SelectedIndex != -1)
                {
                    foreach (var a in Age)
                    {
                        if (a.PatientType == cbType.SelectedItem.ToString() && a.PatientGender == cbGender.SelectedItem.ToString())
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }
            }
            
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e) // Search by Name
        {
            string name = txtPatientName.Text;

            lbPatientResults.Items.Clear();
            cbAge.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            cbType.SelectedIndex = -1;

            List<uspSelectPatientByFullNameResult> PatientFN = new List<uspSelectPatientByFullNameResult>();
            PatientFN = db_con.uspSelectPatientByFullName(name).ToList();

            foreach (var a in PatientFN)
            {
                lbPatientResults.Items.Add(a.PatientName);
            }

        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Gender
        {
            if(cbGender.SelectedIndex != -1)
            {
                string gender = cbGender.SelectedItem.ToString();

                lbPatientResults.Items.Clear();
                txtPatientName.Text = "";

                List<uspSelectPatientByGenderResult> Gender = new List<uspSelectPatientByGenderResult>();
                Gender = db_con.uspSelectPatientByGender(gender).ToList();

                if (cbAge.SelectedIndex == -1 && cbType.SelectedIndex == -1)
                {
                    foreach (var a in Gender)
                    {
                        lbPatientResults.Items.Add(a.PatientName);
                    }
                }

                if (cbAge.SelectedIndex != -1 && cbType.SelectedIndex == -1)
                {
                    foreach (var a in Gender)
                    {
                        if (a.PatientAge == int.Parse(cbAge.SelectedItem.ToString()))
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbAge.SelectedIndex == -1 && cbType.SelectedIndex != -1)
                {
                    foreach (var a in Gender)
                    {
                        if (a.PatientType == cbType.SelectedItem.ToString())
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbAge.SelectedIndex != -1 && cbType.SelectedIndex != -1)
                {
                    foreach (var a in Gender)
                    {
                        if (a.PatientType == cbType.SelectedItem.ToString() && a.PatientAge == int.Parse(cbAge.SelectedItem.ToString()))
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }
            }
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Type
        {
            if(cbType.SelectedIndex != -1)
            {
                string type = cbType.SelectedItem.ToString();

                lbPatientResults.Items.Clear();
                txtPatientName.Text = "";

                List<uspSelectPatientByTypeResult> Type = new List<uspSelectPatientByTypeResult>();
                Type = db_con.uspSelectPatientByType(type).ToList();

                if (cbAge.SelectedIndex == -1 && cbGender.SelectedIndex == -1)
                {
                    foreach (var a in Type)
                    {
                        lbPatientResults.Items.Add(a.PatientName);
                    }
                }

                if (cbAge.SelectedIndex != -1 && cbGender.SelectedIndex == -1)
                {
                    foreach (var a in Type)
                    {
                        if (a.PatientAge == int.Parse(cbAge.SelectedItem.ToString()))
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbAge.SelectedIndex == -1 && cbGender.SelectedIndex != -1)
                {
                    foreach (var a in Type)
                    {
                        if (a.PatientGender == cbGender.SelectedItem.ToString())
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }

                if (cbAge.SelectedIndex != -1 && cbGender.SelectedIndex != -1)
                {
                    foreach (var a in Type)
                    {
                        if (a.PatientGender == cbGender.SelectedItem.ToString() && a.PatientAge == int.Parse(cbAge.SelectedItem.ToString()))
                        {
                            lbPatientResults.Items.Add(a.PatientName);
                        }
                    }
                }
            }
        }

        
    }
}
