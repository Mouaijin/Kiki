namespace Kiki.Models.System
{
    public class KikiError
    {
        public KikiErrorCode ErrorCode { get; private set; }
        public string Message { get; private set; }
        public string[] Details { get; private set; }

        public int DetailCount => Details.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public KikiError(KikiErrorCode errorCode, string message = null, params string[] details)
        {
            ErrorCode = errorCode;
            Message = message ?? errorCode.ToString();
            Details = details ?? new string[0];
        }
        
        
    }

    /// <summary>
    /// Internal error codes that should wrap the intent of any internal exceptions in communicating with clients
    /// </summary>
    public enum KikiErrorCode
    {
        Success = 0,
        UserDoesNotExist = 1,
        UserPasswordIncorrect = 2,
        DatabaseCorruptOrInaccessible = 3,
        DatabaseMigrationFailed = 4,
        DatabaseCreated = 5,
        FileNotFound = 6,
        FileCorruptOrInaccessible = 7,
        GBooksApiKeyNotAuthorized = 8,
        NoGBooksApiKeyFound = 9,
        
        
        
        
    }
    
}