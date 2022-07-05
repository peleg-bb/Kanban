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
        private List<Task> colList;
        private string email;
        public TasksView(int Id,string BoardName,string email)
        {
            InitializeComponent();
            this.colId = colId;
            this.email = email;
            this.colList = new List<Task>();
            //this.colList = _tasksView.GetCol(Int32.Parse(colId.ToString()), email);
            this._tasksView = new TasksVM();
            //
            // this._boardsVM = new BoardsVM(email);
            // this.boards = _boardsVM.GetBoards(email);
        }

        public void GetCoumn(object sender, RoutedEventArgs e)
        {
            
        }



    }
}
