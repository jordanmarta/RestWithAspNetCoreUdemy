using RestWithAspNetCoreUdemy.Services.Interfaces;
using System.IO;

namespace RestWithAspNetCoreUdemy.Services.Concretes
{
    public class FileService : IFileService
    {
        public FileService()
        {

        }

        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\other\\guiagit.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
