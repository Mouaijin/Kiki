using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using Kiki.Models.Scanning;

namespace Kiki.Identification
{
    public class BookInfoScanner
    {
        private BooksService _booksService;

        public BookInfoScanner()
        {
            //todo: more friendly way of storing//accessing API key. 
            //!: no error handling or logging, no graceful exiting
            var apiKey = "";
            try
            {
                apiKey = File.ReadAllText(
                                          Path.Combine(
                                                       AppDomain.CurrentDomain.BaseDirectory, "Properties", "BooksApiKey.txt"
                                                      )
                                         )
                    ;
            }
            catch (FileNotFoundException ex)
            {
                
            }

            _booksService = new BooksService(new BaseClientService.Initializer
                                             {
                                                 ApplicationName = "Kiki Audiobook Server",
                                                 ApiKey = apiKey
                                             });
        }


        public List<GoogleBook> SearchBookByISBN(string isbn)
        {
            return SearchBooksBase($"isbn:{isbn.Remove("-")}");
        }

        public List<GoogleBook> SearchByTitle(string title)
        {
            return SearchBooksBase($"intitle:{title}");
        }

        private List<GoogleBook> SearchBooksBase(string query)
        {
            var request = _booksService.Volumes.List(query);
            Volumes volumes = request.Execute();
            return ParseBookResponse(volumes.Items).ToList();


            IEnumerable<GoogleBook> ParseBookResponse(IList<Volume> vs)
            {
                foreach (var volume in vs)
                {
                    GoogleBook book = new GoogleBook
                                      {
                                          Title = volume.VolumeInfo.Title,
                                          Authors = volume.VolumeInfo.Authors as List<string>,
                                          Description = volume.VolumeInfo.Description,
                                          ThumbnailLink = volume.VolumeInfo.ImageLinks.Thumbnail,
                                          Language = volume.VolumeInfo.Language,
                                          Publisher = volume.VolumeInfo.Publisher,
                                          Published = volume.VolumeInfo.PublishedDate,
                                          Category = volume.VolumeInfo.MainCategory,
                                          IndustryIdentifiers = volume.VolumeInfo.IndustryIdentifiers.Select(i => new IndustryIdentifier(i.Type, i.Identifier)).ToList()
                                      };
                    yield return book;
                }
            }
        }
    }
}