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
using static System.Net.Mime.MediaTypeNames;

namespace Clinic_Management_System
{
    /// <summary>
    /// Interaction logic for ClinicInventoryWindow.xaml
    /// </summary>
    public partial class ClinicInventoryWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public ClinicInventoryWindow()
        {
            InitializeComponent();
            Fill();

            if(ConstantValues.type == "Admin")
            {
                btnAddInventory.IsEnabled = false;
            }
        }

        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            string name = txtMedicineName.Text;

            lbMedicineResults.Items.Clear();
            cbManufacturer.SelectedIndex = -1;
            cbExpDate.SelectedIndex = -1;
            cbBrandName.SelectedIndex = -1;

            List<uspSelectMedicineByGenericNameResult> GenName = new List<uspSelectMedicineByGenericNameResult>();
            GenName = db_con.uspSelectMedicineByGenericName(name).ToList();

            foreach (var a in GenName)
            {
                lbMedicineResults.Items.Add(a.MedicineName);
            }
        }

        private void btnAddInventory_Click(object sender, RoutedEventArgs e)
        {
            ClinicEditInventoryWindow clinicEditInventoryWindow = new ClinicEditInventoryWindow();
            clinicEditInventoryWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void lbMedicineResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string MedName = lbMedicineResults.SelectedItem.ToString();
            int MID = (int)db_con.udfGetMedicineID(MedName);

            ConstantValues.MID = MID;

            SpecificInventoryWindow specificInventoryWindow = new SpecificInventoryWindow();
            specificInventoryWindow.Show();
            this.Close();
        }

        private void Fill()
        {
            List<uspSelectAllMedicineResult> MedicineResults = new List<uspSelectAllMedicineResult>();
            MedicineResults = db_con.uspSelectAllMedicine().ToList();

            for (int x = 0; x < MedicineResults.Count; x++)
            {
                string MedName = MedicineResults[x].MedicineName;
                string ExpDate = MedicineResults[x].MedExpDate.ToString();
                string Manufacturer = MedicineResults[x].MedManufacturer;
                string BrandName = MedicineResults[x].MedicineType;

                if (!cbBrandName.Items.Contains(BrandName))
                {
                    cbBrandName.Items.Add(MedicineResults[x].MedicineType);
                }

                if (!cbExpDate.Items.Contains(ExpDate))
                {
                    cbExpDate.Items.Add(MedicineResults[x].MedExpDate);
                }

                if (!cbManufacturer.Items.Contains(Manufacturer))
                {
                    cbManufacturer.Items.Add(MedicineResults[x].MedManufacturer);
                }

                if (!lbMedicineResults.Items.Contains(MedName))
                {
                    lbMedicineResults.Items.Add(MedicineResults[x].MedicineName);
                }
            }
        }

        private void cbBrandName_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Brand Name
        {
            if (cbBrandName.SelectedIndex != -1)
            {
                string brand = cbBrandName.SelectedItem.ToString();

                lbMedicineResults.Items.Clear();
                txtMedicineName.Text = "";

                List<uspSelectMedicineByBrandNameResult> Brand = new List<uspSelectMedicineByBrandNameResult>();
                Brand = db_con.uspSelectMedicineByBrandName(brand).ToList();


                if (cbManufacturer.SelectedIndex == -1 && cbExpDate.SelectedIndex == -1)
                {
                    foreach (var a in Brand)
                    {
                        lbMedicineResults.Items.Add(a.MedicineName);
                    }
                }

                if (cbManufacturer.SelectedIndex != -1 && cbExpDate.SelectedIndex == -1)
                {
                    foreach (var a in Brand)
                    {
                        if (a.MedManufacturer == cbManufacturer.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbManufacturer.SelectedIndex == -1 && cbExpDate.SelectedIndex != -1)
                {
                    foreach (var a in Brand)
                    {
                        if (a.MedExpDate.ToString() == cbExpDate.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbManufacturer.SelectedIndex != -1 && cbExpDate.SelectedIndex != -1)
                {
                    foreach (var a in Brand)
                    {
                        if (a.MedManufacturer == cbManufacturer.SelectedItem.ToString() && a.MedExpDate.ToString() == cbExpDate.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }
            }
        }

        private void cbExpDate_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Exp Date
        {
            if (cbExpDate.SelectedIndex != -1)
            {
                string expDate = cbExpDate.SelectedItem.ToString();

                lbMedicineResults.Items.Clear();
                txtMedicineName.Text = "";

                List<uspSelectMedicineByExpDateResult> Exp = new List<uspSelectMedicineByExpDateResult>();
                Exp = db_con.uspSelectMedicineByExpDate(DateTime.Parse(expDate)).ToList();


                if (cbManufacturer.SelectedIndex == -1 && cbBrandName.SelectedIndex == -1)
                {
                    foreach (var a in Exp)
                    {
                        lbMedicineResults.Items.Add(a.MedicineName);
                    }
                }

                if (cbManufacturer.SelectedIndex != -1 && cbBrandName.SelectedIndex == -1)
                {
                    foreach (var a in Exp)
                    {
                        if (a.MedManufacturer == cbManufacturer.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbManufacturer.SelectedIndex == -1 && cbBrandName.SelectedIndex != -1)
                {
                    foreach (var a in Exp)
                    {
                        if (a.MedicineType == cbBrandName.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbManufacturer.SelectedIndex != -1 && cbBrandName.SelectedIndex != -1)
                {
                    foreach (var a in Exp)
                    {
                        if (a.MedManufacturer == cbManufacturer.SelectedItem.ToString() && a.MedicineType == cbBrandName.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }
            }
        }

        private void cbManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e) // Search by Manufacturer
        {
            if (cbManufacturer.SelectedIndex != -1)
            {
                string manufacturer = cbManufacturer.SelectedItem.ToString();

                lbMedicineResults.Items.Clear();
                txtMedicineName.Text = "";

                List<uspSelectMedicineByManufacturerResult> Manufacturer = new List<uspSelectMedicineByManufacturerResult>();
                Manufacturer = db_con.uspSelectMedicineByManufacturer(manufacturer).ToList();


                if (cbExpDate.SelectedIndex == -1 && cbBrandName.SelectedIndex == -1)
                {
                    foreach (var a in Manufacturer)
                    {
                        lbMedicineResults.Items.Add(a.MedicineName);
                    }
                }

                if (cbExpDate.SelectedIndex != -1 && cbBrandName.SelectedIndex == -1)
                {
                    foreach (var a in Manufacturer)
                    {
                        if (a.MedExpDate.ToString() == cbExpDate.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbExpDate.SelectedIndex == -1 && cbBrandName.SelectedIndex != -1)
                {
                    foreach (var a in Manufacturer)
                    {
                        if (a.MedicineType == cbBrandName.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }

                if (cbExpDate.SelectedIndex != -1 && cbBrandName.SelectedIndex != -1)
                {
                    foreach (var a in Manufacturer)
                    {
                        if (a.MedExpDate.ToString() == cbExpDate.SelectedItem.ToString() && a.MedicineType == cbBrandName.SelectedItem.ToString())
                        {
                            lbMedicineResults.Items.Add(a.MedicineName);
                        }
                    }
                }
            }
        }
    }
}
