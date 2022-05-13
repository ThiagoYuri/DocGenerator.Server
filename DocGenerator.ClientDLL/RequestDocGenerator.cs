using DocGenerator.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator.ClientDLL
{
    public class RequestDocGenerator
    {
        private string urlDefault = "http://localhost:5000";
        private string controller = "DocumentWord";

        public void createRequestHeader()
        {
            throw new NotImplementedException();
        }

        public string? postDocument()
        {

            return null;
        }

        public HttpResponseMessage getDocument(string guidDocument)
        {
            Request request = new Request(urlDefault, controller, $"GetFile?id={guidDocument}");
            Task<HttpResponseMessage> task = request.get();
            //string teste = task.Result.StatusCode.ToString();
            return task.Result;
        }
    }
}
