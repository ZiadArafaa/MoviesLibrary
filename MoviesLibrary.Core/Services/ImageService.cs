using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Services
{
    public static class ImageService
    {
        private const long ImageSize = 1_048_576;
        private static IList<string> Extensions = new List<string> { ".jpg", ".png" };

        public async static Task<byte[]?> CreateImageAsync(IFormFile FormFile)
        {
            if (FormFile.Length > ImageSize || !Extensions.Contains(Path.GetExtension(FormFile.FileName).ToLower()))
                return null;

            using var stream = new MemoryStream();
            await FormFile.CopyToAsync(stream);

            return stream.ToArray();
        }
    }
}
