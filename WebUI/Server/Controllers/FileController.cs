using Application.Common.Interfaces;
using ContosoUniversityBlazor.WebUI.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebUI.Shared;

namespace WebUI.Server.Controllers;

public class FileController : ContosoApiController
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FileController> _logger;
    private readonly IProfilePictureService _profilePictureService;

    public FileController(IWebHostEnvironment env, ILogger<FileController> logger, IProfilePictureService profilePictureService)
    {
        _env = env;
        _logger = logger;
        _profilePictureService = profilePictureService;
    }

    [HttpPost]
    public async Task<ActionResult<IList<UploadResult>>> UploadFile([FromForm] IEnumerable<IFormFile> files)
    {
        const int maxAllowedFiles = 3;
        const long maxFileSize = 1024 * 1024 * 15;
        var filesProcessed = 0;
        var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
        List<UploadResult> uploadResults = new();

        foreach (var file in files)
        {
            var uploadResult = new UploadResult();
            string trustedFileNameForFileStorage;
            var untrustedFileName = file.FileName;
            uploadResult.FileName = untrustedFileName;
            var trustedFileNameForDisplay =
                WebUtility.HtmlEncode(untrustedFileName);

            if (filesProcessed < maxAllowedFiles)
            {
                if (file.Length == 0)
                {
                    _logger.LogInformation("{FileName} length is 0 (Err: 1)",
                        trustedFileNameForDisplay);
                }
                else if (file.Length > maxFileSize)
                {
                    _logger.LogInformation("{FileName} of {Length} bytes is " +
                        "larger than the limit of {Limit} bytes (Err: 2)",
                        trustedFileNameForDisplay, file.Length, maxFileSize);
                }
                else
                {
                    try
                    {
                        trustedFileNameForFileStorage = (Guid.NewGuid()).ToString() + ".jpg"; //May not actually be a jpeg, fix later

                        await using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            await _profilePictureService.WriteImageFile(trustedFileNameForFileStorage, ms);
                        };

                        _logger.LogInformation("{FileName} saved",
                            trustedFileNameForDisplay);
                        uploadResult.Uploaded = true;
                        uploadResult.StoredFileName = trustedFileNameForFileStorage;
                    }
                    catch (IOException ex)
                    {
                        _logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                            trustedFileNameForDisplay, ex.Message);
                        uploadResult.ErrorCode = 3;
                    }
                }

                filesProcessed++;
            }
            else
            {
                _logger.LogInformation("{FileName} not uploaded because the " +
                    "request exceeded the allowed {Count} of files (Err: 4)",
                    trustedFileNameForDisplay, maxAllowedFiles);
                uploadResult.ErrorCode = 4;
            }

            uploadResults.Add(uploadResult);
        }

        return new CreatedResult(resourcePath, uploadResults);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(string id)
    {
        var path = Path.Combine(_env.ContentRootPath, "Img", "ProfilePictures", id);
        var bytes = await _profilePictureService.GetImageFile(path);
        return File(bytes, "image/jpeg", Path.GetFileName(path));
    }
}
