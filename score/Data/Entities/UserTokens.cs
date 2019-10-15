using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace score.Data.Entities
{
    /// <summary>
    /// Table for management of the user tokens
    /// </summary>
    [Table("user_tokens")]
    public class UserTokens
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("token")]
        public byte[] token { get; set; }
    }
}
