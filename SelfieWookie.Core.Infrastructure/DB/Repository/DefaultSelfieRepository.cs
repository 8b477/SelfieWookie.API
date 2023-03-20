using Microsoft.EntityFrameworkCore;

using SelfieWookie.Core.Domain;
using SelfieWookie.Core.Framework;
using SelfieWookie.Core.Infrastructure.DB;

namespace SelfieWookie.Core.Infrastructure.Data.Repository
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        private readonly SelfieContext _context;

        public DefaultSelfieRepository(SelfieContext context)
        {
            _context = context;
        }


        // On instancie notre UnitOfWork sur noter context actuel.
        public IUnitOfWork? UnitWork => _context;




        /// <summary>
        /// Ajoute un Selfie dans la base de données.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Selfie? AddOne(Selfie? item)
        {
            if (item is not null)
            {
                var result = _context.Add(item).Entity;
                return result;
            }
            return null;
        }



        /// <summary>
        /// Ajoute une photo en base de données.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Picture AddOnePicture(string url)
        {
            return _context.Pictures.Add(new Picture()
            {
                Url = url
            }).Entity;
        }



        /// <summary>
        /// Retourne une liste avec les Wookies qui ont au moins un Selfie.
        /// </summary>
        /// <returns></returns>
        public List<Selfie> GetAll()
        {
            // Je récup mes Selfies et j'inclus (include) les liens avec mon Wookie !
            // Ceci est possible grace à ceci => builder.HasOne(x => x.Wookie).WithMany(x => x.Selfies);
            // Et au fait que j'ai préciser dans la représentation de ma table en classe que les Wookies
            // avais une prop list<Selfie> et les Selfie un Wookie.
            var result = _context.Selfies.Include(item => item.Wookie).ToList();
  
            return result;

        }



        /// <summary>
        /// Récupère un Wookie dans la base de données.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wookie? GetById(int id)
        {
            var result = _context.Wookies.FirstOrDefault(x => x.Id == id);

            return result;
        }



        /// <summary>
        /// Récupère un Wookie et son Selfie liée dans la base de données.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Selfie>? GetByIDWookieAndSelfie(int id)
        {
            var result = _context.Selfies.Include(item => item.Wookie)
                         .FirstOrDefault(item => item.Id == id);

            return result as ICollection<Selfie>;
        }
    }
}
