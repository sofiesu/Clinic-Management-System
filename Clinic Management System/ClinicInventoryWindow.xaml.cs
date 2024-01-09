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
    /// Interaction logic for ClinicInventoryWindow.xaml
    /// </summary>
    public partial class ClinicInventoryWindow : Window
    {
        public ClinicInventoryWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            // USP Search
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
            SpecificInventoryWindow specificInventoryWindow = new SpecificInventoryWindow();
            specificInventoryWindow.Show();
            this.Close();
        }
    }
}
