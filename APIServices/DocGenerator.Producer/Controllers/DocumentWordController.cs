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
        public async Task<JsonResult> PostWordtoConvertPDF(string info, IFormFile file)
        {
            try
            {                
                if (file.Length <= 0)
                    throw new Exception("Empty file");
                if (!file.FileName.Contains(".docx"))
                    throw new Exception("support extension .docx");
                
                MemoryStream ms= new MemoryStream();
                file.OpenReadStream().CopyTo(ms);
                DocumentWord docWord = new DocumentWord(ms);
                docWord.ListNewInfoFile = JsonSerializer.Deserialize<List<DocumentInfo>>(info);
                new Publishe<DocumentWord>(docWord);
                return new JsonResult(docWord.Id) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new JsonResult(e) { StatusCode = 500 };
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
            catch(FileNotFoundException e)
            {
                return NotFound("Erro: file not exist");
            }
            catch (Exception e)
            {
                return StatusCode(500,$"Internal Server Erro: {e.Message}");
            }
        }


    }
}
