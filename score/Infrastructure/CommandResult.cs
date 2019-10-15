using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Infrastructure
{

    /// <summary>
    /// Standart object for commands responses
    /// </summary>
    public class CommandResult
    {
        public string FailureReason;
        public dynamic Data;
        public bool IsSuccess => string.IsNullOrWhiteSpace(FailureReason);

        private CommandResult()
        {
        }

        private CommandResult(string failureReason)
        {
            this.FailureReason = failureReason;
        }

        public static CommandResult Success() => new CommandResult();

        public static CommandResult Success(dynamic data)
        {
            return new CommandResult
            {
                Data = data
            };
        }

        public static CommandResult Failure(string message) => new CommandResult(message);

        public static CommandResult Failure(Exception ex)
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
            return new CommandResult { FailureReason = message };
        }
      

    }
}
