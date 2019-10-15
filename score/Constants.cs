using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score
{
    /// <summary>
    /// Initialize constants for the application
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Constants for swagger
        /// </summary>
        public static class SwaggerConfiguration
        {
            /// <summary>
            /// Title for swagger documentation endpoint
            /// </summary>
            public const string Title = "Api - Bowling Game Score";

            /// <summary>
            /// Description 
            /// </summary>
            public const string Description = "Calculate final score for a bowling game";

            /// <summary>
            /// Contact Name
            /// </summary>
            public const string ContactName = "Mario Ramos Machado";

            /// <summary>
            /// Contact Email
            /// </summary>
            public const string ContactEmail = "m.ramos257@gmail.com";

            /// <summary>
            /// Contact URL
            /// </summary>
            public const string ContactUrl = "https://www.linkedin.com/in/marioramosmachado/";


        }


        /// <summary>
        /// Configuration values for the application
        /// </summary>
        public static class AppConfiguration
        {
            /// <summary>
            /// Array with valid characters for input string
            /// </summary>
            public static readonly string[] ValidValues = new string[12] { "-", "1", "2", "3", "4", "5", "6", "7", "8", "9", "X", "/" };

            /// <summary>
            /// Static function that returns numeric values
            /// </summary>
            /// <param name="character">String that represents an attempt or a ball in the game</param>
            /// <returns>Numeric value</returns>
            public static int getCharacterValue(string character)
            {

                var value = 0;

                switch (character)
                {
                    case "-":
                        value = 0;
                        break;

                    case "1":
                        value = 1;
                        break;

                    case "2":
                        value = 2;
                        break;

                    case "3":
                        value = 3;
                        break;

                    case "4":
                        value = 4;
                        break;

                    case "5":
                        value = 5;
                        break;

                    case "6":
                        value = 6;
                        break;

                    case "7":
                        value = 7;
                        break;

                    case "8":
                        value = 8;
                        break;

                    case "9":
                        value = 9;
                        break;

                    case "X":
                        value = 10;
                        break;

                    case "/":
                        value = 10;
                        break;

                    default:
                        value = 0;
                        break;
                }

                return value;
            }

        }

        public static class JwtConfiguration
        {
            public const string IssuerSigningKey = "JwtConfiguration:IssuerSigningKey";
            public const string ValidIssuer = "JwtConfiguration:ValidIssuer";
            public const string ValidAudience = "JwtConfiguration:ValidAudience";

        }

        public static class CustomClaimsNames
        {
            public const string Id = "Id";
            public const string Email = "Email";
            public const string Name = "Name";            
        }
    }
}
