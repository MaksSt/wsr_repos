using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using Login.Models;

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
            using (LoginDbContext db = new LoginDbContext())
            {
                // получаем объекты из бд и выводим на консоль
                object? user = db.Elemens.ToList().Where(u => txtUSER.Text == u.Username && txtPASSWORD.Password == u.Password).LastOrDefault();
                if (user is Elemen)
                {
                    this.blockCode.Visibility = Visibility.Visible;
                    this.txtCode.Visibility = Visibility.Visible;
                    MessageBox.Show($"Code = {code}");
                    this.txtUSER.IsEnabled = false;
                    this.txtPASSWORD.IsEnabled = false;
                    btnLogin.Click -= btnLogin_Click;
                    btnLogin.Click += btnLogin2_Click;
                }
                else
                {
                    MessageBox.Show("Bad username or password");
                }
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
                    this.Close();
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
