using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Dto
{
    /// <summary>
    /// Dto for users games
    /// </summary>
    public class UserGameDto
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Sequence { get; set; }
        public int FinalScore { get; set; }
    }
}
