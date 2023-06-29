using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Models
{
    public class Genere
    {
        public byte Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public byte[] Poster { get; set; } = null!;
    }
}
