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
        TasksVM _tasksView;
        private string colId;
        private List<string> colList0;
        private List<string> colList1;
        private List<string> colList2;
        private string email;
        public TasksView(int Id,string BoardName,string email)
        {
            InitializeComponent();
            this.colId = colId;
            this.email = email;
            this.colList0 = new List<string>();
            this.colList1 = new List<string>();
            this.colList2 = new List<string>();
            this.colList0 = _tasksView.GetCol(0, email);
            this.colList1 = _tasksView.GetCol(1, email);
            this.colList2 = _tasksView.GetCol(2, email);
            this._tasksView = new TasksVM();
            DataContext = this.colList0;
            DataContext = this.colList1;
            DataContext = this.colList2;
            // this._boardsVM = new BoardsVM(email);
            // this.boards = _boardsVM.GetBoards(email);
        }

        public void GetCoumn(object sender, RoutedEventArgs e)
        {
            
        }

        private void GetCoumn(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
