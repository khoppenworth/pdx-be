using System.Threading.Tasks;
using PDX.BLL.Model;

namespace PDX.BLL.Services.Interfaces
{
    public interface IAccountService
    {      
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        Task<bool> AuthenticateUserAsync(string username, string password);

        /// <summary>
        /// Change exiting password of a user by a new one
        /// </summary>
        /// <param name="userID"></param>
         Task<bool> ChangePasswordAsync(int userID, string newPassword);

        /// <summary>
        /// Change exiting password of a user by a new one
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPassword"></param>
         Task<bool> ChangePasswordAsync(int userID, string oldPassword,string newPassword);

        /// <summary>
        /// Reset exiting password 
        /// </summary>
        /// <param name="username"></param>
         Task<string> ResetPasswordAsync(string username);
    }
}