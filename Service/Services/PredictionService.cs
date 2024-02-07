using FromHeartyAI.DataStructures;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Service.DTO;
using System.Reflection;
using System.Text;

namespace Service.Services
{
    public interface IPredictionService
    {
        Task<OpenAiPredictionResponse> GetOpenAiDescriptivePrediction(FromHeartyOpenAiData input);
    }
    public class PredictionService : IPredictionService
    {
        // Not yet done
        public async Task<OpenAiPredictionResponse> GetOpenAiDescriptivePrediction(FromHeartyOpenAiData input)
        {
            var options = new RestClientOptions("https://api.openai.com/v1/completions")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 100000, // 100 seconds
                Authenticator = new JwtAuthenticator("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")
            };

            var client = new RestClient(options);

            var request = new RestRequest();
            request.Method = Method.Post;

            var parameters = new StringBuilder();
            Type inputTypes = typeof(FromHeartyData);
            PropertyInfo[] properties = inputTypes.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(input);
                if (value != null)
                {
                    parameters.Append($"{property.Name}: {value}");
                }
            }

            request.AddHeader("Content-Type", "application/json");
            var body = $$"""
            {
            "model": "gpt-3.5-turbo-instruct",
            "prompt": "Please provide a very short explanation on why this person has a {{input.Percentage}}% chance of heart problem based on these parameters: {{parameters}}",
            "temperature": 0,
            "max_tokens": 600
            }
            """;


            request.AddParameter("application/json", body, ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var json = JObject.Parse(response.Content);
            OpenAiPredictionResponse res = JsonConvert.DeserializeObject<OpenAiPredictionResponse>(json.ToString());

            return res;
        }
    }
}
