using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string PosterUrl { get; set; } = null!;
        public string PublicId { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public byte GenereId { get; set; }
        public Genere? Genere { get; set; }
    }
}
