using System;
using System.Collections.Generic;
using System.IO;
using Kiki.FileSys;
using Kiki.Models;
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
            List<AudioBook> audioBooks = scanner.ScanForBooks(ScanDirectory);
            Assert.Equal(2, audioBooks.Count);
            Assert.Contains(audioBooks, book => book.Files.Count == 2);
            Assert.Contains(audioBooks, book => book.Files.Count == 3);
            AudioBook aFrequency = audioBooks.Find(x => x.Files.Count == 2);
            AudioBook lifeOfNoise = audioBooks.Find(x => x.Files.Count == 3);
            
            Assert.Equal(1, aFrequency.Files[0].TrackNumber);
            Assert.Equal("1 minute A", aFrequency.Files[0].Name);
            Assert.Equal(2, aFrequency.Files[1].TrackNumber);
            Assert.Equal("30 minute A", aFrequency.Files[1].Name);

            
            Assert.Equal(1, lifeOfNoise.Files[0].TrackNumber);
            Assert.Equal("Pink", lifeOfNoise.Files[0].Name);

            Assert.Equal(2, lifeOfNoise.Files[1].TrackNumber);
            Assert.Equal("Brown", lifeOfNoise.Files[1].Name);

            Assert.Equal(3, lifeOfNoise.Files[2].TrackNumber);
            Assert.Equal("White", lifeOfNoise.Files[2].Name);


            
        }
    }
}