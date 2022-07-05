using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Newtonsoft.Json;

namespace Frontend.Model
{
    internal class UserModel
    {
        private ServiceFactory serviceFactory;
        private UserService userService;

        public UserModel()
        {
            this.serviceFactory = ServiceFactory.getServiceFactrory();
            this.userService = serviceFactory.userService;
        }

        //login
        public string Login(string username, string password)
        {
            string response = userService.Login(username, password);
            return JsonConvert.DeserializeObject<string>(response);
        }

        public void Logout(string username)
        {
            userService.logout(username);
        }

        public void Register(string Username, string Password)
        {
            userService.CreateUser(Username, Password);
        }
    }
}
