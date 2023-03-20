using Microsoft.Extensions.Logging;

namespace SelfieWookie.Core.Infrastructure.Logger
{
    public class LoggerCustomMessage : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.Trace;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"{DateTime.Now} : #{logLevel.ToString()}# {formatter(state,exception)}");
        }
    }
}
/* Informations supplémentaire sur la classe !
   ------------------------------------------

< BeginScope > : 
    Cette méthode est utilisée pour créer un nouveau IDisposable pour un état de journalisation particulier.
    Cette méthode est utilisée pour ajouter un contexte spécifique au journal des événements. Par exemple, si vous voulez
    suivre les événements liés à une requête HTTP, vous pouvez utiliser cette méthode pour créer un IDisposable
    pour stocker les informations de contexte de la requête. Le retour de cette méthode est IDisposable et le type
    de paramètre générique TState est le type de l'objet d'état.

< IsEnabled > : 
    Cette méthode détermine si un niveau de journalisation spécifié est activé pour un logger donné.
    Si la méthode retourne false, alors l'enregistrement de journal est ignoré. Cette méthode prend un paramètre
    de type LogLevel qui représente le niveau de journalisation.

< Log > : 
    Cette méthode est utilisée pour enregistrer des informations de journalisation. Elle prend plusieurs paramètres,
    notamment le niveau de journalisation (LogLevel), l'ID de l'événement (EventId), l'état (TState),
    l'exception (Exception?) et le formateur (Func<TState, Exception?, string>). Le paramètre TState est l'état associé
    à l'événement de journalisation, tandis que Exception contient des informations sur l'exception, le cas échéant.
    Le paramètre formatter est une fonction qui formate les informations de journalisation en une chaîne de caractères.

En résumé :
----------
    Toutes ces méthodes sont utilisées pour enregistrer des informations de journalisation dans différentes situations.
    La méthode IsEnabled permet de vérifier si le niveau de journalisation est activé avant d'enregistrer les informations,
    tandis que la méthode BeginScope permet de stocker des informations de contexte supplémentaires avec chaque 
    enregistrement de journal. Enfin, la méthode Log est la méthode principale utilisée pour enregistrer des informations
    de journalisation avec différents niveaux de détails.

Doc Microsoft : https://learn.microsoft.com/fr-fr/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0#clms
*/