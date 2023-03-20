using MediatR;

using SelfieWookie.API.UI.Apllications.DTO;

namespace SelfieWookie.API.UI.Apllications.Queries
{
    /// <summary>
    /// Query pour sélectionner un Wookie, retourne un SelfieResumeDto.
    /// </summary>
    public class SelectAllSelfieQuery : IRequest<List<SelfieResumeDto>> // J'implémente IRequest de MediatR et je spécifie le retour ici <List<SelfieResumeDto>>.
    {
        public int WookieId { get; set; } // => les paramètre d'entrée lors de ma request.
    }
}
