namespace KnowCloud.Utility
{
    public static class ConfigExtensions
    {
        public static void ConfigureUtilities(this IServiceCollection services, IConfiguration configuration) 
        {
            Utilities.AuthAPIBase = configuration["ServiceUrls:AuthAPI"];
        }
    }
}
