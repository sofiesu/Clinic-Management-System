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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClinicDatabaseDataContext db_con = ConstantValues.DBConnectionString;

        int UID = 0;
        string UType = "";
        string UName = "";
        string UPass = "";
        string UFN = "";
        string UEmail = "";
        string UNum = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtEmailAddress.Text.Length > 0 && txtPassword.Password.Length > 0))
            {
                MessageBox.Show("Please fill out the following details."
                    , "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                if (Users(txtEmailAddress.Text, txtPassword.Password) == true)
                {
                    //USP for Log In
                    db_con.uspLogin(txtEmailAddress.Text, txtPassword.Password);

                    MessageBox.Show("You have successfully logged in."
                        , "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);

                    if(UType != "Admin")
                    {
                        ConstantValues.type = "MedPro";
                        db_con.uspInsertLogs(UID, "A medical professional has logged in");
                        MedProHomepage();
                        this.Close();
                    }
                    else
                    {
                        ConstantValues.type = "Admin";
                        db_con.uspInsertLogs(UID, "An admin has logged in");
                        AdminHomepage();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("User ID/Pass is incorrect");
                    txtPassword.Password = "";
                }
            }
        }

        private void MedProHomepage()
        {
            MainMenu1 mainMenu1 = new MainMenu1();
            mainMenu1.Show();
        }

        private void AdminHomepage()
        {
            MainMenu2 mainMenu2 = new MainMenu2();
            mainMenu2.Show();
        }

        public void BackFunction(string type)
        {
            if(type != "Admin")
            {
                MedProHomepage();
            }
            else
            {
                AdminHomepage();
            }
        }

        private bool Users(string UserEmail, string UserPass)
        {
            bool response = false;
            int index = 0;
            List<uspSelectAllUserResult> UserResults = new List<uspSelectAllUserResult>();
            UserResults = db_con.uspSelectAllUser().ToList();
            for (int x = 0; x < UserResults.Count; x++)
            {
                if (UserResults[x].UserEmail == UserEmail)
                {
                    index = x;

                    if (UserResults[index].UserPassword == UserPass)
                    {
                        response = true;
                        UID = UserResults[index].UserID;
                        UType = UserResults[index].UserType;
                        UName = UserResults[index].UserName;
                        UPass = UserResults[index].UserPassword;
                        UFN = UserResults[index].UserFullName;
                        ConstantValues.FullName = UFN;
                        ConstantValues.UID = UID;
                        UEmail = UserResults[index].UserEmail;
                        UNum = UserResults[index].UserContact;
                    }
                }
            }
            return response;
        }
    }
}
