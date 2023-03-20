
namespace SelfieWookie.Core.Domain
{
    public class Wookie
    {
        public int Id { get; set; }
        public string? Surname { get; set; }
        public List<Selfie>? Selfies { get; set; }
    }
}
