namespace Kiki.Models.System
{
    /// <summary>
    /// Holds credentials used for accessing 3rd-party services by system
    /// </summary>
    public class SystemServiceCredentials
    {
        public string GoogleBooksApiKey { get; set; }
        public bool GoogleBooksApiKeyWorks { get; set; }
        
        //?: Further services to be added
    }
}