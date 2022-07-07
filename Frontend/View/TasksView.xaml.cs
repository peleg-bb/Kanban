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
        private List<Task> _backlog;
        private List<Task> _inProgress;
        private List<Task> _done;
        private string email;
        public TasksView(int id,string boardName, string email)
        {
            InitializeComponent();
            this._tasksVM = new TasksVM();
            this._backlog = _tasksVM.GetColumn(email, boardName, 0);
            this._inProgress = _tasksVM.GetColumn(email, boardName, 1);
            this._done = _tasksVM.GetColumn(email, boardName, 2);
            DataContext = this._backlog;
            DataContext = this._inProgress;
            DataContext = this._done;
        }

        // public void GetColumn(object sender, RoutedEventArgs e)
        // {
        //     
        // }

        private void GetColumn(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
