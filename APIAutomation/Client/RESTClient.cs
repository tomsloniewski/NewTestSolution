using APIAutomation.Shared.BasicTestConfiguration;
using APIAutomation.Shared.Response;
using Newtonsoft.Json;
using RestSharp;

namespace APIAutomation.Client;

public class RESTClient
{
    private readonly RestClient Client;
    private readonly string BaseUrl;
    private readonly JsonSerializer Serializer;

    public RESTClient(Configuration configuration)
    {
        BaseUrl = configuration.BaseUrl!;
        RestClientOptions options = new RestClientOptions(this.BaseUrl)
        {
            MaxTimeout = configuration.MaxTimeout,
        };
        Client = new RestClient(options);
        Serializer = new JsonSerializer();
    }

    public async Task<T> PostAsync<T>(RestRequest request)
    {
        RestResponse response = await Client.PostAsync(request);
        IBaseResponse deserialized = JsonConvert.DeserializeObject<T>(response.Content) as IBaseResponse;
        deserialized.ResponseObject = response;
        return (T) deserialized;
    }

    public async Task<T> GetAsync<T>(RestRequest request)
    {
        RestResponse response = await Client.GetAsync(request);
        IBaseResponse deserialized = JsonConvert.DeserializeObject<T>(response.Content) as IBaseResponse;
        deserialized.ResponseObject = response;
        return (T) deserialized;
    }
}
