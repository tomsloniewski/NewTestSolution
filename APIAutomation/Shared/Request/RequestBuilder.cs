using APIAutomation.Shared.BasicTestConfiguration;
using RestSharp;

namespace APIAutomation.Shared.Request;

public class RequestBuilder
{
    private RestRequest request;

    public RequestBuilder(BaseTest test, string endpoint, Method method)
    {
        string resource = test.Configuration.BaseUrl + endpoint;
        request = new RestRequest(resource, method);
    }

    public RequestBuilder AddPathParameter(string pathParameterValue, string? pathParameterName = null)
    {
        if (pathParameterName is not null)
        {
            request.Resource = $"{request.Resource}/{pathParameterName}";
        }
        request.Resource = $"{request.Resource}/{pathParameterValue}";
        return this;
    }
    public RequestBuilder AddBody(object body)
    {
        request.AddBody(body);
        return this;
    }

    public RequestBuilder AddHeader(string name, string value)
    {
        request.AddHeader(name, value);
        return this;
    }

    public RestRequest Build()
    {
        return request;
    }
}
