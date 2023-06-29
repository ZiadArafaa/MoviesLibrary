using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace MoviesLibrary.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [MaxLength(100)]
        public string PosterUrl { get; set; } = null!;
        public int PublishingYear { get; set; }
    }
}
