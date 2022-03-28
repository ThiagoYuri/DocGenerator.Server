﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
                if(file == null)
                    throw new Exception("File is null");
                if (file.Length <= 0)
                    throw new Exception("Empty file");
                if (!file.FileName.Contains(".docx"))
                    throw new Exception("support extension .docx");

                DocumentWord docWord = new DocumentWord(Request.Headers["User-Agent"]);
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
