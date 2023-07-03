using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        public async static Task<Dictionary<string,string>?> UploadImageAsync(IFormFile FormFile, Cloudinary cloudinary)
        {
            if (FormFile.Length > ImageSize || !Extensions.Contains(Path.GetExtension(FormFile.FileName).ToLower()))
                return null;

            var ImageStream = FormFile.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("Image", ImageStream),
                FilenameOverride = Guid.NewGuid().ToString() + Path.GetExtension(FormFile.FileName)
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return new() { {"Url",uploadResult.SecureUrl.ToString() }, {"PublicId",uploadResult.PublicId } };
        }
        public async static Task DeleteAsync(string PublicId,Cloudinary cloudinary)
        {
            var DeletionParams =new DeletionParams(PublicId);

            await cloudinary.DestroyAsync(DeletionParams);
        }
    }
}
