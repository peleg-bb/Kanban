using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
    
        private UserVM userVM;
        private string _email;
        private string _password;
        public MainWindow()
        {
            InitializeComponent();
            userVM = new UserVM();
            this.DataContext = userVM;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            _email = e.ToString();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userVM.Login(email.ToString(), Password.ToString()))
                {
                    BoardsView boards = new BoardsView(email.ToString());
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userVM.Register(email.ToString(), Password.ToString());
                userVM.Login(email.ToString(), Password.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
