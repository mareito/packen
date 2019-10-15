using Microsoft.EntityFrameworkCore;
using score.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Data
{
    /// <summary>
    /// Class for Entity Framework configuration
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) :base(options)
        {
        }
       
        public DbSet<User> users { get; set; }
        public DbSet<UserTokens> UserTokens { get; set; }
        public DbSet<UserGame> userGames { get; set; }
     }
}
