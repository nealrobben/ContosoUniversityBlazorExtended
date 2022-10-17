using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared;

namespace WebUI.Client.Services;

public interface IFileuploadService
{
    Task<string> UploadFile(IBrowserFile file);
}

public class FileuploadService : IFileuploadService
{
    private HttpClient _http;

    const long maxFileSize = 1024 * 1024 * 15;

    public FileuploadService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> UploadFile(IBrowserFile file)
    {
        var storedFileName = "";
        using var content = new MultipartFormDataContent();

        try
        {
            var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

            content.Add(content: fileContent, name: "\"files\"", fileName: file.Name);

            var response = await _http.PostAsync("api/File", content);

            var newUploadResults = await response.Content.ReadFromJsonAsync<IList<UploadResult>>();

            if (newUploadResults.Count != 0)
            {
                storedFileName = newUploadResults.First().StoredFileName;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return storedFileName;
    }
}
