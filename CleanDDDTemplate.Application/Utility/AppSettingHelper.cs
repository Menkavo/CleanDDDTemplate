using Microsoft.Extensions.Configuration;

namespace CleanDDDTemplate.Application.Utility
{
    public static class AppSettingHelper
    {
        public static string GetCleanDDDTemplateConnectionString(IConfiguration configuration)
        {
#if DEBUG
            return GetConnectionString(configuration, "CleanDDDTemplateConnectionStringDevelopment");
#else
            var encrypted = GetConnectionString(configuration, "CleanDDDTemplateConnectionString");
            return DecrypteConnectionString(encrypted);
#endif
        }

        private static string DecrypteConnectionString(string encryptedConnectionString)
        {
            //TODO
            //Encrypt your databases connection strings and decrypt it using this method to insure security.
            return encryptedConnectionString;
        }

        private static string GetConnectionString(IConfiguration configuration, string connectionName) => configuration.GetConnectionString(connectionName);
    }
}