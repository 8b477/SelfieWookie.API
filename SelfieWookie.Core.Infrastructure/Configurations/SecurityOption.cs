using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
