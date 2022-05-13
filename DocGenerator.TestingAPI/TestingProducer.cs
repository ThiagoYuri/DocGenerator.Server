using DocGenerator.ClientDLL;
using DocGenerator.Producer.Controllers;
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

        }


        [Fact]
        public void TestMethodPost()
        {

        }
        #endregion

        #region GET Testing
        [Fact]
        public void TestMethodGetOk()
        {
            DocumentWordController ctl = new DocumentWordController();
            Task<IActionResult> task = ctl.GetPdfFile("75d05ba4-159e-4d96-a02f-70aba35fda58");
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
