using SelfieWookie.Core.Domain;

namespace SelfieWookie.API.UI.Apllications.DTO
{
    public class SelfieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }


        /// <summary>
        /// Prend SelfieDto et le retourne sous forme Selfie.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Selfie? MapDtoToSelfie(SelfieDto model)
        {
            Selfie wookie = new() { Title = model.Title };
            return wookie;
        }
    }
}
