using System.Collections.Generic;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Frontend.Model;

public class TasksModel
{
    private BoardService boardService;
    private ServiceFactory serviceFactory;
    private string _email;
    private string _boardName;
    public TasksModel(string email, string boardName)
    {
        this.serviceFactory = ServiceFactory.getServiceFactrory();
        this.boardService = serviceFactory.boardService;
        this._email = email;
        this._boardName = boardName;
    }

    public List<Task> GetColumn(string email, string boardName, int columnOrdinal)
    {
        return boardService.GetColumn(email, boardName, columnOrdinal);
    }
}