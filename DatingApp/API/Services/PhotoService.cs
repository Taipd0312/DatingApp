using System.Threading.Tasks;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions)
        {
        }

        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            throw new System.NotImplementedException();
        }
    }
}