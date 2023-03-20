
namespace SelfieWookie.Core.Infrastructure.Configurations
{
    /// <summary>
    /// Récupère notre clef secrete du côté "secret.json". 
    /// </summary>
    public class SecurityOption
    {
        public string? Key { get; set; } // Je donne le meme nom que celui dans le secret.json
    }
}
