using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieWookie.Core.Domain;

namespace SelfieWookie.Core.Infrastructure.Data.TypeConfiguration
{
    public class SelfieEntityTypeConfiguration : IEntityTypeConfiguration<Selfie>
    {
        // Ne pas oublier de modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
        // => Dans le Model Builder (ici Selfieontext)

        public void Configure(EntityTypeBuilder<Selfie> builder)
        {
            // Ici je rename le nom de ma table qui était au pluriel par default
            builder.ToTable("Selfie");

            // Ici je m'assure qu'il prend l'id comme clef primaire
            builder.HasKey(x => x.Id);

            // Je lui dit que notre selfie n'a que un Wookie
            builder.HasOne(x => x.Wookie)
                // Ici je lui dit que par contre attention mon wookie peut avoir plusieurs >Selfie<.
                .WithMany(x => x.Selfies);
        }
    }
}
