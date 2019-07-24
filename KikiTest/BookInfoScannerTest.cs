using System.Collections.Generic;
using Google.Apis.Books.v1.Data;
using Kiki.Identification;
using Kiki.Models.Scanning;
using Xunit;

namespace KikiTest
{
    public class BookInfoScannerTest
    {
        [Fact]
        public void SearchByISBN()
        {
            BookInfoScanner scanner = new BookInfoScanner();
            List<GoogleBook> tradingInDangerResults = scanner.SearchBookByISBN("0-345-44760-3");
            Assert.Single(tradingInDangerResults);
            // Assert.Equal("Trading in Danger", tradingInDangerResults[0].VolumeInfo.Title);
        }
    }
}