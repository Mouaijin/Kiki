using System;
using System.IO;
using Kiki.FileSys;
using Xunit;

namespace KikiTest
{
    public class ScanTest
    {
        private readonly string ScanDirectory = 

           Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestBooks");
        
        [Fact]
        public void FlatDirectoryToAudioBook()
        {
            DirectoryScanner scanner = new DirectoryScanner();
            var audioBooks = scanner.ScanForBooks(ScanDirectory);
            Assert.NotEmpty(audioBooks);
        }
    }
}