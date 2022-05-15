using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// A class for calling the User class in the BussinessLayer.
    /// <para>
    /// Each of the class' methods should return a JSON string with the following structure (see <see cref="System.Text.Json"/>):
    /// <code>
    /// {
    ///     "ErrorMessage": &lt;string&gt;,
    ///     "ReturnValue": &lt;object&gt;
    /// }
    /// </code>
    /// Where:
    /// <list type="bullet">
    ///     <item>
    ///         <term>ReturnValue</term>
    ///         <description>
    ///             The return value of the function.
    ///             <para>
    ///                 The value may be either a <paramref name="primitive"/>, a <paramref name="Task"/>, or an array of of them. See below for the definition of <paramref name="Task"/>.
    ///             </para>
    ///             <para>If the function does not return a value or an exception has occorred, then the field is undefined.</para>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>ErrorMessage</term>
    ///         <description>If an exception has occurred, then this field will contain a string of the error message.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// The structure of the JSON of a Task, is:
    /// <code>
    /// {
    ///     "Id": &lt;int&gt;,
    ///     "CreationTime": &lt;DateTime&gt;,
    ///     "Title": &lt;string&gt;,
    ///     "Description": &lt;string&gt;,
    ///     "DueDate": &lt;DateTime&gt;
    /// }
    /// </code>
    /// </para>
    /// </summary>
    public class User
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>Response with a createUser task, unless user already exists.</returns>
        public string createUser(string email, string password)

        {
            log.Info("User created!");
            throw new NotImplementedException();

        }

        /// <summary>
        ///  This method changes the password of a given user.
        /// </summary>
        /// <param name="username">The email address of the user.</param>
        /// <param name="oldP">The old password of the user. Must match with existing password in database.</param>
        /// <param name="newP">The new password of the user. Must match with password rules.</param>
        /// <returns> Response with changePassword task, unless an error occurs.</returns>
        public string changePassword(string username, string oldP, string newP)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="username">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns> Response with user email, unless an error occurs</returns>
        public string login(string username, string password)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// This method creates a new board for the user it is called from.
        /// </summary>
        /// <param name="boardName">Name of the board. Must be unique (ie. a user cannot have 2 boards with the same name).</param>
        /// <returns>Response with a command to create board, unless a board exists with the same name.</returns>
        public string newBoard(string boardName)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="username">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs</returns>
        public string getInProgress(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="username">The email of the user to log out</param>
        /// <returns>The string "{}", unless an error occurs </returns>
        public string logout(string username)
        {
            throw new NotImplementedException();
        }

    }
}
