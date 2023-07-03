using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace MoviesLibrary.Core.Dtos
{
    public abstract class MovieDto
    {
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        [MaxLength(500)]
        public string Description { get; set; } = null!;
        [AssertThat("PublishingDate <= Today()", ErrorMessage = "Check Publish Date.")]
        public DateTime PublishingDate { get; set; }
        public byte GenereId { get; set; }
    }
}
