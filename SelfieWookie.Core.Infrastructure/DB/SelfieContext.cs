using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SelfieWookie.Core.Domain;
using SelfieWookie.Core.Framework;
using SelfieWookie.Core.Infrastructure.Data.TypeConfiguration;

using System.Diagnostics.CodeAnalysis;

namespace SelfieWookie.Core.Infrastructure.DB
{
    public class SelfieContext : IdentityDbContext, IUnitOfWork // Ici pour but d'apprentissage je passe mon DbContext en identityDbContext mais normalemnt on crée une API spécifique pour l'auhtentification.
    {

        #region MyRegion

        public SelfieContext() : base() { } // => Ne pas oublier d'instancier le ctor vide.


        #region Internal method

        // Permet de rajouter des fonctionnalité , dire comment une table fonctionne par rapport à une autre.
        // Donc une classe par rapport à une autres.
        // Et du coup rajouter pas mal de fonctionnalité autour de la base de données ("comment elle parle enfaite").

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WookieEntityTypeConfiguration());
        }

        #endregion


        #region Properties

        public DbSet<Selfie> Selfies { get; set; }
        public DbSet<Wookie> Wookies { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        #endregion

    }
}
