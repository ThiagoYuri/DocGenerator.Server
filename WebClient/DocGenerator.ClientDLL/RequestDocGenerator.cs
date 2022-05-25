using DocGenerator.ClientDLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocGenerator.ClientDLL
{
    public class RequestDocGenerator
    {
        private const string urlDefault = "https://localhost:5001"; 
        private const string controller = "DocumentWord";


        public HttpResponseMessage postDocument(List<DocumentInfo> listInfo, byte[] file)
        {
            string json = JsonSerializer.Serialize<List<DocumentInfo>>(listInfo);

            Request request = new Request(urlDefault, controller, $"PostFile?info={json}");
            request.CreateRequestBody(file);
            Task<HttpResponseMessage> task = request.post();
            return task.Result;
        }

        public HttpResponseMessage getDocument(string guidDocument)
        {
            Request request = new Request(urlDefault, controller, $"GetFile?id={guidDocument}");
            Task<HttpResponseMessage> task = request.get();
            return task.Result;
        }
    }
}
