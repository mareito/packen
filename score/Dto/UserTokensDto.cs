using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace score.Dto
{   
    /// <summary>
    /// Dto for users tokens
    /// </summary>
    public class UserTokensDto
    {
 
        public int Id { get; set; }
        public int IdUser { get; set; }
        public byte[] token { get; set; }
    }
}
