// using Microsoft.AspNetCore.Mvc;
//using Moq;
//using SelfieWookie.API.UI.Apllications.DTO;
//using SelfieWookie.API.UI.Controllers;
//using SelfieWookie.Core.Domain;
//using SelfieWookie.Core.Framework;

//namespace TestWebAPI
//{
//    public class SelfieControllerUnitTest
//    {
//        #region Test m�thode Ajouter => AddOne()

//        [Fact] // Mot cl�e < Fact > pour d�limiter une partie d'un test d'un autre.
//        public void ShouldAddOneSelfie()
//        {
//            // ARRANGE
//            // => Pr�pare les donn�es sur lesquelle je veut travailler.

//            SelfieDto selfie = new SelfieDto();

//            var repositoryMock = new Mock<ISelfieRepository>(); // cr�ation d'un MocK = simuler un ensemble
//                                                                // de donn�es comme une base de donn�es, car le but
//                                                                // ici n'est pas de check la connection mais bien
//                                                                // le traitement de donn�es.

//            var unit = new Mock<IUnitOfWork>();

//            // Ici je m'assure que quand j'appelle UnitOfWork qu'il contienne bien Object et pas NULL
//            repositoryMock.Setup(item => item.UnitWork)
//                          .Returns(unit.Object);

//            // Ici je m'assure que mon =>
//            // < AddOne > � bien un comportement simuler donc est bien un < Mock >
//            // < It.Any > veut dire que pour n'importe quelle Selfie
//            // < Returns > un nouveau Selfie avec un Id = 4
//            repositoryMock.Setup(item => item.AddOne(It.IsAny<Selfie>())).Returns(new Selfie() {  Id = 4 });
              
//            // ACT
//            // => Action que l'on veut faire sur les donn�es pr�par� juste en haut.

//            var controller = new SelfieController(repositoryMock.Object);

//            var result = controller.AddOne(selfie);

//            // ASSERT
//            // => Ce que je veut avoir en sortie, le r�sultat.

//            Assert.NotNull(result);

//            Assert.IsType<OkObjectResult>(result);

//            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;

//            Assert.NotNull(addedSelfie);
//            Assert.True(addedSelfie?.Id > 0);
//        }

//        #endregion

//        #region Test m�thode, doit retourner une liste de Selfie => RenvoieAvecLesWookie

//        [Fact]
//        public void ShouldReturnListOfSelfie()
//        {
//            // ARRANGE
//            // => Pr�pare les donn�es sur lesquelle je veut travailler.

//            var expectedList = new List<Selfie>()
//            {
//                new Selfie() { Wookie = new Wookie() },
//                new Selfie() { Wookie = new Wookie() }
//            };

//            var repositoryMock = new Mock<ISelfieRepository>();

//            repositoryMock.Setup(item => item.GetAll()).Returns(expectedList);

//            var controller = new SelfieController(repositoryMock.Object);

//            // ACT
//            // => Action que l'on veut faire sur les donn�es pr�par� juste en haut.

//            var result = controller.RenvoieAvecLesWookie();

//            // ASSERT
//            // => Ce que je veut avoir en sortie, le r�sultat

//            Assert.NotNull(result); // je m'assure que le r�sultat ne sera pas null.

//            Assert.IsType<OkObjectResult>(result); //F12 sur le Ok() que j'utilise dans mon controller
//                                                   //pour voire de quelle type il est, il est de type
//                                                   //OkObjectResult, donc je check si mon result
//                                                   //est bien du type attendu.

//            OkObjectResult? okResult = result as OkObjectResult;

//            Assert.IsType<List<SelfieResumeDto>>(okResult?.Value);
//            Assert.NotNull(okResult?.Value);


//            List<SelfieResumeDto>? list = okResult?.Value as List<SelfieResumeDto>;

//            Assert.True(list?.Count == expectedList.Count());

//                     // je m'assure que mon r�sultat est bien une liste.
//                    // ( Test pour check si ses bien une liste )

//           //=> Assert.True(result.RenvoieAvecLesWookie().MoveNext()); 
//        }

//        #endregion

//        #region *TODO* Test m�thode qui renvoie un Wookie sur base de son ID *TODO*

//        [Fact]
//        public void ShouldGiveWookieByID()
//        {

//        //ARRANGE => je pr�pare les donn�es sur le quelle je vais travailler.


//        //ACT => je dit l'action que mes donn�es sont cens� �ffectuer.


//        //ASSERT => ici je check si le r�sultat de mon action est bien le r�sultat que j'attend.

//        }

//        #endregion
//    }
//}