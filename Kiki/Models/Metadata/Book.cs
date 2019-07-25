using System;
using System.Collections.Generic;

namespace Kiki.Models.Metadata
{
    public class Book
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Book title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Publication year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 1-based index of book in series
        /// </summary>
        public int SeriesEntry { get; set; }

        /// <summary>
        /// Two-digit language code
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// JPEG image data, base64-encoded
        /// </summary>
        public string ThumbnailData { get; set; }

        /// <summary>
        /// ISBN-10 number, if book has one
        /// </summary>
        public string ISBN10 { get; set; }

        /// <summary>
        /// ISBN-13 number, if book has one
        /// </summary>
        public string ISBN13 { get; set; }

        /// <summary>
        /// ID of series, if book is part of one
        /// </summary>
        public Guid? SeriesId { get; set; }

        /// <summary>
        /// Series book belongs to, if any
        /// </summary>
        public Series Series { get; set; }

        /// <summary>
        /// ID of publisher
        /// </summary>
        public Guid PublisherId { get; set; }

        /// <summary>
        /// Book publisher
        /// </summary>
        public Publisher Publisher { get; set; }

        /// <summary>
        /// List of authors for this book
        /// </summary>
        public List<BookAuthor> Authors { get; set; }

        /// <summary>
        /// True if book has a non-null SeriesId
        /// </summary>
        public bool IsPartOfSeries => SeriesId != null;
    }
}