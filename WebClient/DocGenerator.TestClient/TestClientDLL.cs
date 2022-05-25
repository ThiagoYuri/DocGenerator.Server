using DocGenerator.ClientDLL;
using DocGenerator.ClientDLL.Models;
using DocGenerator.TestClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            HttpResponseMessage responseMessage = requestDocGenerator.postDocument(new List<DocumentInfo>() { }, System.IO.File.ReadAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\\Certificate.docx"));
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
