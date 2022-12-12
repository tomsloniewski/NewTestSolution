using APIAutomation.Shared.BasicTestConfiguration;
using APIAutomation.Shared.Response;
using Newtonsoft.Json;
using RestSharp;

namespace APIAutomation.Shared.Client;

public class RESTClient
{
    private readonly RestClient client;
    private readonly string baseUrl;

    public RESTClient(Configuration configuration)
    {
        baseUrl = configuration.BaseUrl!;
        RestClientOptions options = new RestClientOptions(this.baseUrl)
        {
            MaxTimeout = configuration.MaxTimeout,
        };
        client = new RestClient(options);
    }

    public async Task<T> PostAsync<T>(RestRequest request)
    {
        RestResponse response = await client.PostAsync(request);
        IBaseResponse deserialized = JsonConvert.DeserializeObject<T>(response.Content) as IBaseResponse;
        deserialized.ResponseObject = response;
        return (T) deserialized;
    }

    public async Task<T> GetAsync<T>(RestRequest request)
    {
        RestResponse response = await client.GetAsync(request);
        IBaseResponse deserialized = JsonConvert.DeserializeObject<T>(response.Content) as IBaseResponse;
        deserialized.ResponseObject = response;
        return (T) deserialized;
    }
}
