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
using Task = IntroSE.Kanban.Backend.Buissnes_Layer.Task;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class TasksView : Page
    {
        Dictionary<int, string> boards;
        TasksVM _tasksVM;
        private string colId;
        private List<string> _backlog;
        private List<string> _inProgress;
        private List<string> _done;
        private string _email;
        private string _boardName;
        public TasksView(string email, string boardName)
        {
            InitializeComponent();
            this._tasksVM = new TasksVM();
            this._backlog = _tasksVM.GetColumn(email, boardName, 0);
            this._inProgress = _tasksVM.GetColumn(email, boardName, 1);
            this._done = _tasksVM.GetColumn(email, boardName, 2);
            backlog1.ItemsSource = _backlog;
            inprogress1.ItemsSource = _inProgress;
            done1.ItemsSource = _done;
        }

        private void GetColumn(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void UserBoards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
