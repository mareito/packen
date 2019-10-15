using score.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Services.SecurityService
{
    /// <summary>
    /// Interface that defines methods for security service
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Creates default user for the application (user:test, password: test)
        /// </summary>
        void CreateDefaultUser();

        /// <summary>
        /// Verifies if the password is valid
        /// </summary>
        /// <param name="clave">Password</param>
        /// <param name="claveBd">Array of bytes with the encripted password</param>
        /// <param name="salt">Salt for password encription</param>
        /// <returns>True or false if password is valid or not</returns>
        bool VerifiedPassword(string clave, byte[] claveBd, byte[] salt);

        /// <summary>
        /// Generates a neu token for the user
        /// </summary>
        /// <param name="userDto">Object with user information</param>
        /// <returns>Valid token</returns>
        string getToken(UserDto userDto);

        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True or false</returns>
        bool ValidToken(string token);

        /// <summary>
        /// Return value of claims
        /// </summary>
        /// <param name="token"></param>
        /// <param name="claim"></param>
        /// <param name="value"></param>
        void getClaim(string token, string claim, out string value);
    }
}
