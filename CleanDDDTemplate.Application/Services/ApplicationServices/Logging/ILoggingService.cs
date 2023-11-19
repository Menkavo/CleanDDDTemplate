using Microsoft.AspNetCore.Http;

namespace CleanDDDTemplate.Application.Services.ApplicationServices.Logging
{
    public interface ILoggingService
    {
        public void LogError(string message);

        public void LogWarning(string message);

        public void LogException(Exception ex);

        public void LogInformation(string message);

        public void LogUnauthorizedRequest(HttpContext httpContext);
    }
}