using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using Kiki.Models.Data;
using Kiki.Models.Scanning;
using Kiki.Models.System;

namespace Kiki.Identification
{
    public class BookInfoScanner
    {
        private BooksService _booksService;

        public BookInfoScanner()
        {
            //todo: more friendly way of storing//accessing API key. 
            //!: no error handling or logging, no graceful exiting
            string apiKey = new SystemServiceCredentialsDev().GoogleBooksApiKey;
//            try
//            {
//                apiKey = File.ReadAllText(
//                                          Path.Combine(
//                                                       AppDomain.CurrentDomain.BaseDirectory, "Properties", "BooksApiKey.txt"
//                                                      )
//                                         )
//                    ;
//            }
//            catch (FileNotFoundException ex)
//            {
//                
//            }

            _booksService = new BooksService(new BaseClientService.Initializer
                                             {
                                                 ApplicationName = "Kiki Audiobook Server",
                                                 ApiKey          = apiKey
                                             });
        }

        public List<GoogleBook> SearchAudiobook(AudioBook audioBook)
        {
            List<GoogleBook> firstPass = InitialScan();

            List<GoogleBook> badCandidates = new List<GoogleBook>();
            //remove low-confidence matches
            foreach (GoogleBook googleBook in firstPass)
            {
                string lcs = googleBook.Title.LCS(audioBook.Files.First().Name);
                if (!audioBook.Files.First().Name.Contains(lcs, StringComparison.InvariantCultureIgnoreCase) &&
                    !audioBook.Files.First().AudioTags.AlbumName
                              .Contains(lcs, StringComparison.InvariantCultureIgnoreCase) &&
                    ( (float) lcs.Length / audioBook.Files.First().Name.Length < 0.6f ||
                      (float) lcs.Length / audioBook.Files.First().AudioTags.AlbumName.Length < 0.6f ))
                {
                    badCandidates.Add(googleBook);
                }
            }

            badCandidates.ForEach(x => firstPass.Remove(x));

            if (audioBook.Files.FirstOrDefault()?.AudioTags?.Year.HasValue ?? false)
            {
                List<GoogleBook> yearFilter = firstPass.Where(x => x.Published.HasValue &&
                                                                   x.Published.Value.Year ==
                                                                   audioBook.Files.First().AudioTags.Year).ToList();
                if (yearFilter.Count > 0)
                {
                    return yearFilter;
                }
            }

            return firstPass;

            List<GoogleBook> InitialScan()
            {
                //todo: split into individual file results and consolidate
                //reference first file for now
                AudioFile file = audioBook.Files.FirstOrDefault();
                if (file == null)
                {
                    return new List<GoogleBook>();
                }

                if (file.AudioTags != null)
                {
                    if (!string.IsNullOrEmpty(file.AudioTags.AlbumName))
                    {
                        Console.WriteLine($"Using album search {file.AudioTags.AlbumName}");
                        List<GoogleBook> albumResults = SearchByTitle(file.AudioTags.AlbumName.Trim());
                        if (albumResults.Count == 0)
                        {
                            Console.WriteLine("Splitting album name");
                            string[] split = file.AudioTags.AlbumName.Split('-');
                            if (split.Length > 1)
                            {
                                foreach (string s in split)
                                {
                                    albumResults = SearchByTitle(s.Trim());
                                    if (albumResults != null && albumResults.Count > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        if (albumResults != null && albumResults.Count > 0)
                            return albumResults;
                    }

                    if (!string.IsNullOrEmpty(file.AudioTags.Title))
                    {
                        Console.WriteLine("Using title");
                        List<GoogleBook> titleResults = SearchByTitle(file.AudioTags.Title);
                        if (titleResults != null && titleResults.Count > 0)
                            return titleResults;
                    }
                }

                Console.WriteLine("Using filename search");
                List<GoogleBook> filenameResults = SearchByTitle(file.FileName);
                return filenameResults;
            }
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
            var     request = _booksService.Volumes.List(query);
            Volumes volumes = request.Execute();
            return ParseBookResponse(volumes.Items).ToList();


            IEnumerable<GoogleBook> ParseBookResponse(IList<Volume> vs)
            {
                if (vs == null)
                    yield break;
                foreach (var volume in vs)
                {
                    bool parsedPublishDate = DateTime.TryParse(volume.VolumeInfo.PublishedDate, out DateTime published);
                    GoogleBook book = new GoogleBook
                                      {
                                          Title         = volume.VolumeInfo.Title,
                                          Authors       = volume.VolumeInfo.Authors as List<string>,
                                          Description   = volume.VolumeInfo.Description,
                                          ThumbnailLink = volume.VolumeInfo?.ImageLinks?.Thumbnail,
                                          Language      = volume.VolumeInfo.Language,
                                          Publisher     = volume.VolumeInfo.Publisher,
                                          Published     = parsedPublishDate ? published : (DateTime?) null,
                                          Category      = volume.VolumeInfo.MainCategory,
                                          IndustryIdentifiers =
                                              volume?.VolumeInfo?.IndustryIdentifiers
                                                    ?.Select(i => new IndustryIdentifier(i.Type, i.Identifier))
                                                    ?.ToList() ?? new List<IndustryIdentifier>()
                                      };
                    yield return book;
                }
            }
        }
    }
}