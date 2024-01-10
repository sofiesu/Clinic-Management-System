using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainMenu2.xaml
    /// </summary>
    public partial class MainMenu2 : Window
    {
        ConstantValues ConstantValues = new ConstantValues();
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        public MainMenu2()
        {
            InitializeComponent();
            AdminFN.Content = ConstantValues.FullName;
        }

        private void btnClinicOverview_Click(object sender, RoutedEventArgs e)
        {
            ClinicInventoryWindow clinicInventoryWindow = new ClinicInventoryWindow();
            clinicInventoryWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db_con.uspInsertLogs(ConstantValues.UID, "Logged Out");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
