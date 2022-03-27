using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocGenerator.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentWordController : ControllerBase
    {
        //[HttpGet("{byte}")]
        [HttpGet]
        [Route("Stream")]
        public async Task<IActionResult> DownloadPdfFile(IFormFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.FileName);
                if (file.Length <= 0)
                    return BadRequest("Empty file");
                if (fileInfo.Extension.ToLower() != ".docx")
                    throw new Exception("support extension .docx");

                var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    ms.Position = 0;
                return File(file.OpenReadStream(), "application/docx", "File.docx");
                

                // new FileStreamResult(stream, mimeType)
                //
                //   FileDownloadName = "File.pdf"
                // };
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}
