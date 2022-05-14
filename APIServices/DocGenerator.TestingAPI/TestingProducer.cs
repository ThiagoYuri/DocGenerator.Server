using DocGenerator.Producer.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocGenerator.TestingAPI
{
    
    public class TestingProducer
    {
        #region Post Testing
        [Fact]
        public void TestMethodPostOK()
        {
            DocumentWordController documentWordController = new DocumentWordController();
            Stream s = new MemoryStream(Resource1.Certificate);
            FormFile f = new FormFile(s, 0, s.Length, "teste", "teste.docx");
            Task<JsonResult> task = documentWordController.PostWordtoConvertPDF(Resource1.jsonDefault, f );
            Assert.Equal(200, task.Result.StatusCode);
        }

        [Fact]
        public void TestMethodPostErroJson()
        {
            DocumentWordController documentWordController = new DocumentWordController();
            Stream s = new MemoryStream(Resource1.Certificate);
            FormFile f = new FormFile(s, 0, s.Length, "teste", "teste.docx");
            Task<JsonResult> task = documentWordController.PostWordtoConvertPDF("", f);
            Assert.Equal(500, task.Result.StatusCode);
        }

        [Fact]
        public void TestMethodPostErroFileEmpty()
        {
            DocumentWordController documentWordController = new DocumentWordController();
            Stream s = new MemoryStream(Resource1.Empty);
            Task<JsonResult> task = documentWordController.PostWordtoConvertPDF(Resource1.jsonDefault, new FormFile(s,0,s.Length, "teste", "teste.docx"));
            Assert.Equal(500, task.Result.StatusCode);
        }

        [Fact]
        public void TestMethodPostErroFileNull()
        {
            DocumentWordController documentWordController = new DocumentWordController();
            Task<JsonResult> task = documentWordController.PostWordtoConvertPDF(Resource1.jsonDefault, default(IFormFile));
            Assert.Equal(500, task.Result.StatusCode);
        }
        [Fact]
        public void TestMethodPostErroSupportExtensionDocx()
        {
            DocumentWordController documentWordController = new DocumentWordController();
            Stream s = new MemoryStream(Resource1.filePdf);
            Task<JsonResult> task = documentWordController.PostWordtoConvertPDF(Resource1.jsonDefault, new FormFile(s, 0, s.Length, "teste", "teste.pdf"));
            Assert.Equal(500, task.Result.StatusCode);
        }

        #endregion

        #region GET Testing
        [Fact]
        public void TestMethodGetOk()
        {
            DocumentWordController ctl = new DocumentWordController();
            
            Task<IActionResult> task = ctl.GetPdfFile(Resource1.nameFileTesting);
            Assert.Equal<int>(200, ReturnStatusCode(task));
        }

        [Fact]
        public void TestMethodGetNotFound()
        {

            DocumentWordController ctl = new DocumentWordController();
            Task<IActionResult> task = ctl.GetPdfFile("EmptyURL");
            Assert.Equal<int>(404, ReturnStatusCode(task));
        }
               
        #endregion


        static int ReturnStatusCode(Task<IActionResult> task)
        {
            return (int)task.Result.GetType().GetProperty("StatusCode").GetValue(task.Result);
        }
    }
}
