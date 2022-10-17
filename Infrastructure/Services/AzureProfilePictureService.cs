using Application.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AzureProfilePictureService : IProfilePictureService
{
    private readonly IConfiguration _config;

    private string ConnectionString
    {
        get
        {
            return _config["StorageContainerConnectionString"];
        }
    }

    private string ContainerName
    {
        get
        {
            return _config["StorageContainerName"];
        }
    }

    public AzureProfilePictureService(IConfiguration config)
    {
        _config = config;
    }

    public async void DeleteImageFile(string name)
    {
        var blobServiceClient = new BlobServiceClient(ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        var blobClient = containerClient.GetBlobClient(name);

        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<byte[]> GetImageFile(string fullName)
    {
        var fileName = Path.GetFileName(fullName);

        var blobServiceClient = new BlobServiceClient(ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        using (var ms = new MemoryStream())
        {
            await blobClient.DownloadToAsync(ms);
            return ms.ToArray();
        }
    }

    public async Task WriteImageFile(string name, MemoryStream ms)
    {
        var blobServiceClient = new BlobServiceClient(ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        var blobClient = containerClient.GetBlobClient(name);

        ms.Position = 0;
        await blobClient.UploadAsync(ms, true);
    }
}
