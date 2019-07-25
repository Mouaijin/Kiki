namespace Kiki.Models.Scanning
{
    /// <summary>
    /// Used for parsing ISBNs from Google Books API
    /// </summary>
    public class IndustryIdentifier
    {
        ///eg. ISBN10 or ISBN13
        public string IdentificationType { get; set; }
        
        ///The actual ISBN 
        public string IdentificationCode { get; set; }

        public IndustryIdentifier(string identificationType, string identificationCode)
        {
            IdentificationType = identificationType;
            IdentificationCode = identificationCode;
        }
    }
}