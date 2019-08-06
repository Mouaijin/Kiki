using System;
using TagLib;

namespace Kiki.Models.Data
{
    public class AudioTags
    {
        public Guid Id { get; set; }

        public string   Title        { get; set; }
        public string   Publisher    { get; set; }
        public string   Comment      { get; set; }
        public uint?    Disc         { get; set; }
        public string   Description  { get; set; }
        public string[] AlbumArtists { get; set; }
        public string[] Genres       { get; set; }
        public uint?    Year         { get; set; }
        public string   Subtitle     { get; set; }
        public string   Grouping     { get; set; }
        public uint?    Track        { get; set; }
        public uint?    DiscCount    { get; set; }
        public string[] Performers   { get; set; }

        public AudioTags()
        {
        }

        public AudioTags(Tag tag)
        {
            Publisher    = tag?.Publisher;
            Comment      = tag?.Comment;
            Disc         = tag?.Disc;
            Description  = tag?.Description;
            AlbumArtists = tag?.AlbumArtists;
            Genres       = tag?.Genres;
            Title        = tag?.Title;
            Year         = tag?.Year;
            Subtitle     = tag?.Subtitle;
            Grouping     = tag?.Grouping;
            Track        = tag?.Track;
            DiscCount    = tag?.DiscCount;
            Performers   = tag?.Performers;
        }
    }
}