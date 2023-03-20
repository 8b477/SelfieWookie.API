namespace SelfieWookie.Core.Domain
{

    public class Selfie
    {
        // ATTENTION LES PROP DOIVENT ETRE IDENTIQUE A MA DB
        // Not Null en DB est égal à Required !

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int WookieID { get; set; }
        public int? PictureID { get; set; }
        public Wookie? Wookie { get; set; }
        public Picture? Picture { get; set; }
    }
}
