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
        /// <summary>
        /// Edit and Post file .docx convert to .pdf
        /// </summary>
        /// <param name="file"></param>
        /// <returns>ID pdf</returns>
        [HttpPost]
        [Route("PostFile")]
        public async Task<JsonResult> DownloadPdfFile(IFormFile file)
        {
            try
            {
                DocumentWord docWord = new DocumentWord(Request.Headers["User-Agent"]);
                FileInfo fileInfo = new FileInfo(file.FileName);
                
                if (file.Length <= 0)
                    throw new Exception("Empty file");
                if (fileInfo.Extension.ToLower() != ".docx")
                    throw new Exception("support extension .docx");

                docWord.DocumentWordConvertPdf(file);
                return new JsonResult(docWord);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }           
        }


        /// <summary>
        /// Get file .pdf by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>File PDF</returns>
        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetPdfFile(string id)
        {
            try
            {
                var stream = new FileStream($"{Properties.StaticProperties.pathDefaultPdf}/{id}.pdf", FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            catch(FileNotFoundException)
            {
                return BadRequest("Erro: file not exist");
            }
            catch (Exception e )
            {
                return BadRequest(e.Message);
            }

        }

    }
}
