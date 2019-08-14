using System;

namespace Kiki.Models.Requests.Identification
{
    public class BookInfoConfirmationModel
    {
        public string GoogleBooksId { get; set; }
        public Guid   KikiBookId    { get; set; }
    }
}