using CsvHelper.Configuration.Attributes;

namespace Movie_Library
{
    public class Movies
    {
        [Name("movieId")]
        public int movieID { get; set; }
        
        [Name("title")]
        public string title { get; set; }
        
        [Name("genres")]
        public string genre { get; set; }
        
    }
}