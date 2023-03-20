using SelfieWookie.Core.Domain;

namespace SelfieWookie.API.UI.Apllications.DTO
{
    public class SelfieDto
    {
        #region Properties

        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Prend SelfieDto et le retourne sous forme Selfie.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Selfie? MapDtoToSelfie(SelfieDto model)
        {
            return wookie;
        }
    }
}
