using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace score.Dto
{   
    /// <summary>
    /// DTO for reading users
    /// </summary>
    public class UserDto
    {       
        public int Id { get; set; }      
        public string Name { get; set; }       
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }    
    }
}
