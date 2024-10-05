namespace PDFuploading.Services.Abstract
{
    public interface IImageService
    {
        void UploadImageToAzure(IFormFile file);
    }
}
