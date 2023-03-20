namespace SelfieWookie.Core.Framework
{

    #region Commentaire

    // Notre classe ici permet d'enregistrer en dehors de nos projet liée par Entity Framework
    // PATERN => < 'Unit Of Work' > 
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}