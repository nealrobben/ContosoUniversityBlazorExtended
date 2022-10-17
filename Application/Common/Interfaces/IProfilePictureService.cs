using System.IO;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IProfilePictureService
{
    Task WriteImageFile(string name, MemoryStream ms);
    Task<byte[]> GetImageFile(string fullName);
    void DeleteImageFile(string name);
}
