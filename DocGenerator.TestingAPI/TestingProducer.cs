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
    
    public class TestingProducer
    {
        [Fact]
        public void TestMethodPost()
        {

        }


        #region GET Testing
        [Fact]
        public void TestMethodGetOk()
        {
            RequestDocGenerator requestDocGenerator = new RequestDocGenerator();
            HttpResponseMessage responseMessage = requestDocGenerator.getDocument("75d05ba4-159e-4d96-a02f-70aba35fda58");
            Assert.Equal<int>(200, (int)responseMessage.StatusCode);
        }

        [Fact]
        public void TestMethodGetBadRequest()
        {
            RequestDocGenerator requestDocGenerator = new RequestDocGenerator();
            HttpResponseMessage responseMessage = requestDocGenerator.getDocument("EmptyURL");
            Assert.Equal<int>(404, (int)responseMessage.StatusCode);
        }
        #endregion

    }
}
