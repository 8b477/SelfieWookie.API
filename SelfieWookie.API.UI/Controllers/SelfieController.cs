using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using SelfieWookie.API.UI.Apllications.DTO;
using SelfieWookie.API.UI.ExtensionMethods;
using SelfieWookie.Core.Domain;

namespace SelfieWookie.API.UI.Controllers
{

    #region Commentaire/Mémo Controller
    //    Si on laisse controller celui-ci prendras automatiquement le nom de la classe du controller
    // => Exemple ici ma classe se nomme SelfieController
    // => "api/[controller]" devient donc
    // => api/Selfie 
    #endregion

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)] // => Je spécifie un CORS particulier, valide la requete AJAX ou WebSocket.

    public class SelfieController : ControllerBase
    {

        #region Injections.

        private readonly ISelfieRepository _repository;

        // Donne l'emplacement de l'application sur notre pc,
        // permet de retrouver dans quelle dossier
        // et de créée d'autre dossier, enregistrer, ..
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor.

        public SelfieController(ISelfieRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = hostEnvironment;
        }

        #endregion
        #endregion

        #region Donne la liste des Selfie -> Qui sont lié par au moins un Wookie.

        /// <summary>
        /// Renvoie la liste des Selfies
        /// </summary>
        /// <returns> "{ ICollection<Selfie> }" </returns>
        /// <response code = "200">Succès</response>
        /// <response code = "204">NoContent</response>
        /// <response code = "404">NotFound</response>

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            #region Commentaire

            // Je récupère en premier ma méthode via le repository < GetAll() >
            // Je crée un modèle à l'intérieur de ma variable < model >
            // un nouvelle objet constuit sur base des critères dispo dans mon DTO 
            #endregion

            var selfieList = _repository.GetAll();

            if (selfieList is not null)
            {
                var model = selfieList.Select(item => new SelfieResumeDto()
                {
                    WookieID = item.WookieID,
                }).ToList();

                if (model == null)
                {
                }

                return Ok(model);
            }
            return BadRequest("Une erreur s'est produite !");
        }

        #endregion

        #region Méthode pour ajouter un Selfie.

        /// <summary>
        /// Ajout d'un Selfie en DB
        /// </summary>
        /// <param name="selfie"></param>
        /// <returns>
        /// <response code = "200">Succès</response>
        /// <response code = "400">BadRequest</response>
        /// </returns>
        [HttpPost]
        public IActionResult AddOne(SelfieDto model)
        {

            IActionResult result = BadRequest();


            Selfie? mapGetSelfie = SelfieDto.MapDtoToSelfie(newDto);

            if (mapGetSelfie is not null)
            {
            Selfie? addNewSelfie = _repository.AddOne(mapGetSelfie);


            if (addNewSelfie != null)
            {
                model.Id = addNewSelfie.Id; // On met à jour l'id !

                _repository?.UnitWork?.SaveChanges();

                result = Ok(model);
            }
            }

            return result;
        }

        #endregion

        #region Retourne un Wookie sur base de son ID.

        [HttpGet]
        [Route("GetByIdWookie")]
        public IActionResult GetById([FromQuery] int id)
        {
            return Ok(_repository.GetById(id));
        }

        #endregion

        #region Retourne un model SelfieDto sur base d'un ID récupérer par Query.

        [HttpGet]
        [Route("GetByIdSelfieResumeDto")]
        {

            //Je récupère la méthode préparé dans mon DefautlRepository
            var result = _repository.GetByIDWookieAndSelfie(id);

            // Je construit le model que je veut retourner ici SelfieResumeDto
            // J'ai créé cette classe au préalable
            // dans ma solution principal cad => UI
            // => configuration
            // => DTO
            //var model = result.Select(item => new SelfieResumeDto()
            //{
            //    Title = item.Title,
            //    WookieID = item.WookieID,
            //    NbSelfieWookie = (item.Wookie?.Selfies?.Count()).GetValueOrDefault(0)
            //});

            //return Ok(model);
            return Ok(result);

        }

        #endregion

        #region Méthode pour ajouter une photos (1).

        //[Route("photo")]
        //[HttpPost]
        //public async Task<IActionResult> AddPicture()
        //{

        //    // J'ouvre mon StreamReader pour lire le contenu de mon fichier images reçus.
        //    using var stream = new StreamReader(this.Request.Body);

        //    //Je le lis en traitement asynchrone.
        //    var content = await stream.ReadToEndAsync();

        //    return this.Ok();
        //}

        #endregion

        #region Pour ajouter une photo (2), enregistrer l'image dans un/des dossier(s), où l'application s'exécute.

        /// <summary>
        /// Enregistre une image.
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        [Route("photo")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            // Je donne le chemin ou sera stocker/enregistrer la picture.
            // Avec _hostEnvironement je vais chercher le chemin ou se trouve le programme sur le pc de l'utilisateur, dans quelle dossier ?
            // Ensuite je combine le chemin de l'appli avec le chemin/dossier que je veux ajout.
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");

            // Je check si celui ci n'est pas existant, si il n'existe pas => je le crée.
            if (! Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // Une fois que je sais que mon fichier existe, je lui donne le chemin du dossier ou le stocker plus son propre nom.
            filePath = Path.Combine(filePath, picture.FileName);

            // Pour enregistrer la meilleur façon est d'utiliser FileStream().
            // 1 param => le chemin.
            // 2 param => le mode.
            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);

            // On copie notre élément
            await picture.CopyToAsync(stream);

            // J'appelle ma méthode crée dans mon interface ISelfieRepository qui est implémenter par ma classe DefaultSelfieRepository
            var itemFile = _repository.AddOnePicture(@"images\selfies");

            _ = _repository?.UnitWork?.SaveChanges();

            // Je retourne un 200 Ok() tout est bien qui fini bien.
            return Ok();
        }
        #endregion
    }
}
