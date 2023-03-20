
namespace SelfieWookie.Core.Domain
{
    public class Wookie
    {

        //ATTENTION LES PROP DOIVENT ETRE IDENTIQUE A MA DB
        #region Properties

        public int Id { get; set; }

        public string? Surname { get; set; }

        public List<Selfie>? Selfies { get; set; }

        #endregion
    }
}
