using DocGenerator.ClientDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocGenerator.TestingAPI
{
    public class TestClientDLL
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
