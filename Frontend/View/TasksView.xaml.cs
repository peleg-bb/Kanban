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
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class TasksView : Page
    {
        Dictionary<int, string> boards;
        BoardsVM _boardsVM;
        public TasksView(string email)
        {
            InitializeComponent();
            this._boardsVM = new BoardsVM(email);
            this.boards = _boardsVM.GetBoards(email);
        }

        

        
    }
}
