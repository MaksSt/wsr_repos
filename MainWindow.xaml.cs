using System;
using System.Windows;
using System.Windows.Input;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=c:\Users\student\Desktop\login_wsr\db\LoginDB.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True";
            SqlConnection sqlcon = new SqlConnection(connectionString);
            string query = "Select * from tbl_login Where username = '" + txtUSER.Text.Trim() + "' and password = '" + txtPASSWORD.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if(dtbl.Rows.Count == 1)
            {
                Panel objPanel = new Panel();
                this.Hide();
                objPanel.Show();
            }
            else
            {
                MessageBox.Show("Bad username or password");
            }
        }
    }
}
