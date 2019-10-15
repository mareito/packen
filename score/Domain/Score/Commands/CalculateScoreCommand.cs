using MediatR;
using score.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace score.Domain.Score.Commands
{
    /// <summary>
    /// Class with necessary field for calculate game score
    /// </summary>
    public class CalculateScoreCommand :IRequest<CommandResult>
    {

        public CalculateScoreCommand(string sequence)
        {
            this.RollsSequence = sequence;
        }

       [Required]        
       public string RollsSequence { get; set; }
    }
}
