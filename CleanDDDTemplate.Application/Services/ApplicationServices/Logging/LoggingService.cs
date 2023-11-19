using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Exceptions;
using System.Net.Http;

namespace CleanDDDTemplate.Application.Services.ApplicationServices.Logging
{
    public class LoggingService : ILoggingService
    {
        private static readonly string _errorLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Error\ErrorLog-.Log";
        private static readonly string _warningLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Warning\WarningLog-.Log";
        private static readonly string _exceptionLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Exception\ExceptionLog-.Log";
        private static readonly string _informationLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Information\InformationLog-.Log";
        private static readonly string _unauthorizedRequestLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Unauthorized\UnauthorizedRequests-.Log";

        private readonly ILogger _errorLogger;
        private readonly ILogger _warningLogger;
        private readonly ILogger _exceptionLogger;
        private readonly ILogger _informationLogger;
        private readonly ILogger _unauthorizedRequestsLogger;

        public LoggingService()
        {
            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(_errorLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetErrorTemplate())
                .CreateLogger();

            _warningLogger = new LoggerConfiguration()
                .WriteTo.File(_warningLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetWarningTemplate())
                .CreateLogger();

            _exceptionLogger = new LoggerConfiguration()
                .WriteTo.File(_exceptionLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetExceptionTemplate()).Enrich.WithExceptionDetails()
                .CreateLogger();

            _informationLogger = new LoggerConfiguration()
                .WriteTo.File(_informationLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetInformationTemplate())
                .CreateLogger();

            _unauthorizedRequestsLogger = new LoggerConfiguration()
                .WriteTo.File(_unauthorizedRequestLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetUnauthorizedRequestTemplate())
                .CreateLogger();
        }

        public void LogError(string errorMessage)
        {
            var logMessage = "An error has occurred.\n" +
                "Error details: {0}";
            _errorLogger.Error(logMessage, errorMessage);
        }

        public void LogWarning(string message) => _warningLogger.Warning(message);

        public void LogException(Exception ex)
        {
            var logMessage = "An exception was thrown.\n" +
                "Exeption details:";
            _exceptionLogger.Error(ex, logMessage);
        }

        public void LogInformation(string message) => _informationLogger.Information(message);

        public void LogUnauthorizedRequest(HttpContext httpContext)
        {
            var message = "Unauthorized request submited.\n" +
                "Client ip address: {0}";

            _unauthorizedRequestsLogger.Error(message, httpContext.Connection.RemoteIpAddress);
        }


        private static string GetLoggingBaseTemplate()
        {
            return "Date: {Timestamp:yyyy/MM/dd}{NewLine}" +
                "Time: {Timestamp:HH:mm:ss}{NewLine}" +
                "[{Level}]{NewLine}" +
                "{Message}{NewLine}" +
                "**************************************************************{NewLine}{NewLine}";
        }

        //TODO
        private static string GetErrorTemplate() => GetLoggingBaseTemplate();

        //TODO
        private static string GetWarningTemplate() => GetLoggingBaseTemplate();

        //TODO
        private static string GetExceptionTemplate() => GetLoggingBaseTemplate();

        //TODO
        private static string GetInformationTemplate() => GetLoggingBaseTemplate();

        //TODO
        private static string GetUnauthorizedRequestTemplate() => GetLoggingBaseTemplate();
    }
}