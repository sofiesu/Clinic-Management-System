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
        public SpecificPatientWindow()
        {
            InitializeComponent();

            Clear();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void Clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtAdviserEmailAddress.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtContactNum.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtEAddress.Text = string.Empty;
            txtEEmailAddress.Text = string.Empty;
            txtEName.Text = string.Empty;
            txtEContactNumber.Text = string.Empty;
            txtType.Text = string.Empty;
        }
    }
}
