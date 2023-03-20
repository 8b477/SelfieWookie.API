#region Comment
// Classe ici static qui me permet de faire les injections de dépendance comme Microsoft le conseil,
// => cad via une méthodes d'extensions 
// => Ici j'étend donc l'interface < IServiceCollection > grace au mot clée < this >. 
#endregion

using SelfieWookie.Core.Domain;
using SelfieWookie.Core.Infrastructure.Data.Repository;
using MediatR;
using System.Reflection;

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

            // Plus d'info sur cette ligne de code ci dessous de la classe.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
/*  A propos de * AddMediatR *
    ------------------------

    Cette ligne de code est utilisée dans le cadre de la configuration des services
    dans une application C#utilisant le pattern de conception de médiateur.

    Plus spécifiquement, elle ajoute le package MediatR au conteneur d'injection
    de dépendances de l'application en utilisant la méthode d'extension AddMediatR
    fournie par ce package. Cette méthode prend en paramètre une expression lambda
    cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) qui
    permet de configurer les services associés au médiateur.

    La méthode RegisterServicesFromAssembly utilisée ici est également fournie
    par MediatR et permet d'enregistrer tous les types de services impliqués
    dans la communication entre les différentes classes du médiateur à partir
    de l'assembly courant (ici, Assembly.GetExecutingAssembly()).

    En résumé, cette ligne de code permet de configurer les services nécessaires
    pour utiliser le pattern de conception de médiateur dans une application C#
    et de les enregistrer dans le conteneur d'injection de dépendances.
 */