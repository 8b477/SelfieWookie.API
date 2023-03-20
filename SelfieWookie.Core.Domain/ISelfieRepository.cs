using SelfieWookie.Core.Framework;

namespace SelfieWookie.Core.Domain
{ 
    /// <summary>
    /// Handler of Selfie.
    /// </summary>
    {

        /// <summary>
        /// Return List of Selfies.
        /// </summary>
        /// <returns></returns>    
        List<Selfie> GetAll();

        /// <summary>
        /// Get Wookie with Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Wookie? GetById(int id);

        /// <summary>
        /// Get Wookie and Selfie connected with id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Selfie>? GetByIDWookieAndSelfie(int id);


        /// <summary>
        /// Add Selfie in Data Base.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Selfie? AddOne(Selfie item);

        /// <summary>
        /// Add picture.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Picture AddOnePicture(string url);
    }
}
