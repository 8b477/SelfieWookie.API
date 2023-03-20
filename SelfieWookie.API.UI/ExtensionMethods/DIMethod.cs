#region Comment
// Classe ici static qui me permet de faire les injections de dépendance comme Microsoft le conseil,
// => cad via une méthodes d'extensions 
// => Ici j'étend donc l'interface < IServiceCollection > grace au mot clée < this >. 
#endregion

using SelfieWookie.Core.Domain;
using SelfieWookie.Core.Infrastructure.Data.Repository;

namespace SelfieWookie.API.UI.ExtensionMethods
{
    /// <summary>
    /// DI => dependency injection
    /// </summary>

    public static class DIMethod
    {

        /// <summary>
        /// Group and prepare customs dependency injection
        /// </summary>
        /// <param name="services"></param>
        /// 

        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
        }
    }
}
