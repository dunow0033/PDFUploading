using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PDFuploading.Options;
using PDFuploading.Services.Abstract;
using System.Net.Http.Headers;

namespace PDFuploading.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly AzureOptions _azureOptions;
        public ImageService(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
        }

        public void UploadImageToAzure(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);

            using MemoryStream fileUploadStream = new MemoryStream();
            file.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;
            BlobContainerClient blobContainerClient = new BlobContainerClient(
                _azureOptions.ConnectionString,
                _azureOptions.Container);

            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            {

                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "application/pdf"
                }
            }, cancellationToken: default);
        }
    }
}
