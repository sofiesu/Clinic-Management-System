using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window
    {
        string username = "";
        string password = "beut ekgz secg xasy";
        string senderEmail = "sfsuriaga@gmail.com";
        
        string template = "";

        public EmailWindow()
        {
            InitializeComponent();
            cbTemplate.Items.Add("Adviser Template");
            cbTemplate.Items.Add("Parent Template");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string recipient = txtRecipient.Text;
            SendEmail(username, password, senderEmail, recipient, txtSubject.Text, txtMessage.Text);
        }

        public void SendEmail(string username, string password, string senderEmail, string recipient, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, password),
                EnableSsl = true,
            };


            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recipient);

            try
            {
                smtpClient.Send(mailMessage);
                MessageBox.Show("Sent! Kindly check your Gmail...");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BackFunction(ConstantValues.type);
            this.Close();
        }

        private void cbTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtMessage.Text = "";
            txtRecipient.Text = "";
            txtSubject.Text = "";
            template = cbTemplate.SelectedItem.ToString();

            if(template == "Adviser Template")
            {
                txtSubject.Text = "Urgent: Student Early Dismissal Notification";
                txtMessage.Text = "Dear [Adviser Full Name],\r\nI hope this email finds you well. We would like to inform you of a situation concerning a student under your guidance." +
                    "\r\n\r\n[Student Full Name], [Patient Description], needs to be taken home immediately due to feeling unwell. We believe it is in the best interest of the " +
                    "student’s well-being to be in the care of their family at this time. Please excuse him/her from the remaining classes for the day." +
                    "\r\n\r\nBest Regards,\r\n[User Full name],\r\n[User Type],\r\n[User Contact Number],\r\nNorthville Public High School\r\n";
            }
            if (template == "Parent Template")
            {
                txtSubject.Text = "Urgent: Student Early Dismissal Notification";
                txtMessage.Text = "Dear [Parent/Guardian’s Name],\r\n\r\nI hope this email finds you well. We wish to inform you of a situation involving your child, " +
                    "[Student Full Name], in [Patient Description].\r\n\r\nThe student needs to be taken home immediately due to feeling unwell. " +
                    "The decision has been made in the interest of his/her well-being, and we kindly request your immediate attention to this matter. " +
                    "\r\n\r\nWe ask that you make the necessary arrangements to pick up your child as soon as possible. If you are unable to do so, please let us know at " +
                    "your earliest convenience, and we will work together to ensure your child’s safe return home." +
                    "\r\n\r\nBest Regards,\r\n[User Full name],\r\n[User Type],\r\n[User Contact Number],\r\nNorthville Public High School\r\n";
            }
        }
    }
}
