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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Frontend.ModelView;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Boards.xaml
    /// </summary>
    public partial class BoardsView : Page
    {
        private BoardsVM _boardsVM;
        private string _email;
        private string selectedBoardName;

        private Dictionary<int, string> _boardsDictionary;
        // {
        //     set => this._boardsDictionary = _boardsVM.GetBoards(_email);
        //     get => this._boardsDictionary;
        // }

        public BoardsView(string userEmail)
        {
            
            this._email = userEmail;
            this._boardsVM = new BoardsVM(_email);
            this._boardsDictionary = new Dictionary<int, string>();
            this._boardsDictionary = _boardsVM.GetBoards(_email);
            this.DataContext = this._boardsDictionary;
            InitializeComponent();

        }
        // public void Boards(object sender, RoutedEventArgs e, string email)
        // {
        //     _boardsVM.GetBoards(email);
        // }
        private void Search_Board(object sender, RoutedEventArgs e)
        {
            try
            {
                TasksView tx = new TasksView(_email, selectedBoardName);
                // Note that the board is searched using board name and email only!
                // I deem the ID box is redundant
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        // What is this nonsense of a name?!
        // }

        private void BoardNameT_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.selectedBoardName = BoardNameText.Text;
        }
    }
}
