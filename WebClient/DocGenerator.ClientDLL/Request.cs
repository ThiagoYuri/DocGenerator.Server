using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator.ClientDLL
{
    internal class Request
    {
        private static readonly HttpClient client = new HttpClient();

        private string urlFull;

        MultipartFormDataContent formDataContent = new MultipartFormDataContent();

        /// <summary>
        /// Contructor Request
        /// </summary>
        /// <param name="urlAPI"></param>
        /// <param name="controller"></param>
        /// <param name="endPoint"></param>
        public Request(string urlAPI, string controller, string endPoint)
        {
            urlFull = $"{urlAPI}/{controller}/{endPoint}";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/user"));
            client.DefaultRequestHeaders.Add("User-Agent", "DocGenerator Client");
        }




        public async Task<HttpResponseMessage> get()
        {
            HttpResponseMessage message = await client.GetAsync(urlFull);
            return message;
        }


  
        
        public async Task<HttpResponseMessage> post()
        {
            return await client.PostAsync(urlFull, formDataContent);
        }
       

       
        public void CreateRequestBody(byte[] file)
        {
            // Get The ByteArrayContent,Reading Bytes From The Give File Path.
            // Adding It To The MultipartFormDataContent Once File Is Read.
            ByteArrayContent fileContent = new ByteArrayContent(file);

            // Add Content Type For MediaTypeHeaderValue.
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            // Add The File Under ''input'
            formDataContent.Add(fileContent, "file", "file.docx");
        }

    }
}
