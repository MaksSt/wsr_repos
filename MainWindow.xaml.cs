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
                        //Panel objPanel = new Panel();
                        //this.Hide();
                        //objPanel.Show();
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
    }
}
