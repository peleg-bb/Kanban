using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Buissnes_Layer;

namespace BackendTests.ServiceLayer
{
    [TestClass()]
    public class UserTests
    {
        private UserController userController;
        public UserService userService;

        public UserTests(UserController UC, UserService US)
        {
            this.userController = UC;
            this.userService = US;
        }

        /// <summary>
        /// This method tests a valid creation of a new user in the system according to requirement 7.
        /// </summary>
        [TestMethod()]
        public void createUserTest()
        {
            Response response = new Response(null, true);
            Console.WriteLine(ToJson.toJson(response));
            Assert.AreEqual(userService.CreateUser("johndoe@gmail.com", "Hash123"), response.OKJson());
            
            Console.WriteLine("User created successfully!");
        }

        /// <summary>
        /// This method tests a valid login of an existing user in the system according to requirement 8.
        /// </summary>
        [TestMethod()]
        public void validUserLoginTest()
        {
            Response response = new Response((object)"johndoe@gmail.com");
            Console.WriteLine(ToJson.toJson(response));

            Assert.AreEqual(userService.Login("johndoe@gmail.com", "Hash123"), ToJson.toJson(response));
            Console.WriteLine("Login successful!");
        }

        /// <summary>
        /// This method tests an invalid login of a user due to a wrong password, according to requirement 1.
        /// </summary>
        public void invalidUserLoginTest()
        {
            Response response = new Response("Wrong password");
            Console.WriteLine(ToJson.toJson(response));
            Assert.AreEqual(userService.Login("johndoe@gmail.com", "wrong_password"), ToJson.toJson(response));
            Console.WriteLine("Login unsuccessful, user does not exist. Test passed!");
        }

        /// <summary>
        /// This method tests an invalid login of a user which doesn't exist, according to requirement 1.
        /// </summary>
        public void invalidLoginTest_2()
        {
            Response response = new Response("User does not exist", false);
            Console.WriteLine(response.BadJson());
            Assert.AreEqual(userService.Login("null@gmail.com", "wrong"), response.BadJson());
            Console.WriteLine("Login unsuccessful, user does not exist. Test passed!");
        }

        /// <summary>
        /// This method tests an invalid user creation - due to a short password(under 6 characters) according to requirement 2.
        /// </summary>
        public void invalidUserCreation()
        {
            Response response =
                new Response(
                    "Illegal password. A legal password must be 6-20 characters" +
                    " and must contain an Upper case, a lower case and a number", false);
            Console.WriteLine(response.BadJson());
            Assert.AreEqual(userService.CreateUser("tom@gmail.com", "1234"), response.BadJson());
            Console.WriteLine("User not created, short password");
        }

        /// <summary>
        /// This method tests an invalid user creation - due to an email which already exists - according to requirement 3.
        /// </summary>
        public void invalidUserCreation_2()
        {
            Response response = new Response("User already exists", false);
            Console.WriteLine(response.BadJson());
            Assert.AreEqual(userService.CreateUser("johndoe@gmail.com", "Ai9898"), response.BadJson());
            Console.WriteLine("User not created, email already exists");
        }

        /// <summary>
        /// This method tests an invalid user creation - due to an email which already exists - according to requirement 3.
        /// </summary>
        public void invalidUserCreation_3()
        {
            Response response = new Response("Not a valid email address", false);
            Console.WriteLine(response.BadJson());
            Assert.AreEqual(userService.CreateUser("_@gmailcom", "Ai9898"), response.BadJson());
            Console.WriteLine("User not created, email already exists");
        }


        /// <summary>
        /// This method tests the logout of a user- according to requirement 8.
        /// </summary>
        public void logoutTest()
        {
            Response response = new Response(null, true);
            Console.WriteLine(response.OKJson());
            Assert.AreEqual(userService.logout("johndoe@gmail.com"), response.OKJson());
            Console.WriteLine("User was logged out successfuly.");
        }

        /// <summary>
        /// This method tests an invalid logout - of a user that is already logged out - according to requirement 8.
        ///</summary>
        public void invalidLogoutTest()
        {
            Response response = new Response("User is already logged out", false);
            Console.WriteLine(response.BadJson());
            Assert.AreEqual(userService.logout("johndoe@gmail.com"), response.BadJson());
            Console.WriteLine("User was already logged out.");
        }

        public void LoadUsersTest()
        {
            userController.LoadUsers();
        }
    }
}
