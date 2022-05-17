using CardShow.Shared.Constants;
using CardShow.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.APIServices
{
    public static class AssessmentAPIService
    {
        public static async Task<IEnumerable<Assessment>> GetbyCard(int cardId)
        {
            IEnumerable<Assessment> assessments;

            var url = UrlStrings.baseUrl + UrlStrings.assessments + "/" + cardId;
            var client = new HttpClient();
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
                    assessments = serializer
                        .Deserialize<IEnumerable<Assessment>>(jsonReader);

                    return await Task.FromResult(assessments);
                }
                catch
                {
                    Console.WriteLine("Invalid Json");
                    return new List<Assessment>();
                }
            }

            return new List<Assessment>();
        }

        public static async Task<HttpResponseMessage> Add(Assessment assessment)
        {
            var url = UrlStrings.baseUrl + UrlStrings.assessments;
            var client = new HttpClient();
            return await client
                .PostAsJsonAsync(url, assessment);
        }

        public static async Task<HttpResponseMessage> Delete (int id)
        {
            var url = UrlStrings.baseUrl +
                UrlStrings.assessments + "/delete";

            using var client = new HttpClient();
            return await client
                .PostAsJsonAsync(url, id);
        }
    }
}
