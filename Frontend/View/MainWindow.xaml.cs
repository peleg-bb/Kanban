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
using Frontend.ModelView;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserVM userVM;
        private string _email;
        private string _password;
        public MainWindow()
        {
            InitializeComponent();
            userVM = new UserVM();
        }

        public void UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            _email = e.ToString();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this._email = email.Text;
                this._password = Password.Password.ToString();
                if (userVM.Register(_email, _password))
                {
                    userVM.Login(_email, _password);
                }
                else
                {
                    MessageBox.Show("Something Went wrong ;(");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this._email = email.Text;
                this._password = Password.Password.ToString();

                if (userVM.Login(_email, _password))
                {
                    BoardsView boards = new BoardsView(_email);
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
