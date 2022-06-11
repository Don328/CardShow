using CardShow.Shared.Constants;
using CardShow.Shared.Enums;
using CardShow.Shared.Interfaces;
using CardShow.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Services
{
    public class APIService<T> : IAPIService<T>
    {
        private readonly Type type;
        private APIRequestType reqType;

        public APIService()
        {
            type = typeof(T);
        }

        public async Task<IEnumerable<T>> Get()
        {
            var url = UrlBuilder.Build(type, reqType);
            return await SubmitGetRequest(url);
        }

        public async Task<IEnumerable<T>> Get(int parentId)
        {
            var url = UrlBuilder.Build(type, reqType, (int)parentId);
            return await SubmitGetRequest(url);
        }

        public async Task<HttpResponseMessage> Add(T newEntry)
        {
            reqType = APIRequestType.Add;
            var url = UrlBuilder.Build(type, reqType);
            using var client = new HttpClient();

            return await client.PostAsJsonAsync(url, newEntry);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            reqType = APIRequestType.Delete;
            var url = UrlBuilder.Build(type, reqType, id);
            using var client = new HttpClient();
            return await client.PostAsJsonAsync(url, id);
        }

        private async Task<IEnumerable<T>> SubmitGetRequest(string url)
        {
            IEnumerable<T> list;
            reqType = APIRequestType.Get;

            using var client = new HttpClient();
            using var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode
                && response.Content.Headers
                    .ContentType?.MediaType
                    == "application/json")
            {
                using var stream = await response
                    .Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(streamReader);
                var serializer = new JsonSerializer();

                try
                {
                    list = serializer
                        .Deserialize<IEnumerable<T>>(jsonReader);

                    return await Task.FromResult(list);
                }
                catch
                {
                    Console.WriteLine("Invalid Json");
                }
            }

            return await Task.FromResult(new List<T>());
        }
    }
}
