using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SelfieWookie.Core.Infrastructure.DB
{
    // Implémente l'interface IDesignTimeDbContextFactory<SelfieContext> pour fournir un contexte de base de données au design time.
    public class SelfieContextFactory : IDesignTimeDbContextFactory<SelfieContext>
    {
        // Crée un contexte de base de données à l'aide des paramètres spécifiés.
        public SelfieContext CreateDbContext(string[] args)
        {
            // Ajoute un ConfigurationBuilder pour accéder aux paramètres de configuration de l'application.
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            // Ajoute un fichier de configuration appsettings.json pour les paramètres de connexion à la base de données.
            configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),"Settings", "appsettings.json"));

            // Construit la configuration de l'application.
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            // Crée un DbContextOptionsBuilder et configure le fournisseur de base de données à utiliser
            // (SQL Server) en utilisant la chaîne de connexion définie dans appsettings.json.
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(configurationRoot.GetConnectionString("DBSelfieWookie"));

            // Créer un nouveau contexte de base de données en utilisant les options de contexte configurées.
            SelfieContext context = new SelfieContext(builder.Options);

            return context;
        }
    }
}
#region Plus d'infos sur l'utilité de la classe !
//Cette interface est utilisée pour fournir un contexte de base de données au design time.
//Elle permet aux outils de création de migrations Entity Framework Core,
//tels que la commande Add-Migration dans Visual Studio, d'instancier un contexte de base de données
//à partir d'un code séparé de l'application.

//L'utilisation d'un ContextFactory permet de configurer le contexte de base de données
//de manière indépendante de l'application, ce qui est particulièrement utile pour la création
//de migrations dans des environnements où l'application n'est pas exécutable, tels que des scripts
//de déploiement ou des tests automatisés.

//En résumé, le ContextFactory est un moyen de fournir un contexte de base de données
//pour les outils de migration Entity Framework Core à partir d'un code indépendant de l'application.
#endregion

#region CLI pour EF !
//* Toujours faite une migrations avant de pouvoir Update notre base de données *
//dotnet ef => liste les commandes / infos de base.
//dotnet ef migrations list => listes toutes nos migrations.
//dotnet ef migrations add leNomDeLaMigration => ajout d'une migration.
//dotnet ef migrations add leNomDeLaMigration --project=leNomDuProjet => ajout d'une migration en ciblant un projet.
//dotnet ef database update => mise à jour de la base de données.
#endregion