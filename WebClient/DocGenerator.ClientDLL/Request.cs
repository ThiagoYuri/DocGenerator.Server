using System;
using System.Collections.Generic;
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


  
        /*/
        public static async Task<HttpContent> post()
        {
            var stringTask = client.GetStringAsync("");

            var msg = await stringTask;
            Console.Write(msg);
        }
        /*/

        /*/
        public void CreateRequestBody()
        {

        }
        /*/
    }
}
