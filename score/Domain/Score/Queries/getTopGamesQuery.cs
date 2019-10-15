using MediatR;
using score.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Domain.Score.Queries
{
    /// <summary>
    /// Number or rows returned by the web service
    /// </summary>
    public class GetTopGamesQuery : IRequest<QueryResult>
    {
        public int NumRows { get; set; } = 5;
    }
}
