using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocGenerator.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentWordController : ControllerBase
    {
        
        [HttpPost]
        [Route("PostFile")]
        public async Task<JsonResult> DownloadPdfFile(IFormFile file)
        {
            try
            {
                DocumentWord docWord = new DocumentWord("useAgent");
                FileInfo fileInfo = new FileInfo(file.FileName);
                
                if (file.Length <= 0)
                    throw new Exception("Empty file");
                if (fileInfo.Extension.ToLower() != ".docx")
                    throw new Exception("support extension .docx");

                docWord.DocumentWordConvertPdf(file);
                return new JsonResult(docWord);

                //return File(file.OpenReadStream(), "application/docx", "File.docx"); funcionou

            }
            catch (Exception)
            {

                throw;
            }           
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetPdfFile(string id)
        {
            try
            {
                string directory = Directory.GetCurrentDirectory() + "/doc/" + id + ".pdf";
                var stream = new FileStream(directory, FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            catch
            {
                throw;
            }

        }

    }
}
