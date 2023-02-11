using System;
using System.Windows;
using System.Windows.Input;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
        static int RandomValue()
        {
            Random rnd = new Random();
            return rnd.Next(0, 100000);
        }
        public int code = RandomValue();
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                          Integrated Security=True;
                          Initial Catalog=LoginDB";
            SqlConnection sqlcon = new SqlConnection(connectionString);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                    string query = "Select * from Login Where username=@Username AND password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", txtUSER.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", txtPASSWORD.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        this.blockCode.Visibility = System.Windows.Visibility.Visible;
                        this.txtCode.Visibility = System.Windows.Visibility.Visible;
                        MessageBox.Show($"Code = {code}");
                        this.btnLogin.Visibility = System.Windows.Visibility.Collapsed;
                        this.btnLogin2.Visibility = System.Windows.Visibility.Visible;
                        this.txtUSER.IsEnabled = false;
                        this.txtPASSWORD.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Bad username or password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        private void btnLogin2_Click(object sender, RoutedEventArgs e)
        {
            if (txtCode != null && !string.IsNullOrWhiteSpace(txtCode.Text))
            {
                int theCode = Convert.ToInt32(txtCode.Text);
                if (code == theCode)
                {
                    Panel objPanel = new Panel();
                    this.Hide();
                    objPanel.Show();
                }
                else
                    MessageBox.Show($"Code = {code}");
            }
            else
                MessageBox.Show($"Code = {code}");
        }
    }
}
