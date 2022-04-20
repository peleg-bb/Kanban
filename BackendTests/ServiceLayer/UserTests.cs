using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend;

namespace BackendTest
{
    [TestClass()]
    public class UserTests
    {
        User user1 = new User();

        /// <summary>
        /// This method tests a valid creation of a new user in the system according to requirement 7.
        /// </summary>
        [TestMethod()]
        public void createUserTest()
        {
            user1.createUser("johndoe@gmail.com", "12345");
        }

        /// <summary>
        /// This method tests a valid login of an existing user in the system according to requirement 8.
        /// </summary>
        public void validUserLoginTest()
        {
            user1.login("johndoe@gmail.com", "12345");
        }

        /// <summary>
        /// This method tests an invalid login of a user due to a wrong password, according to requirement 1.
        /// </summary>
        public void invalidUserLoginTest()
        {
            user1.login("johndoe@gmail.com", "wrong_password");
        }

        /// <summary>
        /// This method tests an invalid login of a user which doesn't exist, according to requirement 1.
        /// </summary>
        public void invalidLoginTest_2()
        {
            user1.login("none@gmail.com", "wrong_password");
        }

        /// <summary>
        /// This method tests an invalid login of a user which doesn't exist, according to requirement 1.
        /// </summary>
        public void invalidUserCreation()
        {
            user1.createUser("none@gmail.com", "short");
        }










    }
}