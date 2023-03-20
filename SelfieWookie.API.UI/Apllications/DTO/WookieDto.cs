using SelfieWookie.Core.Domain;

namespace SelfieWookie.API.UI.Apllications.DTO
{
    public class WookieDto
    {
        public string? Surname { get; set; }


        /// <summary>
        /// Prend un Wookie et le retourne sous forme DTO.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WookieDto? MapWookieToDto(Wookie model) {

            WookieDto wookie = new WookieDto() { Surname = model.Surname };
            return wookie;
        }

        /// <summary>
        /// Prend WookieDto et le retourne sous forme Wookie.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Wookie? MapDtoToWookie(WookieDto model)
        {
            Wookie wookie = new Wookie() { Surname = model.Surname};
            return wookie;
        }
    }
}