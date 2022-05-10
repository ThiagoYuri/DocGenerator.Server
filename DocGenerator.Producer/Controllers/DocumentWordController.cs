using DocGenerator.Shared;
using DocGenerator.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<JsonResult> DownloadPdfFile(string info, IFormFile file)
        {
            try
            {
                if(file == null)
                    throw new Exception("File is null");
                if (file.Length <= 0)
                    throw new Exception("Empty file");
                if (!file.FileName.Contains(".docx"))
                    throw new Exception("support extension .docx");

                                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                DocumentWord docWord = new DocumentWord(Request.Headers["User-Agent"],ms);
                docWord.ListNewInfoFile = JsonSerializer.Deserialize<List<DocumentInfo>>(info);
                //docWord.ListNewInfoFile = listStringPDF;
                new Publishe<DocumentWord>(docWord);
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
                var stream = new FileStream($"{Shared.Utils.GeneratorPDF.pathDefaultPdf}/{id}.pdf", FileMode.Open);
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
