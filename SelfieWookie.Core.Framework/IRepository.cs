
namespace SelfieWookie.Core.Framework
{
    /// <summary>
    /// Tout ce qui est Repository implémentera IRepository
    /// Qui instancie la propiété de type IUnitWork
    /// Qui donne accès à la méthode => SaveChange()
    /// </summary>

    public interface IRepository
    {
        IUnitOfWork? UnitWork { get; } // On l'affecte pas on le renvoie juste donc pas besoin du 'set'.
    }
}
