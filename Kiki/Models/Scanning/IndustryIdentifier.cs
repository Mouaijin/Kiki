namespace Kiki.Models.Scanning
{
    public class IndustryIdentifier
    {
        public string IdentificationType { get; set; }
        public string IdentificationCode { get; set; }

        public IndustryIdentifier(string identificationType, string identificationCode)
        {
            IdentificationType = identificationType;
            IdentificationCode = identificationCode;
        }
    }
}