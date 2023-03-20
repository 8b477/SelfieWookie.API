using SelfieWookie.Core.Infrastructure.Configurations;

namespace SelfieWookie.API.UI.ExtensionMethods
{
    public static class OptionsMethods
    {
        /// <summary>
        /// Permet d'accéder à ma clef secret.json via injection de dépendance.
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }

        // Plus d'info sur la classe :
        // Ici je crée donc une classe pour géré l
    }
}