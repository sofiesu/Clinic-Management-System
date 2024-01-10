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
        public ClinicVisits()
        {
            InitializeComponent();
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            EmailWindow emailWindow = new EmailWindow();
            emailWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }
    }
}
