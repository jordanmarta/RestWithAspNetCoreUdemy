using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Services.Interfaces;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : Controller
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult GetFilePDF()
        {
            byte[] buffer = _fileService.GetPDFFile();

            if(buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }


            return new ContentResult();
        }
    }
}
