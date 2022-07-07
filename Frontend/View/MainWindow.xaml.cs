﻿using System;
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
        [System.ComponentModel.Bindable(true)]
        [System.ComponentModel.SettingsBindable(true)]
        public bool Checked { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            userVM = new UserVM();
            ResizeMode = ResizeMode.NoResize;
        }

        public void UserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            _email = email.Text;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Checked = Convert.ToBoolean(CheckBox1.IsChecked.Value);
                this._email = email.Text;
                this._password = Password.Password.ToString();
                if (userVM.Register(_email, _password) && Checked)
                {
                    MessageBox.Show("You registered successfully!");
                    Login_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Can't register, illegal input");
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
                this._password = Password.Password;

                if (userVM.Login(_email, _password))
                {
                    MessageBox.Show("Login successful");
                    BoardsView boards = new BoardsView(_email);
                    Main.Content = boards;
                    
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
