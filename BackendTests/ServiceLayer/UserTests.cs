using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace BackendTests.ServiceLayer
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
            user1.createUser("johndoe@gmail.com", "123456");
        }

        /// <summary>
        /// This method tests a valid login of an existing user in the system according to requirement 8.
        /// </summary>
        public void validUserLoginTest()
        {
            user1.login("johndoe@gmail.com", "123456");
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
        /// This method tests an invalid user creation - due to a short password (under 6 characters) according to requirement 2.
        /// </summary>
        public void invalidUserCreation()
        {
            user1.createUser("none@gmail.com", "_");
        }

        /// <summary>
        /// This method tests an invalid user creation - due to an email which already exists - according to requirement 3.
        /// </summary>
        public void invalidUserCreation_2()
        {
            user1.createUser("johndoe@gmail.com", "123456");
        }

        /// <summary>
        /// This method tests the creation of a board- according to requirement 9.
        /// </summary>
        public void createBoardTest()
        {
            user1.newBoard("To do list");
        }


        /// <summary>
        /// This method tests the creation of a board using a name which already exists- according to requirement 6.
        /// </summary>
        public void invalidCreateBoardTest()
        {
            user1.newBoard("To do list");
        }

        /// <summary>
        /// This method tests the logout of a user- according to requirement 8.
        /// </summary>
        public void logoutTest()
        {
            user1.logout("johndoe@gmail.com");
        }


        /// <summary>
        /// This method tests an invalid logout - of a user that is already logged out - according to requirement 8.
        /// </summary>
        public void invalidLogoutTest()
        {
            user1.logout("johndoe@gmail.com");
        }

        /// <summary>
        /// This method tests the display of In Progress tasks- according to requirement 16.
        /// </summary>
        public void getInProgressTest()
        {
            user1.getInProgress("johndoe@gmail.com");

        }
    }
}