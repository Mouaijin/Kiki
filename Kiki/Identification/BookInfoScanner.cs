using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace Kiki.Identification
{
    public class BookInfoScanner
    {
        private string _apiKey = null;
        private BooksService _booksService = null;

        public BookInfoScanner()
        {
            //todo: more friendly way of storing//accessing API key. 
            //!: no error handling or logging, no graceful exiting
            _apiKey = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Properties", "BooksApiKey.txt"));
            _booksService = new BooksService(new BaseClientService.Initializer()
                                             {
                                                 ApplicationName = "Kiki Audiobook Server",
                                                 ApiKey = _apiKey
                                             });
        }


        public List<Volume> SearchBookByISBN(string isbn)
        {
            return SearchBooksBase($"isbn:{isbn.Remove("-")}");
        }

        public List<Volume> SearchByTitle(string title)
        {
            return SearchBooksBase($"intitle:{title}");
        }

        private List<Volume> SearchBooksBase(string query)
        {
            var request = _booksService.Volumes.List(query);
            Volumes volumes = request.Execute();
            return volumes.Items as List<Volume>;
        }
    }
}