namespace SelfieWookie.Core.Framework
{
    // Notre classe ici permet d'enregistrer en dehors de nos projet liée par Entity Framework
    // PATERN => < 'Unit Of Work' > 
    // Ne pas oublier de l'ajouter à notre context voire dans Infrastructure => DefaultSelfieRepository!
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}