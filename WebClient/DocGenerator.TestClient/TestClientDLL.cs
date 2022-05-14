using DocGenerator.ClientDLL;
using DocGenerator.TestClient;
using DocGenerator.TestClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DocGenerator.TestingAPI
{
    public class TestClientDLL
    {

       // private const string jsonDefault = JsonSerializer.Deserialize<List<DocumentInfo>>();

        #region Post Testing
        [Fact]
        public void TestMethodPostOK()
        {
            RequestDocGenerator requestDocGenerator = new RequestDocGenerator();
            HttpResponseMessage responseMessage = requestDocGenerator.postDocument(new List<DocumentInfo>() { }, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\\Certificate.docx");
            string json =  responseMessage.Content.ReadAsStringAsync().Result;
            Assert.Equal<int>(200, (int)responseMessage.StatusCode);
        }

        #endregion

        #region GET Testing
        [Fact]
        public void TestMethodGetOk()
        {
            RequestDocGenerator requestDocGenerator = new RequestDocGenerator();
            HttpResponseMessage responseMessage = requestDocGenerator.getDocument(Resource1.nameFileTesting);
            Assert.Equal<int>(200, (int)responseMessage.StatusCode);
        }

        [Fact]
        public void TestMethodGetNotFound()
        {
            RequestDocGenerator requestDocGenerator = new RequestDocGenerator();
            HttpResponseMessage responseMessage = requestDocGenerator.getDocument("EmptyURL");
            Assert.Equal<int>(404, (int)responseMessage.StatusCode);
        }
        #endregion
    }
}
