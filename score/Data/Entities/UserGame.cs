using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace score.Data.Entities
{
    /// <summary>
    /// Table for user games
    /// </summary>
    [Table("user_games")]
    public class UserGame
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_user")]
        public int IdUser { get; set; }

        [Column("sequence")]
        public string Sequence { get; set; }

        [Column("final_score")]
        public int FinalScore { get; set; }
    }
}
