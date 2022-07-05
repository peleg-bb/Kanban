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
        private string email;
        Dictionary<int, string> boards;
        public BoardsView(string userEmail)
        {
            InitializeComponent();
            this._boardsVM = new BoardsVM(email);
            this.boards = _boardsVM.GetBoards(email);
        }


        public void Boards(object sender, RoutedEventArgs e, string email)
        {
            _boardsVM.GetBoards(email);
        }


    }
}
