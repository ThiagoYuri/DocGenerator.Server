using DocGenerator.ClientDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator.ClientDLL
{
    public class RequestDocGenerator<T> 
    {
        private static readonly HttpClient client = new HttpClient();

        const string connectionBase = "/";

        private string controller = "";

        private string endPoint { get; set; }


        public RequestDocGenerator(string controller)
        {
            this.controller = controller;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        private static async Task ProcessRepositories()
        {
            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);
        }

        public void createRequestHeader()
        {
            throw new NotImplementedException();
        }
    }
}
