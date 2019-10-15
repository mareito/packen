using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Infrastructure
{
    /// <summary>
    /// Standart object for query responses
    /// </summary>
    public class QueryResult
    {
        public string FailureReason;
        public IEnumerable Data;
        public bool IsSuccess => string.IsNullOrWhiteSpace(FailureReason);

        private QueryResult()
        {
        }

        private QueryResult(string failureReason)
        {
            this.FailureReason = failureReason;
        }

        public static QueryResult Success() => new QueryResult();

        public static QueryResult Success(IEnumerable data)
        {
            return new QueryResult
            {
                Data = data
            };
        }

        public static QueryResult Failure(string message) => new QueryResult(message);

        public static QueryResult Failure(Exception ex)
        {
            var message = "";
            if (ex.InnerException != null)
            {
                message = ex.InnerException.Message;
            }
            else
            {
                message = ex.Message;
            }
            Log.Logger.Error($"Error Generated : {message} ");
            return new QueryResult { FailureReason = message };
        }
      

    }
}
